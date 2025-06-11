using FluentValidation;
using JrAssessment.Model.Requests;

namespace JrAssessment.Core.Validators
{
    public class JwtTokenValidator : AbstractValidator<JwtTokenRequest>
    {
        public JwtTokenValidator()
        {
            RuleFor(x => x.AccountId)
                 .NotEmpty().WithMessage("Account Id is required.");

            RuleFor(x => x.SessionKey)
                .NotEmpty().WithMessage("Session key is required.");

            RuleFor(x => x.Action)
                .NotEmpty().WithMessage("Action is required.");
        }
    }
}
