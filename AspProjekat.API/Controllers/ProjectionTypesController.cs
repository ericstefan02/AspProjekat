using AspProjekat.Application;
using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Genres;
using AspProjekat.Application.UseCases.Commands.ProjectionTypes;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.DataAccess;
using AspProjekat.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspProjekat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectionTypesController : ControllerBase
    {
        public IApplicationActor _actor;
        public UseCaseHandler _handler;
        public AspContext _context;

        public ProjectionTypesController(IApplicationActor actor, UseCaseHandler handler, AspContext context)
        {
            _actor = actor;
            _handler = handler;
            _context = context;
        }

        [HttpGet]

        public IActionResult Get([FromQuery] PagedSearch search, [FromServices] IGetProjectionTypesQuery query) => Ok(_handler.HandleQuery(query, search));

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateProjectionTypeDto dto, [FromServices] ICreateProjectionTypeCommand cmd)
        {
            _handler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] UpdateProjectionTypeDto dto, [FromServices] IUpdateProjectionTypeCommand cmd)
        {
            dto.Id = id;
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Delete(int id, [FromServices] IUpdateProjectionTypeCommand cmd)
        {
            UpdateProjectionTypeDto dto = new UpdateProjectionTypeDto { Id = id, IsActive = false };
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }
    }
}
