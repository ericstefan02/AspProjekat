using AspProjekat.Application;
using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Genres;
using AspProjekat.Application.UseCases.Commands.Projections;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.DataAccess;
using AspProjekat.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspProjekat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectionsController : ControllerBase
    {
        public IApplicationActor _actor;
        public UseCaseHandler _handler;
        public AspContext _context;

        public ProjectionsController(IApplicationActor actor, UseCaseHandler handler, AspContext context)
        {
            _actor = actor;
            _handler = handler;
            _context = context;
        }

        [HttpGet]

        public IActionResult Get([FromQuery] ProjectionsSearch search, [FromServices] IGetProjectionsQuery query) => Ok(_handler.HandleQuery(query, search));

        [HttpGet("{id}")]

        public IActionResult GetOne(int id, [FromServices] IGetSingleProjectionQuery query) => Ok(_handler.HandleQuery(query, id));

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectionDto dto, [FromServices] ICreateProjectionCommand cmd)
        {
            _handler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPost("book/{id}")]
        public IActionResult BookProjection(int id, [FromServices] IBookProjectionCommand cmd)
        {
            _handler.HandleCommand(cmd, id);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] UpdateProjectionDto dto, [FromServices] IUpdateProjectionCommand cmd)
        {
            dto.Id = id;
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Delete(int id, [FromServices] IUpdateProjectionCommand cmd)
        {
            UpdateProjectionDto dto = new UpdateProjectionDto { Id = id, IsActive = false };
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }
    }
}
