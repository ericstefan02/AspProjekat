using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Queries
{
    public class EfGetSingleMovie:EfUseCase, IGetSingleMovieQuery
    {
        public EfGetSingleMovie(AspContext context) : base(context) { }
        public int Id => 2;

        public string Name => "Get Single Movie";

        public MovieDto Execute(int id)
        {
            var Movie = Context.Movies.Where(x => x.IsActive && x.Id == id).Select(x => new MovieDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DurationInMinutes = x.DurationInMinutes,
                PremierDate = x.PremierDate,
                BasePrice = x.BasePrice,
                CoverImage = x.CoverImage,
                Actors = x.Actors.Select(a => new ActorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                }).ToList(),
                Genres = x.Genres.Select(g=>new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToList()
            }).FirstOrDefault();

            return Movie;
        }
    }
}
