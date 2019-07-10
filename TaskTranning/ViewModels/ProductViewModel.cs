using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TaskTranning.Models;

namespace TaskTranning.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        
        public string ProductName { get; set; }
        
        public int BrandId { get; set; }
        
        public int CategoryId { get; set; }
        
        public int ModelYear { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal ListPrice { get; set; }
        
        public string Picture { get; set; }
        
        public IFormFile PictureFile { get; set; }
        
        public virtual Category Category { get; set; }
        
        public virtual Brand Brand { get; set; }
    }
}