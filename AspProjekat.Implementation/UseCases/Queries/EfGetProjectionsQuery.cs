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
    public class EfGetProjectionsQuery : EfUseCase, IGetProjectionsQuery
    {
        public EfGetProjectionsQuery(AspContext context) : base(context) { }

        public int Id => 2;
        public string Name => "Search projections";
        public PagedResponse<ProjectionDto> Execute(ProjectionsSearch search)
        {
            var query = Context.Projections.AsQueryable();

            if (search.MaxPrice.HasValue)
            {
                query = query.Where(x => (decimal)x.Price < search.MaxPrice);
            }
            if (search.MovieId.HasValue)
            {
                query = query.Where(x => x.MovieId == search.MovieId);
            }
            if (search.TypeId.HasValue)
            {
                query = query.Where(x => x.TypeId == search.TypeId);
            }
            if (search.To.HasValue)
            {
                query = query.Where(x => x.Time < search.To);
            }
            if(search.From.HasValue)
            {
                query = query.Where(x => x.Time > search.From);
            }
            query = query.Where(x => x.IsActive);

            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<ProjectionDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new ProjectionDto
                {
                    Id = x.Id,
                    Price = (decimal)x.Price,
                    Time = x.Time,
                    Hall = new HallDto
                    {
                        Id = x.Hall.Id,
                        Capacity = x.Hall.Capacity,
                        Name = x.Hall.Name
                    },
                    Movie = new MovieDto
                    {
                        Id = x.Movie.Id,
                        Name = x.Movie.Name,
                        Description = x.Movie.Description,
                        DurationInMinutes = x.Movie.DurationInMinutes,
                        PremierDate = x.Movie.PremierDate,
                        CoverImage = x.Movie.CoverImage
                    },
                    ProjectionType = new ProjectionTypeDto
                    {
                        Id = x.Type.Id,
                        Name = x.Type.Name,
                        Multiplier = x.Type.Multiplier
                    },
                    Users = x.Users.Select(y => new UserDto
                    {
                        Id = y.Id,
                        FirstName = y.FirstName,
                        LastName = y.LastName,
                        Email = y.Email,
                        Username = y.Username
                    }).ToList()
                }).ToList(),
                TotalCount = totalCount,
                PerPage = perPage
            };
        }

    }
}
