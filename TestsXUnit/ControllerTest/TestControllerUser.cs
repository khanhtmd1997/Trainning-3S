using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using TaskTranning.Controllers;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;
using Xunit;

namespace TestsXUnit.ControllerTest
{
    public class TestControllerUser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get List Users</returns>
        private static List<AddUserViewModel> GetListUser()
        {
            var user = new List<AddUserViewModel>
            {
                new AddUserViewModel
                {
                    Email = "khanh@gmail.com",
                    PassWord = "Khanh123",
                    IsActive = true,
                    FullName = "Nguyen Van Khanh",
                    Address = "25/23/131 Tran Phu",
                    Phone = 0364606406,
                    Role = 1,
                    StoreId = 1,
                    Picture = "logo.png"
                }
            };
            return user;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get List Have Not User</returns>
        private static List<AddUserViewModel> GetListHaveNotUser()
        {
            var user = new List<AddUserViewModel>();
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Index Have List User</returns>
        [Fact]
        public async Task Index_WhenHaveUsers()
        {
            var userRepo = new Mock<IUserServices>();
            userRepo.Setup(repo => repo.GetListAsync()).ReturnsAsync(GetListUser);
            var storeRepo = new Mock<IStoreServices>();
            var userController = new UserController(userRepo.Object,storeRepo.Object,null);
            var actionResult = await userController.Index();
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<AddUserViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(1,model.Count());
            Assert.NotNull(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Index Have Not List User</returns>
        [Fact]
        public async Task Index_WhenHaveNotUsers()
        {
            var userRepo = new Mock<IUserServices>();
            userRepo.Setup(repo => repo.GetListAsync()).ReturnsAsync(GetListHaveNotUser);
            var storeRepo = new Mock<IStoreServices>();
            var userController = new UserController(userRepo.Object,storeRepo.Object,null);
            var actionResult = await userController.Index();
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<IEnumerable<AddUserViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(0,model.Count());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create User</returns>
        [Fact]
        public async Task Create_Success()
        {
            var user = new AddUserViewModel
                {
                    Email = "khanh@gmail.com",
                    PassWord = "Khanh123",
                    IsActive = true,
                    FullName = "Nguyen Van Khanh",
                    Address = "25/23/131 Tran Phu",
                    Phone = 0364606406,
                    Role = 1,
                    StoreId = 1,
                    Picture = "logo.png"
                };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            
            var options = Options.Create(new LocalizationOptions()); // you should not need any params here if using a StringLocalizer<T>
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new ResourcesServices<UserResource>(factory);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["succcessMessage"] = localizer.GetLocalizedHtmlString("msg_NewUserSuccess")
            };

            var userController = new UserController(userRepo.Object, storeRepo.Object, localizer)
            {
                TempData = tempData
            };
            userController.ModelState.AddModelError("error","error");
            var actionResult = await userController.Create(user);
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<AddUserViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create User</returns>
        [Fact]
        public async Task Create_Error()
        {
            var user = new AddUserViewModel
            {                
                FullName = "Nguyen Van Khanh",
                Address = "25/23/131 Tran Phu",
                Phone = 0364606406,
                Role = 1,
                StoreId = 1,
                Picture = "logo.png"
            };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            var options = Options.Create(new LocalizationOptions()); // you should not need any params here if using a StringLocalizer<T>
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new ResourcesServices<UserResource>(factory);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["errorMessage"] = localizer.GetLocalizedHtmlString("err_NewUser")
            };

            var userController = new UserController(userRepo.Object, storeRepo.Object, localizer)
            {
                TempData = tempData
            };
            var actionResult = await userController.Create(user);
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<AddUserViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Edit User When Not Find Id</returns>
        [Fact]
        public async Task EditUser_WhenNotFindId()
        {
            var user = new EditUserViewModel
            {
                Email = "khanh@gmail.com",
                IsActive = true,
                FullName = "Nguyen Van Khanh",
                Address = "25/23/131 Tran Phu",
                Phone = 0364606406,
                Role = 1,
                StoreId = 1
            };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            var userController = new UserController(userRepo.Object,storeRepo.Object,null);
            var actionResult = await userController.Edit(user.Id);
            Assert.IsType<BadRequestResult>(actionResult);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Edit Password When Not Find Id</returns>
        [Fact]
        public async Task EditPassword_WhenNotFindId()
        {
            var user = new EditPasswordUserViewModel
            {
                NewPassword = "Khanh123",
                ConfirmPassword = "Khanh123",
            };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            var userController = new UserController(userRepo.Object,storeRepo.Object,null);
            var actionResult = await userController.Edit(user.Id);
            Assert.IsType<BadRequestResult>(actionResult);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update User Success</returns>
        [Fact]
        public async Task UpdateUser_WhenHaveId_Success()
        {
            var user = new EditUserViewModel
            {
                Id = 1,
                Email = "khanh@gmail.com",
                IsActive = true,
                FullName = "Nguyen Van Khanh",
                Address = "25/23/131 Tran Phu",
                Phone = 0364606406,
                Role = 1,
                StoreId = 1
            };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            var options = Options.Create(new LocalizationOptions()); // you should not need any params here if using a StringLocalizer<T>
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new ResourcesServices<UserResource>(factory);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["succcessMessage"] = localizer.GetLocalizedHtmlString("msg_EditUserSuccess")
            };

            var userController = new UserController(userRepo.Object, storeRepo.Object, localizer)
            {
                TempData = tempData
            };
            userController.ModelState.AddModelError("error","error");
            var actionResult = await userController.Edit(user.Id);
            Assert.NotNull(actionResult);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update User Error</returns>
        [Fact]
        public async Task UpdateUser_WhenError()
        {
            var user = new EditUserViewModel
            {
                FullName = "Nguyen Van Khanh",
                Address = "25/23/131 Tran Phu",
                Phone = 0364606406,
                Role = 1,
                StoreId = 1
            };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            var options = Options.Create(new LocalizationOptions()); // you should not need any params here if using a StringLocalizer<T>
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new ResourcesServices<UserResource>(factory);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["errorMessage"] = localizer.GetLocalizedHtmlString("err_EditUser")
            };

            var userController = new UserController(userRepo.Object, storeRepo.Object, localizer)
            {
                TempData = tempData
            };
            var actionResult = await userController.Edit(user.Id);
            Assert.NotNull(actionResult);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Edit Password Success</returns>        
        [Fact]
        public async Task UpdatePassword_WhenHaveId()
        {
            var user = new EditPasswordUserViewModel
            {
                Id = 1,
                NewPassword = "Khanh123",
                ConfirmPassword = "Khanh123"
            };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            var options = Options.Create(new LocalizationOptions()); // you should not need any params here if using a StringLocalizer<T>
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new ResourcesServices<UserResource>(factory);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["succcessMessage"] = localizer.GetLocalizedHtmlString("msg_EditUserPasswordSuccess")
            };

            var userController = new UserController(userRepo.Object, storeRepo.Object, localizer)
            {
                TempData = tempData
            };
            userController.ModelState.AddModelError("error","error");
            var actionResult = await userController.EditPassword(user);
            var viewResult = Assert.IsType<PartialViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<EditPasswordUserViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Edit Password Error</returns>        
        [Fact]
        public async Task UpdatePassword_WhenError()
        {
            var user = new EditPasswordUserViewModel
            {
                Id = 1,
                ConfirmPassword = "Khanh123"
            };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            var options = Options.Create(new LocalizationOptions()); // you should not need any params here if using a StringLocalizer<T>
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new ResourcesServices<UserResource>(factory);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["errorMessage"] = localizer.GetLocalizedHtmlString("err_EditUserPassword")
            };

            var userController = new UserController(userRepo.Object, storeRepo.Object, localizer)
            {
                TempData = tempData
            };
            var actionResult = await userController.EditPassword(user);
            var viewResult = Assert.IsType<PartialViewResult>(actionResult);
            var model = Assert.IsAssignableFrom<EditPasswordUserViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_WhenHaveId()
        {
            var user = new AddUserViewModel
            {
                Id = 1,
                Email = "khanh@gmail.com",
                PassWord = "Khanh123",
                IsActive = true,
                FullName = "Nguyen Van Khanh",
                Address = "25/23/131 Tran Phu",
                Phone = 0364606406,
                Role = 1,
                StoreId = 1,
                Picture = "logo.png"
            };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            var options = Options.Create(new LocalizationOptions()); // you should not need any params here if using a StringLocalizer<T>
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new ResourcesServices<UserResource>(factory);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["succcessMessage"] = localizer.GetLocalizedHtmlString("msg_DeleteUserSuccess")
            };

            var userController = new UserController(userRepo.Object, storeRepo.Object, localizer)
            {
                TempData = tempData
            };
            var actionResult = await userController.Delete(user.Id);
            Assert.NotNull(actionResult);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_WhenError()
        {
            var user = new AddUserViewModel
            {                
                FullName = "Nguyen Van Khanh",
                Address = "25/23/131 Tran Phu",
                Phone = 0364606406,
                Role = 1,
                StoreId = 1,
                Picture = "logo.png"
            };
            var userRepo = new Mock<IUserServices>();
            var storeRepo = new Mock<IStoreServices>();
            var options = Options.Create(new LocalizationOptions()); // you should not need any params here if using a StringLocalizer<T>
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            var localizer = new ResourcesServices<UserResource>(factory);
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>())
            {
                ["errorMessage"] = localizer.GetLocalizedHtmlString("err_DeleteUser")
            };
            var userController = new UserController(userRepo.Object, storeRepo.Object, localizer)
            {
                TempData = tempData
            };
            var actionResult = await userController.Delete(user.Id);
            Assert.NotNull(actionResult);
        }
    }
}