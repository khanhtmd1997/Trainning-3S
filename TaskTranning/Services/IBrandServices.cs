using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public interface IBrandServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get Brands</returns>
        IEnumerable<Brand> GetBrands();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get List Brand</returns>
        Task<List<BrandViewModel>> GetListAsync();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createBrand"></param>
        /// <returns>Create Brand</returns>
        Task<bool> CreateBrand(BrandViewModel createBrand);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get By Id</returns>
        Task<BrandViewModel> GetById(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateBrand"></param>
        /// <returns>Update Brand</returns>
        Task<bool> EditBrand(BrandViewModel updateBrand);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Brand</returns>
        Task<bool> DeleteBrand(int id);
        
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