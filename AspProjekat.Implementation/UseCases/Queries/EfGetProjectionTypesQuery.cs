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
    public class EfGetProjectionTypesQuery : EfUseCase, IGetProjectionTypesQuery
    {
        public EfGetProjectionTypesQuery(AspContext context) : base(context) { }

        public int Id => 2;

        public string Name => "Search Projectio Types";

        public PagedResponse<ProjectionTypeDto> Execute(PagedSearch search)
        {
            var query = Context.ProjectionTypes.AsQueryable();

            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Where(x => x.IsActive);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<ProjectionTypeDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new ProjectionTypeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Multiplier = x.Multiplier
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
    
}
