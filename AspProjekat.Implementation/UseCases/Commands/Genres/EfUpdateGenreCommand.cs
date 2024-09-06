using AspProjekat.Application.DTO;
using AspProjekat.Application.Exceptions;
using AspProjekat.Application.UseCases.Commands.Genres;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspProjekat.Implementation.UseCases.Commands.Genres
{
    public class EfUpdateGenreCommand: EfUseCase, IUpdateGenreCommand
    {
        public int Id => 4;
        public string Name => "Create genre";
        private readonly UpdateGenreDtoValidator _validator;

        public EfUpdateGenreCommand(AspContext context, UpdateGenreDtoValidator validator):base(context) { _validator = validator; }

        public void Execute(UpdateGenreDto request)
        {
            _validator.ValidateAndThrow(request);

            var Genre = Context.Genres.FirstOrDefault(x => x.Id == request.Id);

            if (Genre == null)
            {
                throw new EntityNotFoundException("Genre", request.Id);
            }

            if(request.Name != null)
            {
                Genre.Name = request.Name;
            }
            if(request.isActive != null)
            {
                Genre.IsActive = (bool)request.isActive;
            }
            if (request.MovieIds != null && request.MovieIds.Any())
            {
                Genre.Movies = Context.Movies.Where(x => request.MovieIds.Contains(x.Id)).ToList();
            }

            Context.SaveChanges();
        }
    }
}
