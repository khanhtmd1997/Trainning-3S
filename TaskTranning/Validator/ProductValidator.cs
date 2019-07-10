using FluentValidation;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;

namespace TaskTranning.Validator
{
    public class ProductValidator : AbstractValidator<ProductViewModel>
    {
        public ProductValidator(ResourcesServices<ProductResource> resourcesServices, IProductServices productServices)
        {
            RuleFor(x => x.ProductName).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_ProductNameNotNull"));
            RuleFor(x => x.ListPrice).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_ListPriceNotNull"));
            RuleFor(x => x.ModelYear).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_ModelYearNotNull"));
            RuleFor(x => x.ProductName).Must((reg, c) => !productServices.IsExistedName(reg.ProductName, reg.Id))
                .WithMessage((reg, c) => string.Format(resourcesServices.GetLocalizedHtmlString("msg_AlreadyExists"), c)); 
        }
    }
}