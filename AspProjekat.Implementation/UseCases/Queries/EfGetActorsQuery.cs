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
    public class EfGetActorsQuery : EfUseCase, IGetActorsQuery
    {
        public EfGetActorsQuery(AspContext context) : base(context)
        {
        }

        public int Id { get; } = 2;
        public string Name => "Search actors";

        public PagedResponse<ActorDto> Execute(PagedSearch search)
        {
            var query = Context.Actors.AsQueryable();

            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);
            query = query.Where(x => x.IsActive);
            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<ActorDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new ActorDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Movies = x.Movies.Select(m => new MovieDto
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Description = m.Description,
                        DurationInMinutes = m.DurationInMinutes,
                        PremierDate = m.PremierDate,
                    }).ToList()
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount
            };


        }
    }
}
