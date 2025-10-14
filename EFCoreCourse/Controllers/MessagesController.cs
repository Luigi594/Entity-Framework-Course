using EFCoreCourse.Server.Cruds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/Messages/[Action]")]
    public class MessagesController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> SendMessage([FromBody] MessagesCrudController.SendMessage.SendMessageCommand command)
        {
            var resp = await _mediator.Send(command);
            return Ok(resp);
        }
    }
}
