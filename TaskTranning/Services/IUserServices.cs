using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public interface IUserServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="isActive"></param>
        /// <returns>Login</returns>
        bool Login(string email, string password, bool isActive);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Get Email</returns>
        Task<AddUserViewModel> GetEmail(string email);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get List</returns>
        Task<List<AddUserViewModel>> GetListAsync();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns>Create User</returns>
        Task<bool> CreateUser(AddUserViewModel createUser);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns>Update User</returns>
        Task<bool> EditUser(EditUserViewModel updateUser);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete User</returns>
        Task<bool> DeleteUser(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get By Id</returns>
        Task<EditUserViewModel> GetById(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get By Id Password</returns>
        Task<EditPasswordUserViewModel> GetByIdPassword(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatePassword"></param>
        /// <returns>Update Password</returns>
        Task<bool> EditPassword(EditPasswordUserViewModel updatePassword);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns>Is Existed Email</returns>
        bool IsExistedEmail(string email, int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get Users</returns>
        IEnumerable<User> GetUsers();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get By Id Picture</returns>
        Task<EditPictureUserViewModel> GetByIdPicture(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateImage"></param>
        /// <returns>UPdate Image User</returns>
        Task<bool> EditPicture(EditPictureUserViewModel updateImage);
    }
}