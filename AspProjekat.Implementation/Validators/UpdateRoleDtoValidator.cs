using AspProjekat.Application.DTO;
using AspProjekat.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.Validators
{

    public class UpdateRoleDtoValidator:AbstractValidator<UpdateRoleDto>
    {
        private readonly AspContext _context;
        public UpdateRoleDtoValidator(AspContext context)
        {
            _context = context;
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be bigger than 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Role name is Required")
                .Must(BeUniqueName).WithMessage("Role must have unique name");
        }
        private bool BeUniqueName(string name)
        {
            return !_context.Roles.Any(r => r.Name == name);
        }
    }
}
