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
    public class UpdateProjectionDtoValidator:AbstractValidator<UpdateProjectionDto>
    {
        private readonly AspContext ctx;
        public UpdateProjectionDtoValidator(AspContext context)
        {
            ctx = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Time).NotEmpty().WithMessage("Time is required");
            RuleFor(x => x.MovieId).Must(MovieExists).WithMessage("Movie must exist");
            RuleFor(x => x.HallId).Must(HallExists).WithMessage("Hall exists");
            RuleFor(x => x.ProjectionTypeId).Must(ProjectionTypeExists).WithMessage("Projection type must exist");

        }

        private bool HallExists(int? hallId)
        {
            return ctx.Halls.Any(h => h.Id == hallId);
        }
        private bool MovieExists(int? movieId)
        {
            return ctx.Movies.Any(m => m.Id == movieId);
        }
        private bool ProjectionTypeExists(int? projectionTypeId)
        {
            return ctx.ProjectionTypes.Any(p => p.Id == projectionTypeId);
        }
    }
}
