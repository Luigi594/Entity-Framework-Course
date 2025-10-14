using EFCoreCourse.Server.Cruds;
using EFCoreCourse.Server.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/Person/[Action]")]
    public class PersonController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Create([FromBody] PersonCrudController.Create.CreateCommand command)
        {
            var resp = await _mediator.Send(command);
            return Ok(resp);
        }
    }

}
