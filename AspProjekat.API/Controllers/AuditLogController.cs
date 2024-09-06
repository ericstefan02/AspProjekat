using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.Application;
using AspProjekat.DataAccess;
using AspProjekat.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace AspProjekat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private IApplicationActor _actor;
        private UseCaseHandler _handler;
        private AspContext _context;
        public AuditLogController(IApplicationActor actor, UseCaseHandler handler, AspContext context)
        {
            _actor = actor;
            _handler = handler;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] AuditLogSearch search, [FromServices] IGetAuditLogQuery query)
          => Ok(_handler.HandleQuery(query, search));
    }
}
