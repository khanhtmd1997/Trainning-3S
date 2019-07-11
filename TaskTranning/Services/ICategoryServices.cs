using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public interface ICategoryServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get Categories</returns>
        IEnumerable<Category> GetCategories();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get List</returns>
        Task<List<CategoryViewModel>> GetListAsync();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createCategory"></param>
        /// <returns>Create Category</returns>
        Task<bool> CreateCategory(CategoryViewModel createCategory);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get By Id</returns>
        Task<CategoryViewModel> GetById(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateCategory"></param>
        /// <returns>Update Category</returns>
        Task<bool> EditCategory(CategoryViewModel updateCategory);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Category</returns>
        Task<bool> DeleteCategory(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>Is Existed Name</returns>
        bool IsExistedName(string name, int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<bool> ImporTask(IFormFile formFile);
    }
}