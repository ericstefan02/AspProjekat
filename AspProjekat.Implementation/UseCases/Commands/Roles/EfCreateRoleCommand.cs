using AspProjekat.Application.DTO;
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
    public class EfCreateRoleCommand : EfUseCase, ICreateRoleCommand
    {
        public int Id => 5;
        public string Name => "Create role";

        private CreateRoleDtoValidator _validator;

        public EfCreateRoleCommand(AspContext context, CreateRoleDtoValidator validator) : base(context) { _validator = validator; }

        public void Execute(CreateRoleDto data)
        {
            _validator.ValidateAndThrow(data);

            var Role = new AspProjekat.Domain.Role
            {
                Name = data.Name,
            };
            Context.Roles.Add(Role);
            Context.SaveChanges();
        }
    }
}
