using EFCoreCourse.Entities;
using EFCoreCourse.Server.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Server.Cruds
{
    public class CreditCardPaymentCrudController
    {
        public class CreateCreditCardPayment
        {
            public class CreateCreditCardPaymentCommand : IRequest<ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                public string CardNumber { get; set; }
                public string CardHolderName { get; set; }
                public decimal Amount { get; set; }
            }

            public class Validator : AbstractValidator<CreateCreditCardPaymentCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.CardHolderName)
                        .NotEmpty().WithMessage("The Card Holder Name is required.")
                        .MaximumLength(100).WithMessage("Card Holder Name must less than 100 characters.");
                }
            }

            public class CreateCreditCardPaymentCommandHandler(ApplicationDbContext context)
                : IRequestHandler<CreateCreditCardPaymentCommand, ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Handle(CreateCreditCardPaymentCommand command, CancellationToken cancellationToken)
                {
                    if (command.Amount <= 0)
                    {
                        string message = "Come on dude, you gotta be serious. Your paymment cannot be less than zero.";
                        return EndpointResponses.ResponseWithSimpleMessage.Create(message);
                    }

                    if(command.CardNumber.Length < 4)
                    {
                        string message = "Credit card not valid.";
                        return EndpointResponses.ResponseWithSimpleMessage.Create(message);
                    }

                    var creditCardPayment = CreditCardPayment.Create(command.CardNumber, command.CardHolderName, command.Amount);
                    await _context.AddAsync(creditCardPayment, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    return EndpointResponses.ResponseWithSimpleMessage.Create("Payment confirmed successfully.");
                }
            }
        }
    }
}
