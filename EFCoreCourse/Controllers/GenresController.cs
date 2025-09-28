using EFCoreCourse.Server.Cruds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/Genres/[Action]")]
    public class GenresController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetGenres([FromQuery] GenresCrudController.GetGenres.GetGenresQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetGenresByDescription([FromQuery] GenresCrudController.GetGenresByDescription.GetGenresByDescriptionQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        // Biography, Horror, Comedy, Drama, Action, Romance
        [HttpPost]
        public async Task<ActionResult> CreateGenre([FromBody] GenresCrudController.CreateGenre.CreateGenreCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateGenresByList([FromBody] GenresCrudController.CreateGenresByList.CreateGenresByListCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
