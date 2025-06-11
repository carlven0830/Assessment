using FluentValidation;
using JrAssessment.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrAssessment.Core.Validators
{
    public class EmployeeValidator : AbstractValidator<AddEmpRequest>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must be contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
                .Matches(@"[\#\?\!\@\$\%\^\&\*\-]").WithMessage("Password must contain at least one special character ( #, ?, !, @, $, %, ^, &, *, - ).");

            RuleFor(x => x.ConfirmedPassword)
                 .NotEmpty().WithMessage("Confirmed password is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.");

            RuleFor(x => x.EmpName)
                .NotEmpty().WithMessage("Employee name is required.");

            RuleFor(x => x.EmpPosition)
                .IsInEnum().WithMessage("Invalid position.");

            RuleFor(x => x.EmpLevel)
                .IsInEnum().WithMessage("Invalid employee level.");
        }
    }
}
