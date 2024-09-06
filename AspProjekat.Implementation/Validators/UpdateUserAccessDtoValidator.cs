using AspProjekat.Application.DTO;
using AspProjekat.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspProjekat.Implementation.Validators
{
    public class UpdateUserAccessDtoValidator : AbstractValidator<UpdateUserAccessDto>
    {
        private static int updateUserAccessId = 4;
        public UpdateUserAccessDtoValidator(AspContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.RoleId)
                    .Must(x => context.Roles.Any(r => r.Id == x && r.IsActive))
                    .WithMessage("Requested role doesn't exist.")
                    .Must(x => !context.UserUseCases.Any(u => u.UseCaseId == updateUserAccessId && u.RoleId == x))
                    .WithMessage("Not allowed to change this role.");

            RuleFor(x => x.UseCaseIds)
                .NotEmpty().WithMessage("Parameter is required.")
                .Must(x => x.All(id => id > 0 && id <= UseCaseInfo.MaxUseCaseId)).WithMessage("Invalid usecase id range.")
                .Must(x => x.Distinct().Count() == x.Count()).WithMessage("Only unique usecase ids must be delivered.");


        }
    }
}
