using AspProjekat.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.Validators
{
    public class CreateProjectionTypeDtoValidator:AbstractValidator<CreateProjectionTypeDto>
    {
        public CreateProjectionTypeDtoValidator() 
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required")
                .MaximumLength(30).WithMessage("Name has max of 30 chars")
                .MinimumLength(2).WithMessage("Name has min of 2 chars");
            RuleFor(x => x.Multiplier).NotEmpty().WithMessage("Multiplier is required")
                .GreaterThan(1).WithMessage("Multiplier must be bigger than 1");
        }
    }
}
