using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public interface IStoreServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get List</returns>
        Task<List<StoreViewModel>> GetListAsync();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createStore"></param>
        /// <returns>Create Store</returns>
        Task<bool> CreateStore(StoreViewModel createStore);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get By Id</returns>
        Task<StoreViewModel> GetById(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateStore"></param>
        /// <returns>Update Store</returns>
        Task<bool> EditStore(StoreViewModel updateStore);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Store</returns>
        Task<bool> DeleteStore(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get Stores</returns>
        IEnumerable<Store> GetStores();
        
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
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns>Is Existed Email</returns>
        bool IsExistedEmail(string email, int id);
    }
}