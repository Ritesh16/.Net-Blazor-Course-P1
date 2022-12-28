using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models.Dtos
{
    public class OrderDto
    {
        public OrderHeaderDto OrderHeader { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
