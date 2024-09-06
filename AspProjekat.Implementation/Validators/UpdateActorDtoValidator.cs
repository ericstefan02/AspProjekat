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
    public class UpdateActorDtoValidator : AbstractValidator<UpdateActorDto>
    {
        private readonly AspContext ctx;

        public UpdateActorDtoValidator(AspContext context) 
        {
            ctx = context;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");

             RuleFor(x=>x.FirstName).NotEmpty().WithMessage("First name is required")
                .MinimumLength(2).WithMessage("First name must have at least 2 characters")
                .MaximumLength(50).WithMessage("First name must have at most 50 characters");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required")
               .MinimumLength(2).WithMessage("Last name must have at least 2 characters")
               .MaximumLength(50).WithMessage("Last name must have at most 50 characters");
            RuleFor(x => x.MovieIds).Must(MovieExists).WithMessage("All movies must exist");

        }
        private bool MovieExists(IEnumerable<int>? movieIds)
        {
            if (movieIds == null || !movieIds.Any())
            {
                return true;
            }
            return ctx.Movies.Count(m => movieIds.Contains(m.Id)) == movieIds.Count();
        }
    }
}
