using EFCoreCourse.Server.GenresCrud;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/Genres")]
    public class GenresController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetGenres([FromQuery] GenresCrudController.GetGenres.GetGenresQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetGenresByDescription")]
        public async Task<ActionResult> GetGenresByDescription([FromQuery] GenresCrudController.GetGenresByDescription.GetGenresByDescriptionQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
