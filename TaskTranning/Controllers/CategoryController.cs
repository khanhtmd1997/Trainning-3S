using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        /// 
        /// </summary>
        /// <param name="categoryServices">declare categoryServices</param>
        /// <param name="resourcesServices"></param>
        public CategoryController(ICategoryServices categoryServices,ResourcesServices<CategoryResource> resourcesServices)
        {
            _categoryServices = categoryServices;
            _resourcesServices = resourcesServices;
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