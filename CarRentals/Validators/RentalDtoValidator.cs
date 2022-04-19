﻿using CarRentals.DTOs;
using CarRentals.Models;
using FluentValidation;

namespace CarRentals.Validators
{
    public class RentalDtoValidator : AbstractValidator<RentalDto>
    {
        public RentalDtoValidator()
        {
            RuleForEach<CarDto>(rental => rental.RentedCars)
                .Must(car => car.State != CarState.Damaged)
                .WithMessage("Car is damaged.");

            RuleFor(r => r.RentStartTime).LessThan(r => r.RentEndTime);

            RuleFor(r => r.RentedCars).NotEmpty();

            RuleFor(r => r.Client).NotEmpty();
        }
    }
}