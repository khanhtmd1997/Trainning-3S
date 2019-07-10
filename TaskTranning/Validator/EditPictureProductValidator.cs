using FluentValidation;
using TaskTranning.Resources;
using TaskTranning.ViewModels;

namespace TaskTranning.Validator
{
    public class EditPictureProductValidator : AbstractValidator<EditPictureProductViewModel>
    {
        public EditPictureProductValidator(ResourcesServices<ProductResource> resourcesServices)
        {
            RuleFor(x => x.PictureFile).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_PictureFileNotNull"));
        }
    }
}