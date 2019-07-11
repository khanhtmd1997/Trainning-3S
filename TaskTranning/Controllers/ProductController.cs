using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
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
        /// declare product resources
        /// </summary>
        private readonly ResourcesServices<CommonResource> _commonResource;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productServices">declare productServices</param>
        /// <param name="brandServices"></param>
        /// <param name="categoryServices"></param>
        /// <param name="resourcesServices"></param>
        /// <param name="commonResource"></param>
        public ProductController(IProductServices productServices,IBrandServices brandServices,ICategoryServices categoryServices,
            ResourcesServices<ProductResource> resourcesServices,ResourcesServices<CommonResource> commonResource)
        {
            _productServices = productServices;
            _brandServices = brandServices;
            _categoryServices = categoryServices;
            _resourcesServices = resourcesServices;
            _commonResource = commonResource;
        }
        
        /// <summary>
        /// ExportExcelProduct
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ExportExcelProduct()
        {
            var columnTitle = new string[]
            {
                "Product Name",
                "Brand Name",
                "CategoryName",
                "Model Year",
                "List Price"
            };

            byte[] result;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Product");
                using (var cells = worksheet.Cells[1,1,1,5])
                {
                    cells.Style.Font.Bold = true;
                }

                for (var i = 0; i < columnTitle.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = columnTitle[i];
                }

                var j = 2;
                foreach (var product in _productServices.GetProducts())
                {
                    worksheet.Cells["A" + j].Value = product.ProductName;
                    worksheet.Cells["B" + j].Value = product.BrandId;
                    worksheet.Cells["C" + j].Value = product.CategoryId;
                    worksheet.Cells["D" + j].Value = product.ModelYear;
                    worksheet.Cells["E" + j].Value = product.ListPrice;
                    j++;
                }

                var b = 2;
                foreach (var brand in _brandServices.GetBrands() )
                {
                    worksheet.Cells["G" + b].Value = brand.Id;
                    worksheet.Cells["H" + b].Value = brand.BrandName;
                    b++;
                }
                
                var c  = 2;
                foreach (var category in _categoryServices.GetCategories() )
                {
                    worksheet.Cells["J" + c].Value = category.Id;
                    worksheet.Cells["K" + c].Value = category.CategoryName;
                    c++;
                }

                result = package.GetAsByteArray();
            }

            return File(result,"application/ms-excel",$"Product.xlsx");
        }

        public async Task<IActionResult> ImportExcelProduct(IFormFile formFile)
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

            var import =await _productServices.ImporTask(formFile);
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