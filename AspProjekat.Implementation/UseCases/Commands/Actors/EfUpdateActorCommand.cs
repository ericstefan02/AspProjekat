using AspProjekat.Application.DTO;
using AspProjekat.Application.Exceptions;
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
    public class EfUpdateActorCommand:EfUseCase, IUpdateActorCommand
    {
        public int Id => 4;
        public string Name => "Update actor";
        private readonly UpdateActorDtoValidator _validator;

        public EfUpdateActorCommand(AspContext context, UpdateActorDtoValidator validator):base(context) { _validator = validator; }

        public void Execute(UpdateActorDto request)
        {
            _validator.ValidateAndThrow(request);

            var Actor = Context.Actors.FirstOrDefault(x=>x.Id == request.Id);

            if (Actor == null)
            {
                throw new EntityNotFoundException("Actor", request.Id);
            }
            if (request.FirstName != null)
            {
                Actor.FirstName = request.FirstName;
            }
            if(request.LastName != null)
            {
                Actor.LastName = request.LastName;
            }
            if(request.IsActive != null)
            {
                Actor.IsActive = (bool)request.IsActive;
            }
            if(request.MovieIds?.Count()>0)
            {
                Actor.Movies = Context.Movies.Where(x=>request.MovieIds.Contains(x.Id)).ToList();
            }
            Context.SaveChanges();

        }
    }
}
