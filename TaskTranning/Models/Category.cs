using System.Collections.Generic;

namespace TaskTranning.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        public string CategoryName { get; set; }
        
        public virtual ICollection<Product> Product { get; set; }
    }
}