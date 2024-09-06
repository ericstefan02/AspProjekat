using AspProjekat.Application;
using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Actors;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.DataAccess;
using AspProjekat.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspProjekat.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController:ControllerBase
    {
        private IApplicationActor _actor;
        private UseCaseHandler _handler;
        private AspContext _context;

        public ActorsController(IApplicationActor actor, UseCaseHandler handler, AspContext context)
        {
            _actor = actor;
            _handler = handler;
            _context = context;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] PagedSearch search, [FromServices] IGetActorsQuery query) => Ok(_handler.HandleQuery(query, search));

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateActorDto dto, [FromServices] ICreateActorCommand cmd)
        {
            _handler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateActorDto dto, [FromServices] IUpdateActorCommand cmd)
        {
            dto.Id = id;
            _handler.HandleCommand(cmd, dto);

            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Delete(int id, [FromServices] IUpdateActorCommand cmd) 
        {
            UpdateActorDto dto = new UpdateActorDto { Id = id, IsActive = false };
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }
        
    }
}
