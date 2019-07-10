using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TaskTranning.Models;

namespace TaskTranning.ViewModels
{
    public class AddUserViewModel
    {   
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Email")]
        public string Email{ get; set; }

        [Display(Name = "Password")]
        public string PassWord { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Phone")]
        public int Phone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }
 
        [Display(Name = "Picture")]
        public string Picture { get; set; }
        
        [Display(Name = "Picture File")]
        public IFormFile PictureFile { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "Store Name")]
        public int StoreId { get; set; }
        
        [Display(Name = "Role Name")]
        public int Role { get; set; }
        
//        public enum RoleName
//        {
//            Admin = 1,
//            User = 2
//        }
        
        public virtual Store Store { get; set; }
    }
}