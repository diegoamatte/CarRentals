﻿using FluentValidation;

namespace CarRentals.Models
{
    public class Car
    {
        public Guid? Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
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
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.LicensePlate).NotEmpty();
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
