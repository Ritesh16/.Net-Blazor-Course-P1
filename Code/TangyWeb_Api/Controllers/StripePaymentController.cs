using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Tangy_Models.Dtos;

namespace TangyWeb_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripePaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDto stripePaymentDto)
        {
            try
            {
                var domain = _configuration.GetValue<string>("Tangy_Client_URL");

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + stripePaymentDto.SuccessUrl,
                    CancelUrl = domain + stripePaymentDto.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };


                foreach (var item in stripePaymentDto.Order.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100), //20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);
                return Ok(new SuccessModelDto()
                {
                    Data = session.Id + ";" + session.PaymentIntentId
                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModelDto()
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
