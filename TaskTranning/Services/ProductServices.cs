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
    public class ProductServices : IProductServices
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
        public ProductServices(CodeFirstDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>GetProducts</returns>
        public IEnumerable<Product> GetProducts()
        {
            return _context.Product;
        } 
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns>GetListAsync</returns>
        public async Task<List<ProductViewModel>> GetListAsync()
        {
            var products = await _context.Product.Include(p => p.Brand).Include(p => p.Category).ToListAsync();
            var viewModel = _mapper.Map<List<ProductViewModel>>(products);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="createProduct"></param>
        /// <returns>CreateProduct</returns>
        public async Task<bool> CreateProduct(ProductViewModel createProduct)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",   createProduct.PictureFile.FileName);  
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    await createProduct.PictureFile.CopyToAsync(stream);  
                }
                var product = new Product
                {
                    ProductName = createProduct.ProductName,
                    BrandId = createProduct.BrandId,
                    CategoryId = createProduct.CategoryId,
                    ModelYear = createProduct.ModelYear,
                    ListPrice = createProduct.ListPrice,
                    Picture = createProduct.PictureFile.FileName
                };
                _context.Product.Add(product);
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
        public  async Task<ProductViewModel> GetById(int id)
        {
            var product = await _context.Product.FindAsync(id);
            var viewModel = _mapper.Map<ProductViewModel>(product);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GetByIdPicture</returns>
        public  async Task<EditPictureProductViewModel> GetByIdPicture(int id)
        {
            var product = await _context.Product.FindAsync(id);
            var viewModel = _mapper.Map<EditPictureProductViewModel>(product);
            return viewModel;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="updateImage"></param>
        /// <returns>EditPicture</returns>
        public async Task<bool> EditPicture(EditPictureProductViewModel updateImage)
        {
            try
            {
                var product = await _context.Product.FindAsync(updateImage.Id);
                product.Picture = updateImage.PictureFile.FileName;
                _context.Product.Update(product);
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
        /// <param name="updateProduct"></param>
        /// <returns>EditProduct</returns>
        public async Task<bool> EditProduct(ProductViewModel updateProduct)
        {
            try
            {
                var product = await _context.Product.FindAsync(updateProduct.Id);
                product.ProductName = updateProduct.ProductName;
                product.BrandId = updateProduct.BrandId;
                product.ModelYear = updateProduct.ModelYear;
                product.ListPrice = updateProduct.ListPrice;
                _context.Product.Update(product);
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
        /// <returns>DeleteProduct</returns>
        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var productId = await _context.Product.FindAsync(id);
                _context.Remove(productId);
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
            return _context.Product.Any(x => x.ProductName == name && x.Id != id);
        }

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
                            var product = new ProductViewModel
                            {
                                    ProductName = worksheet.Cells[row,1].Value.ToString().Trim(),
                                    BrandId = int.Parse(worksheet.Cells[row,2].Value.ToString().Trim()),
                                    CategoryId = int.Parse(worksheet.Cells[row,3].Value.ToString().Trim()),
                                    ModelYear = int.Parse(worksheet.Cells[row,4].Value.ToString().Trim()),
                                    ListPrice = int.Parse(worksheet.Cells[row,5].Value.ToString().Trim())
                            };
                            if (_context.Product.Any(x => x.ProductName == product.ProductName))
                            {
                                continue;
                            }
                            await CreateProduct(product);
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
    }
}