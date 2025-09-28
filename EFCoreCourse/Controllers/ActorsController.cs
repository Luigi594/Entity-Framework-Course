using EFCoreCourse.Server.Cruds;
using EFCoreCourse.Server.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/Actors/[Action]")]
    public class ActorsController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetActors([FromQuery] ActorsCrudController.GetActors.GetActorsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Create([FromBody] ActorsCrudController.Create.CreateActorCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
