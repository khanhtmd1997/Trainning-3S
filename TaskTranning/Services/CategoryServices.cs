using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using TaskTranning.Models;
using TaskTranning.ViewModels;

namespace TaskTranning.Services
{
    public class CategoryServices : ICategoryServices
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
        public CategoryServices(CodeFirstDataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns>GetCategories</returns>
        public IEnumerable<Category> GetCategories()
        {
            return _context.Category;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>GetListAsync</returns>
        public async Task<List<CategoryViewModel>> GetListAsync()
        {
            var categgories = await _context.Category.ToListAsync();
            var viewModel = _mapper.Map<List<CategoryViewModel>>(categgories);
            return viewModel;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="createCategory"></param>
        /// <returns>CreateCategory</returns>
        public async Task<bool> CreateCategory(CategoryViewModel createCategory)
        {
            try
            {
                var category = new Category()
                {
                    CategoryName = createCategory.CategoryName
                };
                _context.Category.Add(category);
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
        public async Task<CategoryViewModel> GetById(int id)
        {
            var category = await _context.Category.FindAsync(id);
            var viewModel = _mapper.Map<CategoryViewModel>(category);
            return viewModel;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="updateCategory"></param>
        /// <returns></returns>
        public async Task<bool> EditCategory(CategoryViewModel updateCategory)
        {
            try
            {
                var category = await _context.Category.FindAsync(updateCategory.Id);
                category.CategoryName = updateCategory.CategoryName;
                _context.Category.Update(category);
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
        /// <returns>DeleteCategory</returns>
        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                var categoryId = await _context.Category.FindAsync(id);
                _context.Remove(categoryId);
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
            return _context.Category.Any(x => x.CategoryName == name && x.Id != id);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<bool> ImporTask(IFormFile formFile)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    await formFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            
                            var category = new CategoryViewModel
                            {
                                //Id = int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()),
                                CategoryName = worksheet.Cells[row, 1].Value.ToString().Trim()
                            };
                            if (_context.Category.Any(x => x.CategoryName == category.CategoryName))
                            {
                                continue;
                            }
                            await CreateCategory(category);
                        }
                    }
                }
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