using TangyWeb_Client.ViewModels;

namespace TangyWeb_Client.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task IncrementCart(ShoppingCartViewModel shoppingCartToIncrement);
        Task DecrementCart(ShoppingCartViewModel shoppingCartToDecrement);
    }
}
