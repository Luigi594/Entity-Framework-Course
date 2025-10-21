using EFCoreCourse.Entities;
using EFCoreCourse.Server.Utilities;
using EFCoreCourse.Utils;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Server.Cruds
{
    public class CustomerCrudController
    {
        public class Create
        {
            public class CreateCustomerCommand : IRequest<ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                public string Name { get; set; }
                public string LastName { get; set; }
                public DateTime BirthDate { get; set; }
            }

            public class Validator : AbstractValidator<CreateCustomerCommand>
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
                        .Must(AgeValidator.BeAtLeast18YearsOld).WithMessage("You must be at least 18 years old to create a profile.");
                }
            }

            public class CreateCustomerCommandCommandHandler(ApplicationDbContext context)
                : IRequestHandler<CreateCustomerCommand, ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
                {
                    string message;

                    if (command.BirthDate > DateTime.Today)
                    {
                        message = "The BirthDate cannot be in the future.";
                        return EndpointResponses.ResponseWithSimpleMessage
                            .Create(message);
                    }

                    // I need to generate a unique CustomerCode for the new customer
                    var customerCode = CodeGenerator.Generate(command.Name, command.LastName);

                    var newCustomer = Customer.Create(command.Name, command.LastName, command.BirthDate, customerCode);
                    await _context.Person.AddAsync(newCustomer, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    message = $"Customer: {newCustomer.Name} {newCustomer.LastName} created successfully.";

                    return EndpointResponses.ResponseWithSimpleMessage.Create(message);
                }
            }
        }
    }
}
