using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models.Dtos
{
    public class StripePaymentDto
    {
        public StripePaymentDto()
        {
            SuccessUrl = "OrderConfirmation";
            CancelUrl = "Summary";
        }
        public OrderDto Order { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
