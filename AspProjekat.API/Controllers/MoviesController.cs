using AspProjekat.Application;
using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Genres;
using AspProjekat.Application.UseCases.Commands.Movies;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.DataAccess;
using AspProjekat.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspProjekat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public IApplicationActor _actor;
        public UseCaseHandler _handler;
        public AspContext _context;

        public MoviesController(IApplicationActor actor, UseCaseHandler handler, AspContext context)
        {
            _actor = actor;
            _handler = handler;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] MovieSearch search, [FromServices] IGetMoviesQuery query) => Ok(_handler.HandleQuery(query, search));

        [HttpGet("{id}")]
        public IActionResult GetOne(int id, [FromServices] IGetSingleMovieQuery query) => Ok(_handler.HandleQuery(query, id));

        [Authorize]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult Post([FromForm] CreateMovieDto dto, [FromServices] ICreateMovieCommand cmd)
        {
            _handler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]

        public IActionResult Put(int id, [FromForm] UpdateMovieDto dto, [FromServices] IUpdateMovieCommand cmd)
        {
            dto.Id = id;
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Delete(int id, [FromServices] IUpdateMovieCommand cmd)
        {
            UpdateMovieDto dto = new UpdateMovieDto { Id = id, isActive = false };
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }
    }
}
