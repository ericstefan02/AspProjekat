using AspProjekat.Application.DTO;
using AspProjekat.Application.Exceptions;
using AspProjekat.Application.UseCases.Commands.ProjectionTypes;
using AspProjekat.DataAccess;
using AspProjekat.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.UseCases.Commands.ProjectionTypes
{
    public class EfUpdateProjectionTypeCommand:EfUseCase, IUpdateProjectionTypeCommand
    {
        public int Id => 2;
        public string Name => "Update projection type";

        private UpdateProjectionTypeDtoValidator _validator;

        public EfUpdateProjectionTypeCommand(AspContext context,  UpdateProjectionTypeDtoValidator validator):base(context) { _validator = validator; }

        public void Execute(UpdateProjectionTypeDto request)
        {
            _validator.ValidateAndThrow(request);

            var ProjectionType = Context.ProjectionTypes.FirstOrDefault(x => x.Id == request.Id);
            if(ProjectionType == null)
            {
                throw new EntityNotFoundException("Projection type", request.Id);
            }
            ProjectionType.Multiplier = request.Multiplier ?? ProjectionType.Multiplier;
            ProjectionType.Name = request.Name ?? ProjectionType.Name;
            ProjectionType.Multiplier = request.Multiplier ?? ProjectionType.Multiplier;

            Context.SaveChanges();
        }
    }
}
