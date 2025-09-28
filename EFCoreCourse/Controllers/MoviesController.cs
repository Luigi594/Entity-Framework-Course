using EFCoreCourse.Server.Cruds;
using EFCoreCourse.Server.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/Movies/[Action]")]
    public class MoviesController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetMovies([FromQuery] MoviesCrudController.GetMovies.GetMoviesQuery query)
        {
            var resp = await _mediator.Send(query);
            return Ok(resp);
        }

        [HttpGet]
        public async Task<ActionResult> GetMoviesGroupedByIsOnDisplay([FromQuery] MoviesCrudController.GetMoviesGroupedByIsOnDisplay.GetMoviesGroupedByIsOnDisplayQuery query)
        {
            var resp = await _mediator.Send(query);
            return Ok(resp);
        }

        [HttpPost]
        public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> CreateNewMovie([FromBody] MoviesCrudController.CreateNewMovie.CreateNewMovieCommand command)
        {
            var resp = await _mediator.Send(command);
            return Ok(resp);
        }
    }
}
