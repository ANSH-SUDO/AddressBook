﻿using FluentValidation;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators
{
    public class AddressBookEntryDTOValidator
    {
        public class AddressBookEntryDtoValidator : AbstractValidator<AddressBookEntryDTO>
        {
            public AddressBookEntryDtoValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Invalid password format");
            }
        }
    }
}
