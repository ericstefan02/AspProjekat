using AspProjekat.Application.UseCases.Commands.Actors;
using AspProjekat.Application.UseCases.Commands.Genres;
using AspProjekat.Application.UseCases.Commands.Halls;
using AspProjekat.Application.UseCases.Commands.Movies;
using AspProjekat.Application.UseCases.Commands.Projections;
using AspProjekat.Application.UseCases.Commands.ProjectionTypes;
using AspProjekat.Application.UseCases.Commands.Roles;
using AspProjekat.Implementation.UseCases.Commands.Actors;
using AspProjekat.Implementation.UseCases.Commands.Genres;
using AspProjekat.Implementation.UseCases.Commands.Halls;
using AspProjekat.Implementation.UseCases.Commands.Movies;
using AspProjekat.Implementation.UseCases.Commands.Projections;
using AspProjekat.Implementation.UseCases.Commands.ProjectionTypes;
using AspProjekat.Implementation.UseCases.Commands.Roles;
using AspProjekat.Implementation.Validators;
using System.IdentityModel.Tokens.Jwt;

namespace AspProjekat.API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICreateActorCommand, EfCreateActorCommand>();
            services.AddTransient<IUpdateActorCommand, EfUpdateActorCommand>();
            services.AddTransient<CreateActorDtoValidator>();
            services.AddTransient<UpdateActorDtoValidator>();
            services.AddTransient<ICreateGenreCommand, EfCreateGenreCommand>();
            services.AddTransient<IUpdateGenreCommand, EfUpdateGenreCommand>();
            services.AddTransient<CreateGenreDtoValidator>();
            services.AddTransient<UpdateGenreDtoValidator>();
            services.AddTransient<ICreateRoleCommand, EfCreateRoleCommand>();
            services.AddTransient<IUpdateRoleCommand, EfUpdateRoleCommand>();
            services.AddTransient<CreateRoleDtoValidator>();
            services.AddTransient<UpdateRoleDtoValidator>();
            services.AddTransient<ICreateHallCommand, EfCreateHallCommand>();
            services.AddTransient<IUpdateHallCommand, EfUpdateHallCommand>();
            services.AddTransient<CreateHallDtoValidator>();
            services.AddTransient<UpdateHallDtoValidator>();
            services.AddTransient<ICreateMovieCommand, EfCreateMovieCommand>();
            services.AddTransient<IUpdateMovieCommand, EfUpdateMovieCommand>();
            services.AddTransient<CreateMovieDtoValidator>();
            services.AddTransient<UpdateMovieDtoValidator>();
            services.AddTransient<ICreateProjectionCommand, EfCreateProjectionCommand>();
            services.AddTransient<IUpdateProjectionCommand, EfUpdateProjectionCommand>();
            services.AddTransient<CreateProjectionDtoValidator>();
            services.AddTransient<UpdateProjectionDtoValidator>();
            services.AddTransient<ICreateProjectionTypeCommand, EfCreateProjectionTypeCommand>();
            services.AddTransient<IUpdateProjectionTypeCommand, EfUpdateProjectionTypeCommand>();
            services.AddTransient<CreateProjectionTypeDtoValidator>();
            services.AddTransient<UpdateProjectionTypeDtoValidator>();
            services.AddTransient<IBookProjectionCommand, EfBookProjectionCommand>();
        }

        public static Guid? GetTokenId(this HttpRequest request)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization"))
            {
                return null;
            }

            string authHeader = request.Headers["Authorization"].ToString();

            if (authHeader.Split("Bearer ").Length != 2)
            {
                return null; 
            }

            string token = authHeader.Split("Bearer ")[1];

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(token);

            var claims = tokenObj.Claims;

            var claim = claims.First(x => x.Type == "jti").Value;

            var tokenGuid = Guid.Parse(claim);

            return tokenGuid;
        }
    }

}
