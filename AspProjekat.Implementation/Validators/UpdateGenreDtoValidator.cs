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
    public class UpdateGenreDtoValidator : AbstractValidator<UpdateGenreDto>
    {
        private readonly AspContext _context;

        public UpdateGenreDtoValidator(AspContext context)
        {
            _context = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0"); 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .MinimumLength(2).WithMessage("Name must have more than 2 characters")
                .MaximumLength(40).WithMessage("Name must have less than 40 characters")
                .Must(BeUniqueName).WithMessage("Name must be unique");
            RuleFor(x => x.MovieIds).Must(MovieExists).WithMessage("All movies must exist");

        }

        private bool BeUniqueName(string name)
        {
            return !_context.Genres.Any(g => g.Name == name);
        }
        private bool MovieExists(IEnumerable<int>? movieIds)
        {
            if (movieIds == null || !movieIds.Any())
            {
                return true;
            }
            return _context.Movies.Count(m => movieIds.Contains(m.Id)) == movieIds.Count();
        }
    }
}
