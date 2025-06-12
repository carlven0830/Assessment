using FluentValidation;
using JrAssessment.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Core.Validators
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
    {
        public UpdateProjectValidator()
        {
            RuleFor(x => x.ProjectTitle)
                .NotEmpty().WithMessage("Project title is required.");

            RuleFor(x => x.ProjectDescription)
                .NotEmpty().WithMessage("Project description is required.");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status.");
        }
    }
}
