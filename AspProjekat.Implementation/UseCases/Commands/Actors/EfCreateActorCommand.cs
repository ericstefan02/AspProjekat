using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Actors;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Commands.Actors
{
    public class EfCreateActorCommand : EfUseCase, ICreateActorCommand
    {
        public int Id => 4;
        public string Name => "Create actor";

        private readonly CreateActorDtoValidator _validator;

        public EfCreateActorCommand(AspContext context, CreateActorDtoValidator validator):base(context)
        {
            _validator = validator;
        }

        public void Execute(CreateActorDto data) 
        {
            _validator.ValidateAndThrow(data);

            var Actor = new AspProjekat.Domain.Actor
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
            };
            if(data.MovieIds?.Count()>0)
            {
                Actor.Movies = Context.Movies.Where(x=>data.MovieIds.Contains(x.Id)).ToList();
            }
            Context.Actors.Add(Actor);
            Context.SaveChanges();
        }
    }
}
