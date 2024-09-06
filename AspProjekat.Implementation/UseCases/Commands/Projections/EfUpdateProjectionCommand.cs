using AspProjekat.Application.DTO;
using AspProjekat.Application.Exceptions;
using AspProjekat.Application.UseCases.Commands.Projections;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Commands.Projections
{
    public class EfUpdateProjectionCommand:EfUseCase, IUpdateProjectionCommand
    {
        public int Id => 4;
        public string Name => "Update projection";

        private UpdateProjectionDtoValidator _validator;

        public EfUpdateProjectionCommand(AspContext context, UpdateProjectionDtoValidator validator):base(context)
        {
            _validator = validator;
        }

        public void Execute(UpdateProjectionDto request)
        {
            _validator.ValidateAndThrow(request);

            var Projection = Context.Projections.Where(p => p.Id == request.Id).FirstOrDefault();
            if (Projection == null)
            {
                throw new EntityNotFoundException("Projectiin", request.Id);
            }
            Projection.IsActive = request.IsActive ?? Projection.IsActive;
            Projection.Price = (double?)request.Price ?? Projection.Price;
            Projection.Time = request.Time ?? Projection.Time;
            if (request.MovieId != null)
            {
                Projection.Movie = Context.Movies.Where(m => m.Id == request.Id).FirstOrDefault() ?? Projection.Movie;
            }
            if(request.HallId != null)
            {
                Projection.Hall = Context.Halls.Where(h => h.Id == request.Id).FirstOrDefault() ?? Projection.Hall;

            }
            if(request.ProjectionTypeId != null)
            {
                Projection.Type = Context.ProjectionTypes.Where(t => t.Id == request.Id).FirstOrDefault() ?? Projection.Type;

            }
            Context.SaveChanges();
        }
    }
}
