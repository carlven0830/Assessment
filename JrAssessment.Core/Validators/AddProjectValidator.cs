using FluentValidation;
using JrAssessment.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Core.Validation
{
    public class AddProjectValidator : AbstractValidator<AddProjectRequest>
    {
        public AddProjectValidator()
        {
            RuleFor(x => x.ProjectTitle)
                .NotEmpty().WithMessage("Project title is required.");

            RuleFor(x => x.ProjectDescription)
                .NotEmpty().WithMessage("Project description is required.");
        }
    }
}
