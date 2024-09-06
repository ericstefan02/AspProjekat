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
    public class EfGetAuditLogsQuery : EfUseCase, IGetAuditLogQuery
    {
        public EfGetAuditLogsQuery(AspContext context) : base(context)
        {
        }

        public int Id => 4;

        public string Name => "Search audit log";

        public PagedResponse<AuditLogDto> Execute(AuditLogSearch search)
        {
            var query = Context.UseCaseLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Username))
            {
                query = query.Where(x => x.Username.Contains(search.Username));
            }

            if (!string.IsNullOrEmpty(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.Contains(search.UseCaseName));
            }

            if (search.From.HasValue)
            {
                query = query.Where(x => x.ExecutedAt > search.From);
            }

            if (search.To.HasValue)
            {
                query = query.Where(x => x.ExecutedAt < search.From);
            }

            int totalCount = query.Count();

            int perPage = search.PerPage.HasValue ? (int)Math.Abs((double)search.PerPage) : 10;
            int page = search.Page.HasValue ? (int)Math.Abs((double)search.Page) : 1;

            int skip = perPage * (page - 1);

            query = query.Skip(skip).Take(perPage);

            return new PagedResponse<AuditLogDto>
            {
                CurrentPage = page,
                Data = query.Select(x => new AuditLogDto
                {
                    Username = x.Username,
                    UseCaseData = x.UseCaseData,
                    ExecutedAt = x.ExecutedAt,
                    UseCaseName = x.UseCaseName
                }).ToList(),
                PerPage = perPage,
                TotalCount = totalCount,
            };
        }
    }
}
