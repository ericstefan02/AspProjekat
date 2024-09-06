using AspProjekat.Application;
using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Genres;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.DataAccess;
using AspProjekat.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspProjekat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        public IApplicationActor _actor;
        public UseCaseHandler _handler;
        public AspContext _context;

        public GenresController(IApplicationActor actor, UseCaseHandler handler, AspContext context)
        {
            _actor = actor;
            _handler = handler;
            _context = context;
        }

        [HttpGet]

        public IActionResult Get([FromQuery] PagedSearch search, [FromServices] IGetGenresQuery query) => Ok(_handler.HandleQuery(query, search));

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateGenreDto dto, [FromServices] ICreateGenreCommand cmd)
        {
            _handler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] UpdateGenreDto dto, [FromServices] IUpdateGenreCommand cmd)
        {
            dto.Id = id;
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Delete(int id, [FromServices] IUpdateGenreCommand cmd)
        {
            UpdateGenreDto dto = new UpdateGenreDto { Id = id, isActive = false };
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }
    }
}
