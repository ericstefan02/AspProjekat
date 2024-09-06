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
    public class EfGetRolesQuery:EfUseCase, IGetRolesQuery 
    {
        public EfGetRolesQuery(AspContext context) : base(context) { }

        public int Id => 2;
        public string Name => "Search roles";
        public PagedResponse<RoleDto> Execute(PagedSearch search)
        {
            var query = Context.Roles.AsQueryable();
            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Where(x => x.IsActive);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<RoleDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new RoleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount
            };
        }
    }
}
