using AspProjekat.Application.DTO;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.UseCases;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Application.UseCases.Commands.Users
{
    public class EfUpdateUserAccessCommand : EfUseCase, IUpdateUserAccessCommand
    {
        private UpdateUserAccessDtoValidator _validator;
        public EfUpdateUserAccessCommand(AspContext context,
             UpdateUserAccessDtoValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 5;

        public string Name => "Modify user access";

        public void Execute(UpdateUserAccessDto data)
        {
            _validator.ValidateAndThrow(data);

            var userUseCases = Context.UserUseCases
                                      .Where(x => x.RoleId == data.RoleId)
                                      .ToList();

            Context.UserUseCases.RemoveRange(userUseCases);

            Context.UserUseCases.AddRange(data.UseCaseIds.Select(x =>
            new Domain.UserUseCase
            {
                RoleId = data.RoleId,
                UseCaseId = x
            }));

            Context.SaveChanges();
        }
    }
}
