using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreCourse.Entities;
using EFCoreCourse.Server.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static EFCoreCourse.Entities.Actors;

namespace EFCoreCourse.Server.Cruds
{
    public class ActorsCrudController
    {
        public class GetActors
        {
            public class  GetActorsQuery: IRequest<List<ActorsDTO>>
            {
                
            }
            public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetActorsQuery, List<ActorsDTO>>
            {
                private readonly ApplicationDbContext _context = context;
                private readonly IMapper _mapper = mapper;

                public async Task<List<ActorsDTO>> Handle(GetActorsQuery request, CancellationToken cancellationToken)
                {
                    var actors = await _context.Actors
                        .ProjectTo<ActorsDTO>(_mapper.ConfigurationProvider)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);

                    return actors;
                }
            }
        }

        public class Create
        {
            public class CreateActorCommand : IRequest<ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                public string Name { get; set; }
                public string LastName { get; set; }
                public string Biography { get; set; }

                public DateTime BirthDate { get; set; }
            }

            public class Validator : AbstractValidator<CreateActorCommand>
            {
                public Validator()
                {
                    RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("The name is required.")
                        .MaximumLength(50).WithMessage("The name must not exceed 50 characters.");

                    RuleFor(x => x.LastName)
                        .NotEmpty().WithMessage("The lastname is required.")
                        .MaximumLength(50).WithMessage("The lastname must not exceed 50 characters.");
                }
            }

            public class CreateActorCommandHandler(ApplicationDbContext context)
                : IRequestHandler<CreateActorCommand, ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Handle(CreateActorCommand command, CancellationToken cancellationToken)
                {
                    var existingActor = await _context.Actors
                        .Where(x => x.Name == command.Name && x.LastName == command.LastName)
                        .FirstOrDefaultAsync(cancellationToken);

                    if(existingActor != null)
                        return EndpointResponses.ResponseWithSimpleMessage.Create($"The actor {command.Name} {command.LastName} already exists.");

                    var newActor = Actors.Create(command.Name, command.LastName, command.Biography, command.BirthDate);
                    await _context.Actors.AddAsync(newActor, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    var message = $"New Actor: {newActor.Name} {newActor.LastName} was created successfully!";
                    return EndpointResponses.ResponseWithSimpleMessage.Create(message); 
                }
            }
        }
    }
}
