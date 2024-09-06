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
    public class EfGetGenresQuery: EfUseCase, IGetGenresQuery
    {
        public EfGetGenresQuery(AspContext context) : base(context) { }

        public int Id => 2;
        public string Name => "Search genres";

        public PagedResponse<GenreDto> Execute (PagedSearch search)
        {
            var query = Context.Genres.AsQueryable();
            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);
            query = query.Where(x => x.IsActive);
            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<GenreDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new GenreDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Movies = x.Movies.Select(y => new MovieDto
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Description = y.Description,
                        DurationInMinutes = y.DurationInMinutes,
                        PremierDate = y.PremierDate

                    }).ToList()
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount

            };
        }
    }
}
