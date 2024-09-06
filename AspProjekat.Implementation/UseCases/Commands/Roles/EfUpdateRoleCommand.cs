using AspProjekat.Application.DTO;
using AspProjekat.Application.Exceptions;
using AspProjekat.Application.UseCases.Commands.Roles;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Commands.Roles
{
    public class EfUpdateRoleCommand:EfUseCase, IUpdateRoleCommand
    {
        public int Id => 5;
        public string Name => "Update role";

        private UpdateRoleDtoValidator _validator;

        public EfUpdateRoleCommand(AspContext context, UpdateRoleDtoValidator validator):base(context) { _validator = validator; }

        public void Execute(UpdateRoleDto request)
        {
            _validator.ValidateAndThrow(request);

            var Role = Context.Roles.FirstOrDefault(x=>x.Id == request.Id);
            if(Role == null)
            {
                throw new EntityNotFoundException("Role", request.Id);
            }
            Role.Name = request.Name ?? Role.Name;
            Role.IsActive = request.IsActive ?? Role.IsActive;
            Context.SaveChanges();
        }
    }
}
