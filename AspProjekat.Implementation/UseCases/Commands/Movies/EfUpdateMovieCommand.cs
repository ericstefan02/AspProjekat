using AspProjekat.Application.DTO;
using AspProjekat.Application.Exceptions;
using AspProjekat.Application.UseCases.Commands.Movies;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspProjekat.Implementation.UseCases.Commands.Movies
{
    public class EfUpdateMovieCommand : EfUseCase, IUpdateMovieCommand
    {
        public int Id => 4;

        public string Name => "Create Blog";

        private readonly UpdateMovieDtoValidator _validator;

        public EfUpdateMovieCommand(AspContext context, UpdateMovieDtoValidator validator) : base(context) { _validator = validator; }

        public void Execute(UpdateMovieDto request)
        {
            _validator.ValidateAndThrow(request);

            var Movie = Context.Movies.FirstOrDefault(x => x.Id == request.Id);
            if (Movie == null)
            {
                throw new EntityNotFoundException("Movie", request.Id);
            }
            Movie.Name = request.Name ?? Movie.Name;
            Movie.DurationInMinutes = request.DurationInMinutes ?? Movie.DurationInMinutes;
            Movie.Description = request.Description ?? Movie.Description;
            Movie.BasePrice = request.BasePrice ?? Movie.BasePrice;
            Movie.PremierDate = request.PremierDate ?? Movie.PremierDate;
            Movie.IsActive = request.isActive ?? Movie.IsActive;
            if (request.ActorIds != null && request.ActorIds.Any())
            {
                Movie.Actors = Context.Actors.Where(a => request.ActorIds.Contains(a.Id)).ToList();
            }
            if (request.GenreIds != null && request.GenreIds.Any())
            {
                Movie.Genres = Context.Genres.Where(g => request.GenreIds.Contains(g.Id)).ToList();

            }

            if (request.Image != null)
            {
                var extension = Path.GetExtension(request.Image.FileName);
                var filename = Guid.NewGuid().ToString() + extension;
                var savepath = Path.Combine("wwwroot", "images", filename);

                Directory.CreateDirectory(Path.GetDirectoryName(savepath));

                using (var fs = new FileStream(savepath, FileMode.Create))
                {
                    request.Image.CopyTo(fs);
                }

                Movie.CoverImage = "/images/" + filename;
            }
            Context.SaveChanges();
        }
    }
}
