using FluentValidation;
using TaskTranning.Resources;
using TaskTranning.ViewModels;

namespace TaskTranning.Validator
{
    public class LoginVadidator : AbstractValidator<LoginViewModel>
    {
        public LoginVadidator(ResourcesServices<LoginResource> resourcesServices)
        {
            RuleFor(x => x.Email).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_EmailNotNull"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_FormatEmail"));
            RuleFor(x => x.PassWord).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_PasswordNotNull"));
            RuleFor(x => x.PassWord).Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").
                WithMessage(resourcesServices.GetLocalizedHtmlString("msg_LetterAndNumber"));
            RuleFor(x => x.PassWord).MinimumLength(6).WithMessage(resourcesServices.GetLocalizedHtmlString("msg_Min6Characters"));
        }
        
    }
}