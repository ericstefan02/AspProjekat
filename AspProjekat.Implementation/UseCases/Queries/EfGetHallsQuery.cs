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
    public class EfGetHallsQuery : EfUseCase, IGetHallsQuery
    {
        public EfGetHallsQuery(AspContext context) : base(context) { }

        public int Id => 2;

        public string Name => "Search halls";

        public PagedResponse<HallDto> Execute(PagedSearch search)
        {
            var query = Context.Halls.AsQueryable();
            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Where(x => x.IsActive);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<HallDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new HallDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Capacity = x.Capacity,
                    Projections = x.Projections.Select(x => new ProjectionDto
                    {
                        Id = x.Id,
                        Time = x.Time,
                        Price = (decimal)x.Price,
                        Movie = new MovieDto
                        {
                            Id = x.Movie.Id,
                            Name = x.Movie.Name,
                            Description = x.Movie.Description,
                            DurationInMinutes = x.Movie.DurationInMinutes,
                            PremierDate = x.Movie.PremierDate
                        }

                    }).ToList()

                }).ToList(),
                TotalCount = totalCount,
                PerPage = perPage
            };
        }
    }
}
