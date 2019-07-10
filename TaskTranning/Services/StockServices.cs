using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public class StockServices : IStockServices
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
        public StockServices(CodeFirstDataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns>GetListAsync</returns>
        public async Task<List<StockViewModel>> GetListAsync()
        {
            var stocks = await _context.Stock.Include(s => s.Product).Include(s => s.Store).ToListAsync();
            var viewModel = _mapper.Map<List<StockViewModel>>(stocks);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="createStock"></param>
        /// <returns></returns>
        public async Task<bool> CreateStock(StockViewModel createStock)
        {
            try
            {
                var checkStock = await _context.Stock.FindAsync(createStock.ProductId , createStock.StoreId);
                if (checkStock != null)
                {
                    checkStock.Quantity += createStock.Quantity;
                    _context.Stock.Update(checkStock);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var stock = new Stock
                    {
                        ProductId = createStock.ProductId,
                        StoreId = createStock.StoreId,
                        Quantity = createStock.Quantity
                    };
                    _context.Stock.Add(stock);
                    await _context.SaveChangesAsync();
                }
            
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return false;
            }
            
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <returns>GetById</returns>
        public async Task<StockViewModel> GetById(int productId, int storeId)
        {
            var stock = await _context.Stock.FindAsync(productId , storeId);
            var viewModel = _mapper.Map<StockViewModel>(stock);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="updateStock"></param>
        /// <returns>EditStock</returns>
        public async Task<bool> EditStock(StockViewModel updateStock)
        {
            try
            {
                var stock = await _context.Stock.FindAsync(updateStock.ProductId, updateStock.StoreId);
                stock.Quantity = updateStock.Quantity;
                _context.Stock.Update(stock);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return false;
            }
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteStock(int productId, int storeId)
        {
            try
            {
                var stock = await _context.Stock.FindAsync(productId, storeId);
                _context.Remove(stock);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return false;
            } 
        }
    }
    
}