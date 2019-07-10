using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;

namespace TaskTranning.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        /// <summary>
        /// reclare user services
        /// </summary>
        private readonly IUserServices _userServices;
        /// <summary>
        /// reclare store services
        /// </summary>
        private readonly IStoreServices _storeServices;
        
        /// <summary>
        /// reclare user resources
        /// </summary>
        private readonly ResourcesServices<UserResource> _resourcesServices;
        
//        /// <summary>
//        /// declare logger
//        /// </summary>
//        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userServices">declare userServices</param>
        /// <param name="storeServices"></param>
        /// <param name="resourcesServices"></param>
        public UserController(IUserServices userServices,IStoreServices storeServices,
            ResourcesServices<UserResource> resourcesServices)
        {
            _userServices = userServices;
            _storeServices = storeServices;
            _resourcesServices = resourcesServices;
            //_logger = logger;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Index</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {   
            Log.Information("List User");
            var listUsers = await _userServices.GetListAsync();
            ViewBag.Count = listUsers.Count;
            return View(listUsers.ToList());            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create User</returns>
        [HttpGet]
        public IActionResult Create()
        {
            Log.Information("Create User");
            ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName");
            ViewBag.RoleName = new SelectList(_userServices.GetUsers(),"Id","Role");
            return View();         
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns>Create User</returns>
        [HttpPost]
        public async Task<IActionResult> Create(AddUserViewModel createUser)
        { 
            if (ModelState.IsValid)
            {
                if (await _userServices.CreateUser(createUser))
                {
                    ViewBag.StoreId = new SelectList(_storeServices.GetStores(), "Id", "StoreName", createUser.StoreId);
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_NewUserSuccess").ToString();
                    return RedirectToAction("Index");  
                }     
                ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName",createUser.StoreId);
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_NewUser");
                return View(createUser);
            }
            ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName",createUser.StoreId);
            return View(createUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit User</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Log.Information("Update User");
            if (id == 0)
            {
                return BadRequest();
            }
            var getId = await _userServices.GetById(id);
            ViewBag.StoreId = new SelectList(_storeServices.GetStores(),"Id","StoreName");
            return View(getId);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns>Update User</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel updateUser)
        {         
            if (ModelState.IsValid)
            {
                if (await _userServices.EditUser(updateUser))
                {
                    ViewBag.StoreId = new SelectList(_storeServices.GetStores(), "Id", "StoreName", updateUser.StoreId);
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_EditUserSuccess").ToString();
                    return RedirectToAction("Index");
                }
                ViewBag.StoreId = new SelectList(_storeServices.GetStores(), "Id", "StoreName", updateUser.StoreId);
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_EditUser");
                return View(updateUser);
            }
            ViewBag.StoreId = new SelectList(_storeServices.GetStores(), "Id", "StoreName", updateUser.StoreId);
            return View(updateUser);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit Password</returns>
        [HttpGet]
        public async Task<IActionResult> EditPassword(int id)
        {
            Log.Information("Edit Password User");
            if (id == 0)
            {
                return BadRequest();
            }
            var getId = await _userServices.GetByIdPassword(id);
            return PartialView("_ChangePassword",getId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatePassword"></param>
        /// <returns>Update Password User</returns>
        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPasswordUserViewModel updatePassword)
        {
            if (ModelState.IsValid)
            {
                var password = await _userServices.EditPassword(updatePassword);
                if(password)
                {
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_EditUserPasswordSuccess").ToString();
                }  
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_EditUserPassword");
                return PartialView("_ChangePassword",updatePassword);
            }
            return PartialView("_ChangePassword",updatePassword);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete User</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            if (await _userServices.DeleteUser(id))
            {
                TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_DeleteUserSuccess").ToString();
                return RedirectToAction("Index");
            }
            ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_DeleteUser").ToString();
            return View("Index");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit Product Image</returns>
        [HttpGet]
        public async Task<IActionResult> EditUserImage(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var getId = await _userServices.GetByIdPicture(id);
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
        public async Task<IActionResult> EditUserImage(EditPictureUserViewModel updateImage)
        {
            if (ModelState.IsValid)
            {
                if (await _userServices.EditPicture(updateImage))
                {
                    TempData["succcessMessage"] = _resourcesServices.GetLocalizedHtmlString("msg_EditImageUserSuccess").ToString();
                    return PartialView("_ChangePicture");
                }  
                ViewData["errorMessage"] = _resourcesServices.GetLocalizedHtmlString("err_EditImageUser");
                return PartialView("_ChangePicture",updateImage);
            }
            return PartialView("_ChangePicture",updateImage);
        }
    }
}