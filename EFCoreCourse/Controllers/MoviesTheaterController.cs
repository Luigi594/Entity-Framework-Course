using EFCoreCourse.Server.Cruds;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static EFCoreCourse.Entities.MovieTheater;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/MoviesTheater/[Action]")]
    public class MoviesTheaterController(IMediator mediator): Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<MoviesTheaterDTO>>> GetMoviesTheater([FromQuery] MoviesTheaterCrudController.GetMoviesTheater.GetMoviesTheaterQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetMoviesTheaterNearBy([FromQuery] MoviesTheaterCrudController.GetMoviesTheaterNearBy.GetMoviesTheaterNearByQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<MoviesTheaterCrudController.CreateMovieTheather.Response>> CreateMovieTheather([FromBody] MoviesTheaterCrudController.CreateMovieTheather.CreateMovieTheatherCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
