using Microsoft.AspNetCore.Http;

namespace TaskTranning.ViewModels
{
    public class EditPictureProductViewModel
    {
        public int Id { get; set; }
        
        public IFormFile PictureFile { get; set; }
    }
}