using CarRentals.DTOs;
using FluentValidation;

namespace CarRentals.Validators
{
    public class CarDtoValidator : AbstractValidator<CarDto>
    {
        private readonly string _licenseRegexPattern = "[A-Z]{3}-[0-9]{3}|[A-Z]{2}-[0-9]{3}-[A-Z]{2}";
        public CarDtoValidator()
        {
            RuleFor(x => x.Brand)
                .NotEmpty()
                .MinimumLength(2);
            RuleFor(x => x.Model)
                .NotEmpty();
            RuleFor(x => x.LicensePlate)
                .NotEmpty()
                .Matches(_licenseRegexPattern);
            RuleFor(x => x.Type)
                .NotEmpty();
        }
    }
}
