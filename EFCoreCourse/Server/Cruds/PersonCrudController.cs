using EFCoreCourse.Entities;
using EFCoreCourse.Server.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Server.Cruds
{
    public class PersonCrudController
    {
        public class Create
        {
            public class CreateCommand : IRequest<ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                public string Name { get; set; }
                public string LastName { get; set; }
                public DateTime BirthDate { get; set; }
            }

            public class Validator : AbstractValidator<CreateCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("The Name is required.")
                        .MaximumLength(100).WithMessage("The Name must not exceed 100 characters.");

                    RuleFor(x => x.LastName)
                        .NotEmpty().WithMessage("The Name is required.")
                        .MaximumLength(100).WithMessage("The Name must not exceed 100 characters.");

                    RuleFor(x => x.BirthDate)
                        .NotEmpty().WithMessage("The BirthDate is required.")
                        .Must(BeAtLeast18YearsOld).WithMessage("You must be at least 18 years old to create a profile.");
                }

                private bool BeAtLeast18YearsOld(DateTime birthDate)
                {
                    var today = DateTime.Today;
                    var age = today.Year - birthDate.Year;
                    if (birthDate.Date > today.AddYears(-age)) age--;
                    return age >= 18;
                }
            }

            public class CreateCommandHandler(ApplicationDbContext context)
                : IRequestHandler<CreateCommand, ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Handle(CreateCommand command, CancellationToken cancellationToken)
                {
                    var message = "";

                    if(command.BirthDate > DateTime.Today)
                    {
                        message = "The BirthDate cannot be in the future.";
                        return EndpointResponses.ResponseWithSimpleMessage
                            .Create(message);
                    }

                    var newPerson = Person.Create(command.Name, command.LastName, command.BirthDate);
                    await _context.Person.AddAsync(newPerson, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    message = $"Person: {newPerson.Name} {newPerson.LastName} created successfully.";

                    return EndpointResponses.ResponseWithSimpleMessage
                        .Create(message);
                }
            }
        }
    }
}
