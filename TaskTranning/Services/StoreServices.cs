using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public class StoreServices : IStoreServices
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
        public StoreServices(CodeFirstDataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>GetStores</returns>
        public IEnumerable<Store> GetStores()
        {
            return _context.Store;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns>GetListAsync</returns>
        public async Task<List<StoreViewModel>> GetListAsync()
        {
            var stores = await _context.Store.ToListAsync();
            var viewModel = _mapper.Map<List<StoreViewModel>>(stores);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="createStore"></param>
        /// <returns>CreateStore</returns>
        public async Task<bool> CreateStore(StoreViewModel createStore)
        {
            try
            {
                var store = new Store
                {
                    StoreName = createStore.StoreName,
                    Phone = createStore.Phone,
                    Email = createStore.Email,
                    Street = createStore.Street,
                    City = createStore.City,
                    State = createStore.State,
                    ZipCode = createStore.ZipCode
                };
                _context.Store.Add(store);
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
        /// <param name="id"></param>
        /// <returns>GetById</returns>
        public async Task<StoreViewModel> GetById(int id)
        {
            var store = await _context.Store.FindAsync(id);
            var viewModel = _mapper.Map<StoreViewModel>(store);
            return viewModel;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="updateStore"></param>
        /// <returns>EditStore</returns>
        public async Task<bool> EditStore(StoreViewModel updateStore)
        {
            try
            {
                var store = await _context.Store.FindAsync(updateStore.Id);
                store.Phone = updateStore.Phone;
                store.StoreName = updateStore.StoreName;
                store.Street = updateStore.Street;
                store.City = updateStore.City;
                store.State = updateStore.State;
                store.ZipCode = updateStore.ZipCode;
                _context.Store.Update(store);
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
        /// <param name="id"></param>
        /// <returns>DeleteStore</returns>
        public async Task<bool> DeleteStore(int id)
        {
            try
            {
                var store = await _context.Store.FindAsync(id);
                _context.Remove(store);
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
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>IsExistedName</returns>
        public bool IsExistedName(string name, int id)
        {
            return _context.Store.Any(x => x.StoreName == name && x.Id != id);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns>IsExistedEmail</returns>
        public bool IsExistedEmail(string email, int id)
        {
            return _context.Store.Any(x => x.Email == email && x.Id != id);
        }
    }
}