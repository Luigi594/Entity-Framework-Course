using EFCoreCourse.Server.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCourse.Server.Cruds
{
    public class MessagesCrudController
    {
        public class SendMessage
        {
            public class SendMessageCommand : IRequest<ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                public Guid SenderId { get; set; }
                public Guid ReceiverId { get; set; }
                public string Text { get; set; }
            }

            public class SendMessageCommandHandler(ApplicationDbContext context)
                : IRequestHandler<SendMessageCommand, ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Handle(SendMessageCommand command, CancellationToken cancellationToken)
                {
                    if (command.SenderId == command.ReceiverId)
                    {
                        return EndpointResponses.ResponseWithSimpleMessage
                            .Create("Sender and Receiver cannot be the same person.");
                    }

                    var sender = await _context.Person.FirstOrDefaultAsync(x => x.Id == command.SenderId, cancellationToken);

                    if (sender is null)
                    {
                        return EndpointResponses.ResponseWithSimpleMessage
                            .Create($"The sender with id {command.SenderId} does not exist.");
                    }

                    var receiver = await _context.Person.FirstOrDefaultAsync(x => x.Id == command.ReceiverId, cancellationToken);

                    if (receiver is null)
                    {
                        return EndpointResponses.ResponseWithSimpleMessage
                            .Create($"The receiver with id {command.ReceiverId} does not exist.");
                    }

                    var message = Entities.Messages.Create(command.SenderId, command.ReceiverId, command.Text);

                    await _context.Messages.AddAsync(message, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    return EndpointResponses.ResponseWithSimpleMessage
                        .Create("Message sent successfully.");
                }
            }
        }
    }
}
