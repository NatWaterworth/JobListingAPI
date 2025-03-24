using FluentValidation;
using JobListingAPI.DTOs;

namespace JobListingAPI.Validators
{
    public class UpdateJobDtoValidator : AbstractValidator<UpdateJobDto>
    {
        public UpdateJobDtoValidator()
        {
            RuleFor(j => j.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100);

            RuleFor(j => j.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000);

            RuleFor(j => j.Company)
                .NotEmpty().WithMessage("Company is required.");

            RuleFor(j => j.Salary)
                .GreaterThan(0).WithMessage("Salary must be a positive number.");
        }
    }
}
