using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Movies;
using AspProjekat.DataAccess;
using AspProjekat.Domain;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Commands.Movies
{
    public class EfCreateMovieCommand:EfUseCase, ICreateMovieCommand
    {
        public int Id => 4;

        public string Name => "Create movie";

        private CreateMovieDtoValidator _validator;

        public EfCreateMovieCommand(AspContext context, CreateMovieDtoValidator validator) : base(context) { _validator = validator; }

        public void Execute(CreateMovieDto data)
        {
            _validator.ValidateAndThrow(data);

            var extension = Path.GetExtension(data.Image.FileName);
            var filename = Guid.NewGuid().ToString() + extension;
            var savePath = Path.Combine("wwwroot", "images", filename);

            Directory.CreateDirectory(Path.GetDirectoryName(savePath));

            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                data.Image.CopyTo(fs);
            }

            var Movie = new AspProjekat.Domain.Movie
            {
                Name = data.Name,   
                Description = data.Description,
                PremierDate = data.PremierDate,
                BasePrice = data.BasePrice,
                CoverImage = "/images/" + filename,
                DurationInMinutes = data.DurationInMinutes
            };
            if(data.ActorIds!=null && data.ActorIds.Any())
            {
                Movie.Actors = Context.Actors.Where(a => data.ActorIds.Contains(a.Id)).ToList();
            }
            if(data.GenreIds!=null && data.GenreIds.Any())
            {
                Movie.Genres = Context.Genres.Where(g => data.GenreIds.Contains(g.Id)).ToList();

            }
            Context.Movies.Add(Movie);
            Context.SaveChanges();
        }
    }
}
