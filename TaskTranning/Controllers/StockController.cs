using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;
using IProductServices = TaskTranning.Services.IProductServices;
using IStockServices = TaskTranning.Services.IStockServices;

namespace TaskTranning.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        /// <summary>
        /// declare stock services
        /// </summary>
        private readonly IStockServices _stockServices;
        
        /// <summary>
        /// declare product services
        /// </summary>
        private readonly IProductServices _productServices;
        
        /// <summary>
        /// declare store services
        /// </summary>
        private readonly IStoreServices _storeServices;

        /// <summary>
        /// declare stock resources
        /// </summary>
        private readonly ResourcesServices<StockResource> _resourcesServices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockServices">declare stockServices</param>
        /// <param name="productServices"></param>
        /// <param name="storeServices"></param>
        /// <param name="resourcesServices"></param>
        public StockController(IStockServices stockServices, IProductServices productServices, IStoreServices storeServices,
            ResourcesServices<StockResource> resourcesServices)
        {
            _stockServices = stockServices;
            _productServices = productServices;
            _storeServices = storeServices;
            _resourcesServices = resourcesServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Index</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Log.Information("List Stock");
            var listStocks = await _stockServices.GetListAsync();
            if (listStocks == null)
            {
                return NotFound();
            }
            ViewBag.Count = listStocks.Count;
            return View(listStocks);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create</returns>
        [HttpGet]
        public IActionResult Create()
        {
            Log.Information("Create Stock");
            ViewBag.ProductId = new SelectList(_productServices.GetProducts(),"Id","ProductName");
            ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName");
            return View();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createStock"></param>
        /// <returns>Create Stock</returns>
        [HttpPost]
        public async Task<IActionResult> Create(StockViewModel createStock)
        {
            if (ModelState.IsValid)
            {
                if (await _stockServices.CreateStock(createStock))
                {
                    ViewBag.ProductId = new SelectList(_productServices.GetProducts(),"Id","ProductName",createStock.ProductId);
                    ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName",createStock.StoreId);
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_NewStockSuccess").ToString();
                    return RedirectToAction("Index");
                }   
                ViewBag.ProductId = new SelectList(_productServices.GetProducts(),"Id","ProductName",createStock.ProductId);
                ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName",createStock.StoreId);
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_NewStock");
                return View(createStock);
            }
            ViewBag.ProductId = new SelectList(_productServices.GetProducts(),"Id","ProductName",createStock.ProductId);
            ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName",createStock.StoreId);
            return View(createStock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <returns>Edit Stock</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int productId , int storeId)
        {
            Log.Information("Update Stock");
            if (productId == 0 || storeId == 0)
            {
                return BadRequest();
            }
            var getId = await _stockServices.GetById(productId,storeId);
            if (getId == null)
            {
                return NotFound();
            }
            ViewBag.ProductId = new SelectList(_productServices.GetProducts(),"Id","ProductName");
            ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName");
            return View(getId);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateStock"></param>
        /// <returns>Update Stock</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(StockViewModel updateStock)
        {
            if (ModelState.IsValid)
            {
                if (await _stockServices.EditStock(updateStock))
                {
                    ViewBag.ProductId = new SelectList(_productServices.GetProducts(),"Id","ProductName",updateStock.ProductId);
                    ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName",updateStock.StoreId);
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_EditStockSuccess").ToString();
                    return RedirectToAction("Index");
                }
                ViewBag.ProductId = new SelectList(_productServices.GetProducts(),"Id","ProductName",updateStock.ProductId);
                ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName",updateStock.StoreId);
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_EditStock");
                return View(updateStock);
            }
            ViewBag.ProductId = new SelectList(_productServices.GetProducts(),"Id","ProductName",updateStock.ProductId);
            ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName",updateStock.StoreId);
            return View(updateStock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <returns>Delete Stock</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int productId,int storeId)
        {
            Log.Information("Delete Stock");
            if (productId == 0 || storeId == 0)
            {
                return BadRequest();
            }
            if (await _stockServices.DeleteStock(productId,storeId))
            {
                TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_DeleteStockSuccess").ToString();
                return RedirectToAction("Index");
            }
            return View("Index");

        }
    }
}