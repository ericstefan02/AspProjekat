using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Users;
using AspProjekat.DataAccess;
using AspProjekat.Domain;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Commands.Users
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        public int Id => 2;

        public string Name => "UserRegistration";


        private RegisterUserDtoValidator _validator;

        public EfRegisterUserCommand(AspContext context, RegisterUserDtoValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public void Execute(RegisterUserDto data)
        {
            _validator.ValidateAndThrow(data);

            User user = new User
            {
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                Username = data.Username,
                Role = Context.Roles.Where(r=>r.Id==data.RoleId).FirstOrDefault()
            };

            Context.Users.Add(user);

            Context.SaveChanges();
        }
    }
}
