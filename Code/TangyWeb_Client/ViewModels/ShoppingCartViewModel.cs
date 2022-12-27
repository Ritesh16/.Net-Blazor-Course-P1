using Tangy_Models.Dtos;

namespace TangyWeb_Client.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int ProductPriceId { get; set; }
        public ProductPriceDto ProductPrice { get; set; }
        public int Count { get; set; }
    }
}
