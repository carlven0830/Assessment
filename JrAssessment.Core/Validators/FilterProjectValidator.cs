using FluentValidation;
using JrAssessment.Model.Requests;

namespace JrAssessment.Core.Validators
{
    public class FilterProjectValidator : AbstractValidator<FilterProjectRequest>
    {
        public FilterProjectValidator()
        {
            RuleFor(x => x.IsPageList)
                .Must(x => x == true || x == false)
                .WithMessage("Is page list must be true or false.");

            When(x => x.IsPageList, () =>
            {
                RuleFor(x => x.PageNum)
                    .NotEmpty().WithMessage("Page number is required.");

                RuleFor(x => x.PageSize)
                    .NotEmpty().WithMessage("Page size is required.");
            });
        }
    }
}
