using EFCoreCourse.Server.Cruds;
using EFCoreCourse.Server.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    public class MovieRentalController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> CreateMovieRent([FromBody] MovieRentalCrudController.CreateMovieRent.CreateMovieRentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
