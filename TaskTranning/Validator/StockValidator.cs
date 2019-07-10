using FluentValidation;
using TaskTranning.Resources;
using TaskTranning.ViewModels;

namespace TaskTranning.Validator
{
    public class StockValidator : AbstractValidator<StockViewModel>
    {
        public StockValidator(ResourcesServices<StockResource> resourcesServices)
        {
            RuleFor(x => x.Quantity).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_QuantityNotNull"));
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage(resourcesServices.GetLocalizedHtmlString("msg_GreaterThan0"));
        }
    }
}