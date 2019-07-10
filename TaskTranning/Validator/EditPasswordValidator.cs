using FluentValidation;
using TaskTranning.Resources;
using TaskTranning.ViewModels;

namespace TaskTranning.Validator
{
    public class EditPasswordValidator : AbstractValidator<EditPasswordUserViewModel>
    {
        public EditPasswordValidator(ResourcesServices<UserResource> resourcesServices)
        {
            RuleFor(x => x.NewPassword).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_NewPasswordNotNull"));
            RuleFor(x => x.NewPassword).Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").
                WithMessage(resourcesServices.GetLocalizedHtmlString("msg_LetterAndNumber"));
            RuleFor(x => x.NewPassword).MinimumLength(6).
                WithMessage(resourcesServices.GetLocalizedHtmlString("msg_Min6Characters"));
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage(resourcesServices.GetLocalizedHtmlString("msg_ConfirmPasswordNotNull"));
            RuleFor(x => x.ConfirmPassword).Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").
                WithMessage(resourcesServices.GetLocalizedHtmlString("msg_LetterAndNumber"));
            RuleFor(x => x.NewPassword).MinimumLength(6).WithMessage(resourcesServices.GetLocalizedHtmlString("msg_Min6Characters"));
            RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword)
                .WithMessage(resourcesServices.GetLocalizedHtmlString("msg_EqualPassword"));
        }
    }
}