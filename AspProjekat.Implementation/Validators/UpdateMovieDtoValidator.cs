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
    public class UpdateMovieDtoValidator:AbstractValidator<UpdateMovieDto>
    {
        private readonly AspContext _context;

        public UpdateMovieDtoValidator(AspContext context) {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required").GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x.PremierDate).NotEmpty().WithMessage("Premier date is required");
            RuleFor(x => x.DurationInMinutes).NotEmpty().WithMessage("Duration is required").GreaterThan(0).
                    WithMessage("Duration must be bigger than 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required");
            RuleFor(x => x.BasePrice).NotEmpty().WithMessage("Base price is required").GreaterThan(1).WithMessage("Base price must be greater than 1");
            RuleFor(x => x.GenreIds).Must(GenreExists).WithMessage("All genres must exist");
            RuleFor(x => x.ActorIds).Must(ActorExists).WithMessage("All actors must exist");
        }

        private bool GenreExists(IEnumerable<int>? genreIds)
        {
            if (genreIds == null || !genreIds.Any())
            {
                return true;
            }
            return _context.Genres.Count(g => genreIds.Contains(g.Id)) == genreIds.Count();
        }

        private bool ActorExists(IEnumerable<int>? actorIds)
        {
            if (actorIds == null || !actorIds.Any())
            {
                return true;
            }
            return _context.Actors.Count(a => actorIds.Contains(a.Id)) == actorIds.Count();
        }
    }
}
