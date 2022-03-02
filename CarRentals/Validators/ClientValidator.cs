﻿using CarRentals.Models;
using FluentValidation;

namespace CarRentals.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        private int _minDniValue = 1000000;
        private int _maxDniValue = 99999999;

        public ClientValidator()
        {
            RuleFor(client => client.Name).NotEmpty();
            RuleFor(client => client.Address).NotEmpty();
            RuleFor(client => client.Surname).NotEmpty();
            RuleFor(client => client.DNI)
                .ExclusiveBetween(_minDniValue, _maxDniValue);
        }


    }
}
