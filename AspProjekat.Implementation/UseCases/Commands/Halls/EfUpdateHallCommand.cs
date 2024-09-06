using AspProjekat.Application.DTO;
using AspProjekat.Application.Exceptions;
using AspProjekat.Application.UseCases.Commands.Halls;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspProjekat.Implementation.UseCases.Commands.Halls
{
    public class EfUpdateHallCommand:EfUseCase, IUpdateHallCommand
    {
        public int Id => 4;
        public string Name => "Update hall";

        private UpdateHallDtoValidator _validator;

        public EfUpdateHallCommand(AspContext context,  UpdateHallDtoValidator validator):base(context) { _validator = validator; }

        public void Execute(UpdateHallDto request)
        {
            _validator.ValidateAndThrow(request);

            var Hall = Context.Halls.FirstOrDefault(x => x.Id == request.Id);

            if (Hall == null)
            {
                throw new EntityNotFoundException("Hall", request.Id);
            }
            if (request.Name != null)
            {
                Hall.Name = request.Name;
            }
            if(request.Capacity !=  null)
            {
                Hall.Capacity = (int)request.Capacity;
            }
            if(request.IsActive != null)
            {
                Hall.IsActive = (bool)request.IsActive;
            }
            Context.SaveChanges();
        }

    }
}
