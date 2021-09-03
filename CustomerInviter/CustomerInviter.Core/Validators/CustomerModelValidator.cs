using CustomerInviter.Core.Models;
using FluentValidation;

namespace CustomerInviter.Core.Validators
{
    public class CustomerModelValidator : AbstractValidator<CustomerModel>
    {
        public CustomerModelValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Customer must have a name");
            RuleFor(c => c.Latitude).NotNull().NotEmpty().WithMessage("Must provide a latitude");
            RuleFor(c => c.Latitude).Must(r => double.TryParse(r, out var result)).WithMessage("Latitude must have a valid value");
            RuleFor(c => c.Longitude).NotNull().NotEmpty().WithMessage("Must provide a longitude");
            RuleFor(c => c.Longitude).Must(r => double.TryParse(r, out var result)).WithMessage("Longitude must have a valid value");
        }
    }
}