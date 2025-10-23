using EFCoreCourse.Server.Utilities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCourse.Server.Cruds
{
    public class MovieRentalCrudController
    {
        public class CreateRentMovie
        {
            public class CreateRentMovieCommand : IRequest<ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                
            }

            public class CreateRentMovieCommandHandler(ApplicationDbContext context)
                : IRequestHandler<CreateRentMovieCommand, ActionResult<EndpointResponses.ResponseWithSimpleMessage>>
            {
                private readonly ApplicationDbContext _context = context;

                public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Handle(CreateRentMovieCommand command, CancellationToken cancellationToken)
                {
                    

                    var message = $"Was created successfully!";
                    return EndpointResponses.ResponseWithSimpleMessage.Create(message);
                }
            }
        }
    }
}
