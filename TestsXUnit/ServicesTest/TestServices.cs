using System.Threading.Tasks;
using AutoMapper;
using TaskTranning.Models;
using TaskTranning.Services;
using TaskTranning.ViewModels;
using Xunit;
using TaskTranning.Infrastructure;
namespace TestsXUnit.ServicesTest
{
    public class TestServices
    {
        private readonly CodeFirstDataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly UserServices _userService;

        /// <summary>
        /// declare user / declare store
        /// </summary>
        public TestServices()
        {
            _dataContext = TestHelpers.GetDataContext();
            AutoMapperConfig.Initialize();
            _mapper = AutoMapperConfig.GetMapper();

            var user = new User { 
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
            _dataContext.User.Add(user);
            _dataContext.SaveChanges();
            
            var store = new Store
            {
                Id = 1,
                StoreName = "StoreName Test",
                Phone = 0132456789,
                Email = "storetest@gmail.com",
                Street = "123/123 Co Nhue",
                City = "Ha Noi",
                State = "",
                ZipCode = "12000"
                
            };
            _dataContext.Store.Add(store);
            _dataContext.SaveChanges();
            
            _userService = new UserServices(_dataContext, _mapper);
        }
        
        /// <summary>
        /// Get List Users
        /// </summary>
        [Fact]
        public void GetUsersTest()
        {
            var result = _userService.GetUsers();
            Assert.NotNull(result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create User</returns>
        [Fact]
        public async Task CreateUserTest_ReturnTrue()
        {
            var user = new AddUserViewModel { 
                Email = "khanh123@gmail.com",
                PassWord = "Khanh123",
                IsActive = true,
                FullName = "Tran Thi Hoai Thu",
                Address = "25/23/131 Tran Phu",
                Phone = 0147258369,
                Role = 1,
                StoreId = 1
            };
            var result = await _userService.CreateUser(user);
            Assert.True(result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Create User</returns>
        [Fact]
        public async Task CreateUserTest_ReturnFalse()
        {
            var user = new AddUserViewModel { 
                Email = "khanh123@gmail.com",
                IsActive = true,
                FullName = "Tran Thi Hoai Thu",
                Address = "25/23/131 Tran Phu",
                Phone = 0147258369,
                Role = 1,
                StoreId = 1
            };
            var result = await _userService.CreateUser(user);
            Assert.False(result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get Id</returns>
        [Theory]
        [InlineData(1)]
        public async Task GetById_WhenHaveUId(int id)
        {
            var user = await _userService.GetById(id);
            Assert.NotNull(user);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get Id</returns>
        [Theory]
        [InlineData(0)]
        public async Task GetById_WhenHaveNotId(int id)
        {
            var user = await _userService.GetById(id);
            Assert.Null(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get Id Password</returns>
        [Theory]
        [InlineData(0)]
        public async Task GetIdPassword_WhenHaveNotId(int id)
        {
            var user = await _userService.GetById(id);
            Assert.Null(user);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get Id Password</returns>
        [Theory]
        [InlineData(1)]
        public async Task GetIdPassword_WhenHaveId(int id)
        {
            var user = await _userService.GetById(id);
            Assert.NotNull(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update Password</returns>
        [Fact]
        public async Task UpdatePassword_ReturnTrue()
        {
            var user = new EditPasswordUserViewModel
            {
                Id = 1,
                NewPassword = "Khanh123",
                ConfirmPassword = "Khanh123"
            };
            var result = await _userService.EditPassword(user);
            Assert.True(result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Update Password</returns>
        [Fact]
        public async Task UpdatePassword_ReturnFalse()
        {
            var user = new EditPasswordUserViewModel
            {
                Id = 1,
                ConfirmPassword = "Khanh123"
            };
            var result = await _userService.EditPassword(user);
            Assert.False(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete User</returns>
        [Theory]
        [InlineData(1)]
        public async Task Delete_WhenHaveId(int id)
        {
            var user = await _userService.DeleteUser(id);
            Assert.True(user);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete User</returns>
        [Theory]
        [InlineData(0)]
        public async Task Delete_WhenHaveNotId(int id)
        {
            var user = await _userService.DeleteUser(id);
            Assert.False(user);
        }

        /// <summary>
        /// Check Email Exists
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        [Theory]
        [InlineData("khanh@gmail.com", 1)]
        public void IsExistsEmail(string email, int id)
        {
            var checkEmail = _userService.IsExistedEmail(email, id);
            Assert.False(checkEmail);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>UPdate User</returns>
        [Fact]
        public async Task UpdateUser_ReturnTrue()
        {
            var user = new EditUserViewModel
            {
                Id = 1,
                Email = "update@gmail.com",
                FullName = "update",
                Address = "25/23/131 update",
                Phone = 0123456789,
                Role = 2,
                StoreId = 1,
                IsActive = true
            };
            var result = await _userService.EditUser(user);
            Assert.True(result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>UPdate User</returns>
        [Fact]
        public async Task UpdateUser_ReturnFlase()
        {
            var user = new EditUserViewModel
            {
                FullName = "update",
                Address = "25/23/131 update",
                Phone = 0123456789,
                Role = 2,
                StoreId = 1,
                IsActive = true
            };
            var result = await _userService.EditUser(user);
            Assert.False(result);
        }

        /// <summary>
        /// Login False When Email Password IsNull
        /// </summary>
        [Fact]
        public void Login_WhenEmailPasswordIsNull()
        {
            var user = new AddUserViewModel
            {
                Email = "",
                PassWord = "",
                IsActive = true
            };
            var result = _userService.Login(user.Email, user.PassWord, user.IsActive);
            Assert.False(result);
        }
        
        [Fact]
        public void Login_WhenEmailPasswordNotEqualUserModel()
        {
            var user = new AddUserViewModel
            {
                Email = "khanh123@gmail.com",
                PassWord = "Khanh444",
                IsActive = true
            };
            var result = _userService.Login(user.Email, user.PassWord, user.IsActive);
            Assert.False(result);
        }
        
        [Fact]
        public void Login_WhenSuccess()
        {
            var user = new User
            {
                Email = "khanh@gmail.com",
                PassWord = SecurePasswordHasher.Hash("Khanh123"),
                IsActive = true
            };
            var result = _userService.Login(user.Email,user.PassWord,user.IsActive);
            Assert.True(result);
        }
        
    }
}