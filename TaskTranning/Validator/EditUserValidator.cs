using FluentValidation;
using TaskTranning.Resources;
using TaskTranning.Services;
using TaskTranning.ViewModels;

namespace TaskTranning.Validator
{
    public class EditUserValidator : AbstractValidator<EditUserViewModel>
    {
        public EditUserValidator(ResourcesServices<UserResource> resourcesServices, IUserServices userServices)
        {
            RuleFor(x => x.Email).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_EmailNotNull"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_FormatEmail"));
            RuleFor(x => x.FullName).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_FullNameNotNull"));
            RuleFor(x => x.FullName).Length(1,100).WithMessage(resourcesServices.GetLocalizedHtmlString("msg_ToOneFromOneHundred"));
            RuleFor(x => x.Phone).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_PhoneNotNull"));
            RuleFor(x => x.IsActive).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_IsActiveNotNull"));
            RuleFor(x => x.Address).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_AddressNotNull"));
            RuleFor(x => x.Email).Must((reg, c) => !userServices.IsExistedEmail(reg.Email, reg.Id))
                .WithMessage((reg, c) => string.Format(resourcesServices.GetLocalizedHtmlString("msg_AlreadyExists"), c)); 
        }
    }
}