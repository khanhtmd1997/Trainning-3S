using System.Collections.Generic;

namespace TaskTranning.Models
{
    public class Brand
    {
        public int Id { get; set; }
        
        public string BrandName { get; set; }
        
        public ICollection<Product> Product { get; set; }
    }
}