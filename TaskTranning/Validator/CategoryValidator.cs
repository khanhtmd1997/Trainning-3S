using FluentValidation;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;

namespace TaskTranning.Validator
{
    public class CategoryValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryValidator(ResourcesServices<CategoryResource> resourcesServices, ICategoryServices categoryServices)
        {
            RuleFor(x => x.CategoryName).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_CategoryNameNotNull"));
            RuleFor(x => x.CategoryName).Must((reg, c) => !categoryServices.IsExistedName(reg.CategoryName, reg.Id))
                .WithMessage((reg, c) => string.Format(resourcesServices.GetLocalizedHtmlString("msg_AlreadyExists"), c));  
        }
    }
}