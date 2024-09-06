using AspProjekat.Application;
using AspProjekat.Application.Exceptions;
using AspProjekat.Application.UseCases.Commands.Projections;
using AspProjekat.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Commands.Projections
{
    public class EfBookProjectionCommand:EfUseCase, IBookProjectionCommand
    {
        public int Id => 3;
        public string Name => "Book projection";

        private readonly IApplicationActor _actor;

        public EfBookProjectionCommand(AspContext context, IApplicationActor actor):base(context)
        {
            _actor = actor;
        }
        public void Execute(int projectionId)
        {
            var projection = Context.Projections
                .Include(p => p.Users)
                .FirstOrDefault(p => p.Id == projectionId);

            if (projection == null)
            {
                throw new EntityNotFoundException("Projection", projectionId);
            }

            if (projection.Users.Count >= projection.Hall.Capacity)
            {
                throw new ConflictException("Projection is completely booked");
            }

            var currentTime = DateTime.Now;
            if ((projection.Time - currentTime).TotalMinutes <= 30)
            {
                throw new ConflictException("Cannot book this projection as it starts within 30 minutes");
            }

            if (projection.Users.Any(u => u.Id == _actor.Id))
            {
                throw new ConflictException("You have already booked this projection.");
            }

            var user = Context.Users.FirstOrDefault(u => u.Id == _actor.Id);
            if (user == null)
            {
                throw new EntityNotFoundException("User", _actor.Id);
            }

            projection.Users.Add(user);

            Context.SaveChanges();
        }

    }
}
