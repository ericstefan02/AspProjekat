using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.DataAccess;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Queries
{
    public class EfGetMoviesQuery:EfUseCase, IGetMoviesQuery
    {
        public EfGetMoviesQuery(AspContext context ):base(context) { }

        public int Id => 2;
        public string Name => "Search Movies";
        public PagedResponse<MovieDto> Execute(MovieSearch search)
        {
            var query = Context.Movies.AsQueryable();

            if(search.DurationInMinutes.HasValue)
            {
                query = query.Where(x => x.DurationInMinutes < search.DurationInMinutes);
            }
            if(!search.MovieTitle.IsNullOrEmpty())
            {
                query = query.Where(x=>x.Name.ToLower().Trim().Contains(search.MovieTitle.ToLower().Trim()));   
            }
            if(search.GenreIds?.Count()>0)
            {
                query = query.Where(x=>x.Genres.Any(g=>search.GenreIds.Contains(g.Id)));

            }
            if(search.ActorIds?.Count()>0)
            {
                query = query.Where(x=>x.Actors.Any(a=>search.ActorIds.Contains(a.Id)));
            }

            query = query.Where(x => x.IsActive);

            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<MovieDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new MovieDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    BasePrice = x.BasePrice,
                    CoverImage = x.CoverImage,
                    Description = x.Description,
                    DurationInMinutes = x.DurationInMinutes,
                    PremierDate = x.PremierDate,
                    Genres = x.Genres.Select(g => new GenreDto
                    {
                        Id = g.Id,
                        Name = g.Name,
                    }).ToList(),
                    Actors = x.Actors.Select(a=> new ActorDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                    }).ToList()

                }).ToList(),
                TotalCount = totalCount,
                PerPage = perPage
            };

        }


    }
}
