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
    public class EfGetSIngleProjection : EfUseCase, IGetSingleProjectionQuery
    {
        public EfGetSIngleProjection(AspContext context ) : base(context) { }
        public int Id => 2;
        public string Name => "Get single projection";
        public ProjectionDto Execute(int id)
        {
            var Projection = Context.Projections.Where(x => x.IsActive && x.Id == id).Select(x => new ProjectionDto
            {
                Id = x.Id,
                Price = (decimal)x.Price,
                Time = x.Time,
                Hall = new HallDto
                {
                    Id = x.Hall.Id,
                    Name = x.Hall.Name,
                    Capacity = x.Hall.Capacity
                },
                Movie = new MovieDto
                {
                    Id = x.Movie.Id,
                    Name= x.Movie.Name,
                    Description = x.Movie.Description,
                    DurationInMinutes = x.Movie.DurationInMinutes,
                    CoverImage = x.Movie.CoverImage,
                    PremierDate = x.Movie.PremierDate,
                },
                ProjectionType = new ProjectionTypeDto
                {
                    Id = x.Type.Id,
                    Name = x.Type.Name,
                    Multiplier = x.Type.Multiplier
                },
                Users = x.Users.Select(u=>new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Username = u.Username
                }).ToList()
            }).FirstOrDefault();
            return Projection;
        }
    }
}
