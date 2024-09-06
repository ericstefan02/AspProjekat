using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Genres;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Commands.Genres
{
    public class EfCreateGenreCommand:EfUseCase, ICreateGenreCommand
    {
        public int Id => 4;
        public string Name => "Create genre";
        private readonly CreateGenreDtoValidator _validator;

        public EfCreateGenreCommand(AspContext context, CreateGenreDtoValidator validator):base(context) { _validator = validator; }

        public void Execute(CreateGenreDto data) 
        {
            _validator.ValidateAndThrow(data);

            var Genre = new AspProjekat.Domain.Genre
            {
                Name = data.Name,
            };
            if(data.MovieIds != null && data.MovieIds.Any())
            {
                Genre.Movies = Context.Movies.Where(x => data.MovieIds.Contains(x.Id)).ToList();
            }
            Context.Genres.Add(Genre);
            Context.SaveChanges();
            
        }
    }
}
