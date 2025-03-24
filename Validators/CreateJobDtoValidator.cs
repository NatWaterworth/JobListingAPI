using FluentValidation;
using JobListingAPI.DTOs;

namespace JobListingAPI.Validators
{
    public class CreateJobDtoValidator : AbstractValidator<CreateJobDto>
    {
        public CreateJobDtoValidator()
        {
            RuleFor(j => j.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must be 100 characters or less.");

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
