using CarRentals.Models;
using FluentValidation;

namespace CarRentals.Validators
{
    public class CarValidator : AbstractValidator<Car>
    {
        private readonly string _licenseRegexPattern = "[A-Z]{3}-[0-9]{3}|[A-Z]{2}-[0-9]{3}-[A-Z]{2}";
        public CarValidator()
        {
            RuleFor(x => x.Brand)
                .NotEmpty();
            RuleFor(x => x.Model)
                .NotEmpty();
            RuleFor(x => x.LicensePlate)
                .NotEmpty()
                .Matches(_licenseRegexPattern);
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Type)
                .NotEmpty();
        }
    }
}
