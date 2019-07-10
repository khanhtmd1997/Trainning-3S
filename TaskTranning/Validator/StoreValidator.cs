using FluentValidation;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;

namespace TaskTranning.Validator
{
    public class StoreValidator : AbstractValidator<StoreViewModel>
    {
        public StoreValidator(ResourcesServices<StoreResource> resourcesServices,IStoreServices storeServices,IUserServices userServices)
        {
            RuleFor(x => x.StoreName).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_StoreNameNotNull"));
            RuleFor(x => x.Phone).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_PhoneNotNull"));
            RuleFor(x => x.Email).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_EmailNotNull"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_FormatEmail"));
            RuleFor(x => x.Street).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_StreetNotNull"));
            RuleFor(x => x.City).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_CityNotNull"));
            RuleFor(x => x.ZipCode).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_ZipCodeNotNull"));
            RuleFor(x => x.StoreName).Must((reg, c) => !storeServices.IsExistedName(reg.StoreName, reg.Id))
                .WithMessage((reg, c) => string.Format(resourcesServices.GetLocalizedHtmlString("msg_AlreadyExists"), c)); 
            RuleFor(x => x.Email).Must((reg, c) => !storeServices.IsExistedEmail(reg.Email, reg.Id))
                .WithMessage((reg, c) => string.Format(resourcesServices.GetLocalizedHtmlString("msg_AlreadyExists"), c)); 
            RuleFor(x => x.Email).Must((reg, c) => !userServices.IsExistedEmail(reg.Email, reg.Id))
                .WithMessage((reg, c) => string.Format(resourcesServices.GetLocalizedHtmlString("msg_AlreadyExists"), c)); 
               
        }
    }
}