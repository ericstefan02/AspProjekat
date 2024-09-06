using AspProjekat.Application.DTO;
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
    public class EfCreateProjectionCommand:EfUseCase,ICreateProjectionCommand
    {
        public int Id => 4;

        public string Name => "Create projection";

        private CreateProjectionDtoValidator _validator;

        public EfCreateProjectionCommand(AspContext context, CreateProjectionDtoValidator validator):base(context) { _validator  = validator; }

        public void Execute(CreateProjectionDto data)
        {
            _validator.ValidateAndThrow(data);

            var Projection = new AspProjekat.Domain.Projection
            {

                Time = data.Time,
                Hall = Context.Halls.Where(h => h.Id == data.HallId).FirstOrDefault(),
                Movie = Context.Movies.Where(m => m.Id == data.MovieId).FirstOrDefault(),
                Type = Context.ProjectionTypes.Where(p => p.Id == data.ProjectionTypeId).FirstOrDefault()
            };
            Projection.Price = (double)(Projection.Movie.BasePrice * Projection.Type.Multiplier);
            Context.Projections.Add(Projection);
            Context.SaveChanges();
        }
    }
}
