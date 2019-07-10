using System.Collections.Generic;

namespace TaskTranning.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string ProductName { get; set; }       

        public int BrandId { get; set; }       

        public int CategoryId { get; set; }        

        public int ModelYear { get; set; }      

        public decimal ListPrice { get; set; }       
 
        public string Picture { get; set; }
        
        public virtual Category Category { get; set; }
        
        public virtual ICollection<Stock> Stock { get; set; }
        
        public virtual Brand Brand { get; set; }
    }
}