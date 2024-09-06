using AspProjekat.Application.DTO;
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
    public class EfCreateProjectionTypeCommand : EfUseCase, ICreateProjectionTypeCommand
    {
        public int Id => 2;
        public string Name => "Create projection type";

        private CreateProjectionTypeDtoValidator _validator;

        public EfCreateProjectionTypeCommand(AspContext context, CreateProjectionTypeDtoValidator validator) : base(context) { _validator = validator; }

        public void Execute(CreateProjectionTypeDto data)
        {
            _validator.ValidateAndThrow(data);

            var ProjectionType = new AspProjekat.Domain.ProjectionType
            {
                Name = data.Name,
                Multiplier = data.Multiplier
            };
            Context.ProjectionTypes.Add(ProjectionType);
            Context.SaveChanges();
        }
    }
}
