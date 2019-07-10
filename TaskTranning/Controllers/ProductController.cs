using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;
using ICategoryServices = TaskTranning.Services.ICategoryServices;
using IProductServices = TaskTranning.Services.IProductServices;

namespace TaskTranning.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        /// <summary>
        /// declare product services
        /// </summary>
        private readonly IProductServices _productServices;

        /// <summary>
        /// declare product services
        /// </summary>
        private readonly IBrandServices _brandServices;

        /// <summary>
        /// declare product services
        /// </summary>
        private readonly ICategoryServices _categoryServices;

        /// <summary>
        /// declare product resources
        /// </summary>
        private readonly ResourcesServices<ProductResource> _resourcesServices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productServices">declare productServices</param>
        /// <param name="brandServices"></param>
        /// <param name="categoryServices"></param>
        /// <param name="resourcesServices"></param>
        public ProductController(IProductServices productServices,IBrandServices brandServices,ICategoryServices categoryServices,
            ResourcesServices<ProductResource> resourcesServices)
        {
            _productServices = productServices;
            _brandServices = brandServices;
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
            Log.Information("List Products");
            var listProducts = await _productServices.GetListAsync();
            if (listProducts == null)
            {
                return NotFound();
            }
            ViewBag.Count = listProducts.Count;
            return View(listProducts);

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create Product</returns>
        [HttpGet]
        public IActionResult Create()
        {
            Log.Information("Create Product");
            ViewBag.BrandId = new SelectList(_brandServices.GetBrands(),"Id","BrandName");
            ViewBag.CategoryId = new SelectList(_categoryServices.GetCategories(),"Id","CategoryName");
            return View();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createProduct"></param>
        /// <returns>Create Product</returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel createProduct)
        {
            if (ModelState.IsValid)
            {
                if (await _productServices.CreateProduct(createProduct))
                {
                    ViewBag.BrandId = new SelectList(_brandServices.GetBrands(), "Id", "BrandName", createProduct.BrandId);
                    ViewBag.CategoryId = new SelectList(_categoryServices.GetCategories(), "Id", "CategoryName",createProduct.CategoryId);
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_NewProductSuccess").ToString();
                    return RedirectToAction("Index");
                }
                ViewBag.BrandId = new SelectList(_brandServices.GetBrands(), "Id", "BrandName", createProduct.BrandId);
                ViewBag.CategoryId = new SelectList(_categoryServices.GetCategories(), "Id", "CategoryName",createProduct.CategoryId);
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_NewProduct");
                return View(createProduct);
            }
            ViewBag.BrandId = new SelectList(_brandServices.GetBrands(), "Id", "BrandName", createProduct.BrandId);
            ViewBag.CategoryId = new SelectList(_categoryServices.GetCategories(), "Id", "CategoryName",createProduct.CategoryId);
            return View(createProduct);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit Product</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Log.Information("Update Product");
            if (id == 0)
            {
                return BadRequest();
            }
            var getId = await _productServices.GetById(id);
            if (getId == null)
            {
                return NotFound();
            }
            ViewBag.BrandId = new SelectList(_brandServices.GetBrands(),"Id","BrandName");
            ViewBag.CategoryId = new SelectList(_categoryServices.GetCategories(),"Id","CategoryName");
            return View(getId);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateProduct"></param>
        /// <returns>Update Product</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel updateProduct)
        {          
            if (ModelState.IsValid)
            {
                if (await _productServices.EditProduct(updateProduct))
                {
                    ViewBag.BrandId = new SelectList(_brandServices.GetBrands(), "Id", "BrandName", updateProduct.BrandId);
                    ViewBag.CategoryId = new SelectList(_categoryServices.GetCategories(), "Id", "CategoryName",updateProduct.CategoryId);
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_EditProductSuccess").ToString();
                    return RedirectToAction("Index");
                } 
                ViewBag.BrandId = new SelectList(_brandServices.GetBrands(), "Id", "BrandName", updateProduct.BrandId);
                ViewBag.CategoryId = new SelectList(_categoryServices.GetCategories(), "Id", "CategoryName",updateProduct.CategoryId);
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_EditProduct");
                return View(updateProduct);
            }
            ViewBag.BrandId = new SelectList(_brandServices.GetBrands(), "Id", "BrandName", updateProduct.BrandId);
            ViewBag.CategoryId = new SelectList(_categoryServices.GetCategories(), "Id", "CategoryName",updateProduct.CategoryId);
            return View(updateProduct);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit Product Image</returns>
        [HttpGet]
        public async Task<IActionResult> EditProductImage(int id)
        {
            Log.Information("Edit Image Product");
            if (id == 0)
            {
                return BadRequest();
            }
            var getId = await _productServices.GetByIdPicture(id);
            if (getId == null)
            {
                return NotFound();
            }
            return PartialView("_ChangePicture",getId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateImage"></param>
        /// <returns>Update Product Image</returns>
        [HttpPost]
        public async Task<IActionResult> EditProductImage(EditPictureProductViewModel updateImage)
        {
            if (ModelState.IsValid)
            {
                if (await _productServices.EditPicture(updateImage))
                {
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_EditImageProductSuccess").ToString();
                    return PartialView("_ChangePicture");
                }  
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_EditImageProduct");
                return PartialView("_ChangePicture",updateImage);
            }
            return PartialView("_ChangePicture",updateImage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Product</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Delete Product");
            if (id == 0)
            {
                return BadRequest();
            }
            if (await _productServices.DeleteProduct(id))
            {
                TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_DeleteProductSuccess").ToString();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

    }
}