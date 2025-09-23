using EFCoreCourse.Server.Cruds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/MoviesTheater/[Action]")]
    public class MoviesTheaterController(IMediator mediator): Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetMoviesTheater([FromQuery] MoviesTheaterCrudController.GetMoviesTheater.GetMoviesTheaterQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetMoviesTheaterNearBy")]
        public async Task<ActionResult> GetMoviesTheaterNearBy([FromQuery] MoviesTheaterCrudController.GetMoviesTheaterNearBy.GetMoviesTheaterNearByQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
