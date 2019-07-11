using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Serilog;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;

namespace TaskTranning.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        /// <summary>
        /// declare category services
        /// </summary>
        private readonly ICategoryServices _categoryServices;

        /// <summary>
        /// declare category resource
        /// </summary>
        private readonly ResourcesServices<CategoryResource> _resourcesServices;
        
        /// <summary>
        /// declare category resource
        /// </summary>
        private readonly ResourcesServices<CommonResource> _commonResource;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryServices">declare categoryServices</param>
        /// <param name="resourcesServices"></param>
        /// <param name="commonResource"></param>
        public CategoryController(ICategoryServices categoryServices,
            ResourcesServices<CategoryResource> resourcesServices,ResourcesServices<CommonResource> commonResource)
        {
            _categoryServices = categoryServices;
            _resourcesServices = resourcesServices;
            _commonResource = commonResource;
        }

        public IActionResult ExportExcelCategory()
        {
            var columnTitle = new[]
            {
                "Category Name"
            };
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Category");
                using (var cells = worksheet.Cells[1, 1, 1, 5]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                for (var i = 0; i < columnTitle.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = columnTitle[i];
                }

                var j = 2;
                foreach (var category in _categoryServices.GetCategories())
                {
                    //worksheet.Cells["A" + j].Value = category.Id;
                    worksheet.Cells["A" + j].Value = category.CategoryName;
                    j++;
                }

                var result = package.GetAsByteArray();
                return File(result, "application/ms-excel", $"Category.xlsx");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<IActionResult> ImportExcelCategory(IFormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                TempData["importError"] = _commonResource.GetLocalizedHtmlString("err_Import").ToString();
                return Redirect("Index"); 
            }
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))  
            {  
                TempData["importError"] = _commonResource.GetLocalizedHtmlString("err_Import").ToString();
                return Redirect("Index");  
            }

            var import = await _categoryServices.ImporTask(formFile);
            if (import)
            {
                TempData["importSuccess"] = _commonResource.GetLocalizedHtmlString("msg_ImportSuccess").ToString();
                return Redirect("Index");
            }
            TempData["importError"] = _commonResource.GetLocalizedHtmlString("err_Import").ToString();
            return Redirect("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Index</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Log.Information("List Categories");
            var listCategories = await _categoryServices.GetListAsync();
            if (listCategories == null)
            {
                return NotFound();
            }
            ViewBag.Count = listCategories.Count;
            return View(listCategories);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create Category</returns>
        [HttpGet]
        public IActionResult Create()
        {
            Log.Information("Create Category");
            return View();

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createCategory"></param>
        /// <returns>Create Category</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel createCategory)
        {
            if (ModelState.IsValid)
            {
                if (await _categoryServices.CreateCategory(createCategory))
                {
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_NewCategorySuccess").ToString();
                    return RedirectToAction("Index");
                } 
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_NewCategory");
                return View(createCategory);
            }
            return View(createCategory);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit Category</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Log.Information("Update Cateogy");
            if (id == 0)
            {
                return BadRequest();
            }
            var getId = await _categoryServices.GetById(id);
            if (getId == null)
            {
                return NotFound();
            }
            return View(getId);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateCategory"></param>
        /// <returns>Update Category</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel updateCategory)
        {
            
            
            if (ModelState.IsValid)
            {
                if (await _categoryServices.EditCategory(updateCategory))
                {
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_EditCategorySuccess").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_EditCategory");
                return View(updateCategory);
            }
            return View(updateCategory);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Category</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Delete Cateogy");
            if (id == 0)
            {
                return BadRequest();
            }
            if (await _categoryServices.DeleteCategory(id))
            {
                TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_DeleteCategorySuccess").ToString();
                return RedirectToAction("Index");
            }
            return View("Index");

        }

    }
}