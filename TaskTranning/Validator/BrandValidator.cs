using FluentValidation;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;

namespace TaskTranning.Validator
{
    public class BrandValidator : AbstractValidator<BrandViewModel>
    {
        public BrandValidator(ResourcesServices<BrandResource> resourcesServices, IBrandServices brandServices)
        {
            RuleFor(x => x.BrandName).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_BrandNameNotNull"));
            // ((reg, c) => string.Format(resourcesServices.GetLocalizedHtmlString("msg_NotNull"), reg.BrandName));
            RuleFor(x => x.BrandName).Must((reg, c) => !brandServices.IsExistedName(reg.BrandName, reg.Id))
                .WithMessage((reg, c) => string.Format(resourcesServices.GetLocalizedHtmlString("msg_AlreadyExists"),c));
                    //((reg, c) => string.Format(resourcesServices.GetLocalizedHtmlString("msg_AlreadyExists"), c)); 
        }
    }
}