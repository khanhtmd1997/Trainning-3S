using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using TaskTranning.Controllers;
using TaskTranning.Models;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;
using Xunit;

namespace TestsXUnit.ControllerTest
{
    public class UserTest
    {
        private readonly UserController _userController;
        private readonly CodeFirstDataContext _context;

        public UserTest()
        {
            _context = TestHelpers.GetDataContext();
            AutoMapperConfig.Initialize();
            var mapper = AutoMapperConfig.GetMapper();
            var userServices = new UserServices(_context, mapper);
            var storeServices = new StoreServices(_context,mapper);
            var options = Options.Create(new LocalizationOptions());
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new ResourcesServices<UserResource>(factory);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["succcessMessage"] = localizer.GetLocalizedHtmlString("msg_NewUserSuccess"),
                ["errorMessage"] = localizer.GetLocalizedHtmlString("err_NewUser"),
                ["succcessMessage"] = localizer.GetLocalizedHtmlString("msg_EditUserSuccess"),
                ["errorMessage"] = localizer.GetLocalizedHtmlString("err_EditUser"),
                ["succcessMessage"] = localizer.GetLocalizedHtmlString("msg_DeleteUserSuccess"),
                ["errorMessage"] = localizer.GetLocalizedHtmlString("err_DeleteUser")
            };
            _userController = new UserController(userServices, storeServices, localizer)
            {
                TempData = tempData
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Index When Success</returns>
        [Fact]
        public async Task Index_WhenHaveListUser()
        {
            var listUser = await _userController.Index();
            Assert.IsType<ViewResult>(listUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create Success</returns>
        [Fact]
        public async Task Create_WhenSuccess()
        {
            var user = new AddUserViewModel
            {
                Email = "khanhadd@gmail.com",
                PassWord = "Khanh123",
                FullName = "Nguyen Van add",
                Phone = 0147258369,
                Role = 1,
                StoreId = 1,
                IsActive = true,
                Picture = "logo.png"
            };
            var createUser = await _userController.Create(user);
            Assert.IsType<RedirectToActionResult>(createUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create User Error Ivalid</returns>
        [Fact]
        public async Task Create_WhenIvalidError()
        {
            var user = new AddUserViewModel
            {
                Email = "",
                PassWord = "",
                FullName = "",
                Phone = 0147258369,
                Role = 1,
                StoreId = 1,
                IsActive = true,
                Picture = ""
            };
            _userController.ModelState.AddModelError("Create","Error");
            var createUser = await _userController.Create(user);
            Assert.IsType<ViewResult>(createUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update When Have Id</returns>
        [Fact]
        public async Task Update_WhenHaveId()
        {
            const int id = 1;
            var user = await _userController.Edit(id);
            Assert.IsType<ViewResult>(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update When Have Not Id</returns>
        [Fact]
        public async Task Update_WhenHaveNotId_BadRequest()
        {
            const int id = 0;
            var user = await _userController.Edit(id);
            Assert.IsType<BadRequestResult>(user);
        }

        [Fact]
        public async Task Update_Success()
        {
            var user = new EditUserViewModel
            {
                Id = 1,
                Email = "khanhedit@gmail.com",
                FullName = "Nguyen Van Edit",
                Phone = 0123456789,
                Role = 1,
                StoreId = 1,
                IsActive = false
            };
            var updateUser = await _userController.Edit(user);
            Assert.IsType<ViewResult>(updateUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update When Ivalid Error</returns>
        [Fact]
        public async Task Update_WhenIvalidError()
        {
            var user = new EditUserViewModel
            {
                Id = 1,
                Email = "",
                FullName = "",
                Phone = 0123456789,
                Role = 1,
                StoreId = 1,
                IsActive = false
            };
            _userController.ModelState.AddModelError("Update","Error");
            var updateUser = await _userController.Edit(user);
            Assert.IsType<ViewResult>(updateUser);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update Password When Have Id</returns>
        [Fact]
        public async Task UpdatePassword_WhenHaveId()
        {
            const int id = 1;
            var user = await _userController.EditPassword(id);
            Assert.IsType<PartialViewResult>(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update When Have Not Id</returns>
        [Fact]
        public async Task UpdatePassword_WhenHaveNotId_BadRequest()
        {
            const int id = 0;
            var user = await _userController.EditPassword(id);
            Assert.IsType<BadRequestResult>(user);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update Password Success</returns>
        [Fact]
        public async Task UpdatePassword_Success()
        {
            var user = new EditPasswordUserViewModel
            {
                Id = 1,
                NewPassword = "khanh123",
                ConfirmPassword = "khanh123"
            };
            var updateUser = await _userController.EditPassword(user);
            Assert.IsType<PartialViewResult>(updateUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update Password When Ivalid Error</returns>
        [Fact]
        public async Task UpdatePassword_WhenIvalidError()
        {
            var user = new EditPasswordUserViewModel
            {
                Id = 1,
                NewPassword = "khanh123",
                ConfirmPassword = "Khanh123"
            };
            _userController.ModelState.AddModelError("Update Password","Error");
            var updateUser = await _userController.EditPassword(user);
            Assert.IsType<PartialViewResult>(updateUser);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Delete When Have Id</returns>
        [Fact]
        public async Task Delete_WhenHaveId()
        {
            const int id = 1;
            var user = await _userController.Delete(id);
            Assert.IsType<ViewResult>(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Delete When Have Not Id</returns>
        [Fact]
        public async Task Delete_WhenHaveNotId_BadRequest()
        {
            const int id = 0;
            var user = await _userController.Delete(id);
            Assert.IsType<BadRequestResult>(user);
        }
        
        
    }
}