using AspProjekat.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.Validators
{
    public class UpdateHallDtoValidator:AbstractValidator<UpdateHallDto>
    {
        public UpdateHallDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be bigger than 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
               .MinimumLength(3).WithMessage("Name must have at least 3 chars")
               .MaximumLength(40).WithMessage("Name must have maximum of 40 chars");
            RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0");
        }
    }
}
