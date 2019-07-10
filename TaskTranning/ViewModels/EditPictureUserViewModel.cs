using Microsoft.AspNetCore.Http;

namespace TaskTranning.ViewModels
{
    public class EditPictureUserViewModel
    {
        public int Id { get; set; }
        
        public IFormFile PictureFile { get; set; }
    }
}