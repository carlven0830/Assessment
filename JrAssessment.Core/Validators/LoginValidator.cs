using FluentValidation;
using JrAssessment.Model.Requests;

namespace JrAssessment.Core.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
               .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password is required.");
        }
    }
}
