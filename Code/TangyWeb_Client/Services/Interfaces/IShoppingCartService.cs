using TangyWeb_Client.ViewModels;

namespace TangyWeb_Client.Services.Interfaces
{
    public interface IShoppingCartService
    {
        public event Action OnChange;
        Task IncrementCart(ShoppingCartViewModel shoppingCartToIncrement);
        Task DecrementCart(ShoppingCartViewModel shoppingCartToDecrement);
    }
}
