using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Halls;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Commands.Halls
{
    public class EfCreateHallCommand:EfUseCase, ICreateHallCommand
    {
        public int Id => 4;

        public string Name => "Create hall";

        private CreateHallDtoValidator _validator;

        public EfCreateHallCommand(AspContext context, CreateHallDtoValidator validator):base(context) { _validator  = validator; }

        public void Execute(CreateHallDto data)
        {
            _validator.ValidateAndThrow(data);
            var Hall = new AspProjekat.Domain.Hall
            {
                Id = data.Id,
                Name = data.Name,
                Capacity = data.Capacity
            };
            Context.Halls.Add(Hall);
            Context.SaveChanges();
        }
    }
}
