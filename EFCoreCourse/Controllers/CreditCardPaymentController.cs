using EFCoreCourse.Server.Cruds;
using EFCoreCourse.Server.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCourse.Controllers
{
    [ApiController]
    [Route("Api/CreditCardPayment/[Action]")]
    public class CreditCardPaymentController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<EndpointResponses.ResponseWithSimpleMessage>> CreateCreditCardPayment([FromBody] CreditCardPaymentCrudController.CreateCreditCardPayment.CreateCreditCardPaymentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
