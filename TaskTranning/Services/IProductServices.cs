using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public interface IProductServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get Products</returns>
        IEnumerable<Product> GetProducts();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get List</returns>
        Task<List<ProductViewModel>> GetListAsync();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createProduct"></param>
        /// <returns>Create Product</returns>
        Task<bool> CreateProduct(ProductViewModel createProduct);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get By Id</returns>
        Task<ProductViewModel> GetById(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateProduct"></param>
        /// <returns>Update Product</returns>
        Task<bool> EditProduct(ProductViewModel updateProduct);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Product</returns>
        Task<bool> DeleteProduct(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateImage"></param>
        /// <returns>UPdate Image Product</returns>
        Task<bool> EditPicture(EditPictureProductViewModel updateImage);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get By Id Picture</returns>
        Task<EditPictureProductViewModel> GetByIdPicture(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>Is Existed Name</returns>
        bool IsExistedName(string name, int id);
    }
}