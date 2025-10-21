using EFCoreCourse.Server.Cruds;
using EFCoreCourse.Server.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/Customer/[Action]")]
    public class CustomerController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> Create([FromBody] CustomerCrudController.Create.CreateCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
