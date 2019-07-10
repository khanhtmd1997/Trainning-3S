using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public interface IStockServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Get List</returns>
        Task<List<StockViewModel>> GetListAsync();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="createStock"></param>
        /// <returns>Create Stock</returns>
        Task<bool> CreateStock(StockViewModel createStock);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <returns>Get By Id</returns>
        Task<StockViewModel> GetById(int productId, int storeId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateStock"></param>
        /// <returns>Update Stock</returns>
        Task<bool> EditStock(StockViewModel updateStock);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <returns>Delete Stock</returns>
        Task<bool> DeleteStock(int productId, int storeId);
    }
}