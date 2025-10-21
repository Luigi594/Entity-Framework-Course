using EFCoreCourse.Entities;
using EFCoreCourse.Server.Utilities;
using EFCoreCourse.Utils;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Server.Cruds
{
    public class PayPalPaymentCrudController
    {
        public class CreatePayPalPayment
        {
            public class CreatePayPalPaymentCommand : IRequest<ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                public string Email { get; set; }
                public decimal Amount { get; set; }
            }

            public class Validator : AbstractValidator<CreatePayPalPaymentCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Email)
                        .NotEmpty().WithMessage("The Email is required.")
                        .Must(EmailValidator.IsValidEmail).WithMessage("The Email is not valid!");
                }
            }

            public class CreatePayPalPaymentCommandCommandHandler(ApplicationDbContext context)
                : IRequestHandler<CreatePayPalPaymentCommand, ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Handle(CreatePayPalPaymentCommand command, CancellationToken cancellationToken)
                {
                    if (command.Amount <= 0)
                    {
                        string message = "Come on dude, you gotta be serious. Your paymment cannot be less than zero.";
                        return EndpointResponses.ResponseWithSimpleMessage.Create(message);
                    }

                    var payPayPayment = PayPalPayment.Create(command.Email, command.Amount);
                    await _context.AddAsync(payPayPayment, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    return EndpointResponses.ResponseWithSimpleMessage.Create("Payment confirmed successfully.");
                }
            }
        }
    }
}
