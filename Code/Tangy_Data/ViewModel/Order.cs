using Tangy_Data.Entities;

namespace Tangy_Data.ViewModel
{
    public class Order
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
