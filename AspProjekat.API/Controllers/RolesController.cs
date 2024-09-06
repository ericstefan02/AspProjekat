using AspProjekat.Application;
using AspProjekat.Application.DTO;
using AspProjekat.Application.UseCases.Commands.Genres;
using AspProjekat.Application.UseCases.Commands.Roles;
using AspProjekat.Application.UseCases.Commands.Users;
using AspProjekat.Application.UseCases.Queries;
using AspProjekat.DataAccess;
using AspProjekat.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspProjekat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        public IApplicationActor _actor;
        public UseCaseHandler _handler;
        public AspContext _context;

        public RolesController(IApplicationActor actor, UseCaseHandler handler, AspContext context)
        {
            _actor = actor;
            _handler = handler;
            _context = context;
        }

        [HttpGet]

        public IActionResult Get([FromQuery] PagedSearch search, [FromServices] IGetRolesQuery query) => Ok(_handler.HandleQuery(query, search));

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] CreateRoleDto dto, [FromServices] ICreateRoleCommand cmd)
        {
            _handler.HandleCommand(cmd, dto);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("{id}")]

        public IActionResult Put(int id, [FromBody] UpdateRoleDto dto, [FromServices] IUpdateRoleCommand cmd)
        {
            dto.Id = id;
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}/access")]
        public IActionResult ModifyAccess(int id, [FromBody] UpdateUserAccessDto dto,
                                                  [FromServices] IUpdateUserAccessCommand command)
        {
            dto.RoleId = id;
            _handler.HandleCommand(command, dto);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]

        public IActionResult Delete(int id, [FromServices] IUpdateRoleCommand cmd)
        {
            UpdateRoleDto dto = new UpdateRoleDto { Id = id, IsActive = false };
            _handler.HandleCommand(cmd, dto);
            return NoContent();
        }
    }
}
