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
    public class CreateRoleDtoValidator:AbstractValidator<CreateRoleDto>
    {
        private readonly AspContext _context;
        public CreateRoleDtoValidator(AspContext context) 
        {
            _context = context;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Role name is Required")
                .Must(BeUniqueName).WithMessage("Role must have unique name");
        }
        private bool BeUniqueName(string name)
        {
            return !_context.Roles.Any(r => r.Name == name);
        }
    }
}
