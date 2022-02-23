using FluentValidation;

namespace CarRentals.Models
{
    public class Car
    {
        /// <example>49ab41c6-7110-4795-a88e-1e452f10b61f</example>
        public Guid? Id { get; set; }
        /// <example>ABC 123 CD</example>
        public string LicensePlate { get; set; }
        /// <example>Honda</example>
        public string Brand { get; set; }
        /// <example>CR-V</example>
        public string Model { get; set; }
        /// <example>SUV</example>
        public string Type { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null)
                return false;
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return LicensePlate == ((Car)obj).LicensePlate;
        }
    }

    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {           
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.LicensePlate)
                .NotEmpty()
                .MinimumLength(6);
            RuleFor(x => x.Model)
                .NotEmpty();
            RuleFor(x => x.Type)
                .NotEmpty()
                .MinimumLength(3);
        }
    }
}
