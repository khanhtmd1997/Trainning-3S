using System.ComponentModel.DataAnnotations;

namespace TaskTranning.ViewModels
{
    public class BrandViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}