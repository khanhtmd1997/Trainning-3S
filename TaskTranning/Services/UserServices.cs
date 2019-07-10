using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public class UserServices : IUserServices
    {
        /// <summary>
        /// declare datacontext
        /// </summary>
        private readonly CodeFirstDataContext _context;

        /// <summary>
        /// declare mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public UserServices(CodeFirstDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="isActive"></param>
        /// <returns>Login</returns>
        public  bool Login(string email, string password, bool isActive)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            var user = _context.User.FirstOrDefault(x => x.Email == email 
                                                         && Infrastructure.SecurePasswordHasher.Verify(password,x.PassWord));
            return user != null && user.IsActive;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="email"></param>
        /// <returns>GetEmail</returns>
        public async Task<AddUserViewModel> GetEmail(string email)
        {
            var emailUser = await _context.User.FirstOrDefaultAsync(x => x.Email == email);
            var getEmail = _mapper.Map<AddUserViewModel>(emailUser);
            return getEmail;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.User;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns>GetListAsync</returns>
        public async Task<List<AddUserViewModel>> GetListAsync()
        {
            var users = await _context.User.Include(u => u.Store).ToListAsync();
            var viewModel = _mapper.Map<List<AddUserViewModel>>(users);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns>CreateUser</returns>
        public async Task<bool> CreateUser(AddUserViewModel createUser)
        {
            try
            {
                var user = new User
                {
                    Email = createUser.Email,
                    PassWord = Infrastructure.SecurePasswordHasher.Hash(createUser.PassWord),
                    FullName = createUser.FullName,
                    Phone = createUser.Phone,
                    Address = createUser.Address,
                    IsActive = createUser.IsActive,
                    StoreId = createUser.StoreId,
                    Role = createUser.Role
                };
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Console.Write(e);
                return false;
            }
            
        } 

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GetById</returns>
        public  async Task<EditUserViewModel> GetById(int id)
        {
            var user = await _context.User.FindAsync(id);
            var viewModel =_mapper.Map<EditUserViewModel>(user);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GetByIdPassword</returns>
        public  async Task<EditPasswordUserViewModel> GetByIdPassword(int id)
        {
            var user = await _context.User.FindAsync(id);
            var viewModel = _mapper.Map<EditPasswordUserViewModel>(user);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns>Edit User</returns>
        public async Task<bool> EditUser(EditUserViewModel updateUser)
        {
            try
            {
                var user = await _context.User.FindAsync(updateUser.Id);
                user.Email = updateUser.Email;
                user.FullName = updateUser.FullName;
                user.Phone = updateUser.Phone;
                //user.Picture = updateUser.PictureFile.FileName;
                user.Address = updateUser.Address;
                user.IsActive = updateUser.IsActive;
                user.StoreId = updateUser.StoreId;
                user.Role = updateUser.Role;
//                if (updateUser.PictureFile != null)
//                {
//                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",   updateUser.PictureFile.FileName);  
//                    using (var stream = new FileStream(path, FileMode.Create))  
//                    {  
//                        await updateUser.PictureFile.CopyToAsync(stream);  
//                    }
//                    user.Picture = updateUser.PictureFile.FileName;
//                }
                _context.User.Update(user);
                await _context.SaveChangesAsync();                        
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Console.Write(e);
                return false;
            }
            
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="updatePassword"></param>
        /// <returns>Edit Password</returns>
        public async Task<bool> EditPassword(EditPasswordUserViewModel updatePassword)
        {
            try
            {
                var user = await _context.User.FindAsync(updatePassword.Id);
                user.PassWord = Infrastructure.SecurePasswordHasher.Hash(updatePassword.NewPassword);
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Console.Write(e);
                return false;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete User</returns>
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await _context.User.FindAsync(id);
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Console.Write(e);
                return false;
            }            
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns>IsExistedEmail</returns>
        public bool IsExistedEmail(string email, int id)
        {
            return _context.User.Any(x => x.Email == email && x.Id != id);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GetByIdPicture</returns>
        public  async Task<EditPictureUserViewModel> GetByIdPicture(int id)
        {
            var user = await _context.User.FindAsync(id);
            var viewModel = _mapper.Map<EditPictureUserViewModel>(user);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="updateImage"></param>
        /// <returns>EditPicture</returns>
        public async Task<bool> EditPicture(EditPictureUserViewModel updateImage)
        {
            try
            {
                var user = await _context.User.FindAsync(updateImage.Id);
                user.Picture = updateImage.PictureFile.FileName;
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Console.Write(e);
                return false;
            }
        }
    }
}