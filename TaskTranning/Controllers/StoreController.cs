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
    public class StoreController : Controller
    {
        /// <summary>
        /// declare store services
        /// </summary>
        private readonly IStoreServices _storeServices;
       
        /// <summary>
        /// declare store resources
        /// </summary>
        private readonly ResourcesServices<StoreResource> _resourcesServices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeServices">declare storeServices</param>
        /// <param name="resourcesServices"></param>
        public StoreController(IStoreServices storeServices, ResourcesServices<StoreResource> resourcesServices)
        {
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
            Log.Information("List Store");
            var listStores = await _storeServices.GetListAsync();
            if (listStores == null)
            {
                return NotFound();
            }
            ViewBag.Count = listStores.Count;
            return View(listStores);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create Store</returns>
        [HttpGet]
        public IActionResult Create()
        {
            Log.Information("Create Store");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createStore"></param>
        /// <returns>Create Store</returns>
        [HttpPost]
        public async Task<IActionResult> Create(StoreViewModel createStore)
        {
            if (ModelState.IsValid)
            {
                if (await _storeServices.CreateStore(createStore))
                {
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_NewStoreSuccess").ToString();
                    return RedirectToAction("Index");
                }  
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_NewStore");
                return View(createStore);
            }
            return View(createStore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit Store</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Log.Information("Update Store");
            if (id == 0)
            {
                return BadRequest();
            }
            var getId = await _storeServices.GetById(id);
            if (getId == null)
            {
                return NotFound();
            }
            return View(getId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateStore"></param>
        /// <returns>Update Store</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(StoreViewModel updateStore)
        {
            if (ModelState.IsValid)
            {
                if (await _storeServices.EditStore(updateStore))
                {
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_EditStoreSuccess").ToString();
                    return RedirectToAction("Index");
                } 
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_EditStore");
                return View(updateStore);
            }
            return View(updateStore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Store</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Log.Information("Delete Store");
            if (id == 0)
            {
                return BadRequest();
            }
            if (await _storeServices.DeleteStore(id))
            {
                TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_DeleteStoreSuccess").ToString();
                return RedirectToAction("Index");
            }
            return View("Index");

        }
    }
}