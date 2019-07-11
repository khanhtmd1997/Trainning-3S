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
    public class BrandController : Controller
    {
        /// <summary>
        /// declare brand services
        /// </summary>
        private readonly IBrandServices _brandServices;
        
        /// <summary>
        /// declare brand resource file
        /// </summary>
        private readonly ResourcesServices<BrandResource> _resourcesServices;
        
        /// <summary>
        /// declare product resources
        /// </summary>
        private readonly ResourcesServices<CommonResource> _commonResource;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandServices">declare brandServices</param>
        /// <param name="resourcesServices"></param>
        /// <param name="commonResource"></param>
        public BrandController(IBrandServices brandServices, ResourcesServices<BrandResource> resourcesServices,ResourcesServices<CommonResource> commonResource)
        {
            _brandServices = brandServices;
            _resourcesServices = resourcesServices;
            _commonResource = commonResource;
        }
        
        /// <summary>
        /// ExportExcelBrand
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ExportExcelBrand()
        {
            var comlumHeadrs = new[]
            {
                "Brand Name"
            };
            byte[] result;
            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add("Brand"); //Worksheet name
                using (var cells = worksheet.Cells[1, 1, 1, 5]) //(1,1) (1,5)
                {
                    cells.Style.Font.Bold = true;
                }

                //First add the headers
                for (var i = 0; i < comlumHeadrs.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = comlumHeadrs[i];
                }

                //Add values
                var j = 2;
                foreach (var brand in _brandServices.GetBrands())
                {
                    //worksheet.Cells["A" + j].Value = brand.Id;
                    worksheet.Cells["A" + j].Value = brand.BrandName;
                    j++;
                }

                result = package.GetAsByteArray();
            }
            return File(result,"application/ms-excel", $"Brand.xlsx");
        }
        
        [HttpPost]  
        public async Task<IActionResult> ImportExcelBrand(IFormFile formFile)  
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
            var import = await _brandServices.ImporTask(formFile);
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
            Log.Information("List Brands");
            var listBrands = await _brandServices.GetListAsync();
            if (listBrands == null)
            {
                return NotFound();
            }
            ViewBag.Count = listBrands.Count;
            return View(listBrands);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create Brand</returns>
        [HttpGet]
        public IActionResult Create()
        {
            Log.Information("Create Brand");
            return View();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createBrand"></param>
        /// <returns>Create Brand</returns>
        [HttpPost]
        public async Task<IActionResult> Create(BrandViewModel createBrand)
        {
            if (ModelState.IsValid)
            {
                if (await _brandServices.CreateBrand(createBrand))
                {
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_NewBrandSuccess").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_NewBrand");
                return View(createBrand);
            }
            return View(createBrand);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit Brand</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Log.Information("Update Brand");
            if (id == 0)
            {
                return BadRequest();
            }
            var getId = await _brandServices.GetById(id);
            if (getId == null)
            {
                return NotFound();
            }
            return View(getId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateBrand"></param>
        /// <returns>Update Brand</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(BrandViewModel updateBrand)
        {
            if (ModelState.IsValid)
            {
                if (await _brandServices.EditBrand(updateBrand))
                {
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_EditBrandSuccess").ToString();
                    return RedirectToAction("Index");
                }
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_EditBrand");
                return View(updateBrand);
            }
            return View(updateBrand);
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <returns>Delete Brand</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Delete Brand");
            if (id == 0)
            {
                return BadRequest();
            }
            if (await _brandServices.DeleteBrand(id))
            {
                TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_DeleteBrandSuccess").ToString();
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}