using AspProjekat.Application;
using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Genres;
using AspProjekat.Application.UseCases.Commands.Halls;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.DataAccess;
using AspProjekat.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspProjekat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallsController : ControllerBase
    {
        public IApplicationActor _actor;
        public UseCaseHandler _handler;
        public AspContext _context;

        public HallsController(IApplicationActor actor, UseCaseHandler handler, AspContext context)
        {
            _actor = actor;
            _handler = handler;
            _context = context;
        }

        [HttpGet]

        public IActionResult Get([FromQuery] PagedSearch search, [FromServices] IGetHallsQuery query) => Ok(_handler.HandleQuery(query, search));

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateHallDto dto, [FromServices] ICreateHallCommand cmd)
        {
            _handler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] UpdateHallDto dto, [FromServices] IUpdateHallCommand cmd)
        {
            dto.Id = id;
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Delete(int id, [FromServices] IUpdateHallCommand cmd)
        {
            UpdateHallDto dto = new UpdateHallDto { Id = id, IsActive = false };
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }
    }
}
