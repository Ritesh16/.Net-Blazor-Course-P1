using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Tangy_Business.Repository.Interfaces;
using Tangy_Models.Dtos;

namespace TangyWeb_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetAll());
        }

        [HttpGet("{orderHeaderId}")]
        public async Task<IActionResult> Get(int? orderHeaderId)
        {
            if (orderHeaderId == null || orderHeaderId == 0)
            {
                return BadRequest(new ErrorModelDto()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var orderHeader = await _orderRepository.Get(orderHeaderId.Value);
            if (orderHeader == null)
            {
                return BadRequest(new ErrorModelDto()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(orderHeader);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] StripePaymentDto stripePaymentDto)
        {
            stripePaymentDto.Order.OrderHeader.OrderDate = DateTime.Now;
            var result = await _orderRepository.Create(stripePaymentDto.Order);
            return Ok(result);
        }

        [HttpPost("PaymentSuccessful")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDto orderHeaderDto)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(orderHeaderDto.SessionId);

            if(sessionDetails.PaymentStatus.ToLower() == "paid")
            {
                var result = await _orderRepository.MarkPaymentSuccessful(orderHeaderDto.Id, sessionDetails.PaymentIntentId);
                if (result == null)
                {
                    return BadRequest(new ErrorModelDto()
                    {
                        ErrorMessage = "Cannot mark order as confirmed."
                    });
                }

                return Ok(result);
            }

            return BadRequest();
        }
    }
}
