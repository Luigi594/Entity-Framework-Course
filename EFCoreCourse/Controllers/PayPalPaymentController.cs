using EFCoreCourse.Server.Cruds;
using EFCoreCourse.Server.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/PayPalPayment/[Action]")]
    public class PayPalPaymentController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> CreatePayPalPayment([FromBody] PayPalPaymentCrudController.CreatePayPalPayment.CreatePayPalPaymentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
