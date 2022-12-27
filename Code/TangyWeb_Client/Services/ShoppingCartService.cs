using Blazored.LocalStorage;
using Tangy_Common;
using TangyWeb_Client.Services.Interfaces;
using TangyWeb_Client.ViewModels;

namespace TangyWeb_Client.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ILocalStorageService localStorage;

        public event Action OnChange;
        public ShoppingCartService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task DecrementCart(ShoppingCartViewModel cartToDecrement)
        {
            var cart = await localStorage.GetItemAsync<List<ShoppingCartViewModel>>(Constants.ShoppingCart);

            //if count is 0 or 1 then we remove the item
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == cartToDecrement.ProductId && cart[i].ProductPriceId == cartToDecrement.ProductPriceId)
                {
                    if (cart[i].Count == 1 || cartToDecrement.Count == 0)
                    {
                        cart.Remove(cart[i]);
                    }
                    else
                    {
                        cart[i].Count -= cartToDecrement.Count;
                    }
                }
            }

            await localStorage.SetItemAsync(Constants.ShoppingCart, cart);
            OnChange.Invoke();
        }

        public async Task IncrementCart(ShoppingCartViewModel cartToAdd)
        {
            var cartItems = await localStorage.GetItemAsync<List<ShoppingCartViewModel>>(Constants.ShoppingCart);
            bool itemInCart = false;

            if (cartItems == null)
            {
                cartItems = new List<ShoppingCartViewModel>();
            }

            foreach (var cartItem in cartItems)
            {
                if (cartItem.ProductId == cartToAdd.ProductId && cartItem.ProductPriceId == cartToAdd.ProductPriceId)
                {
                    itemInCart = true;
                    cartItem.Count += cartToAdd.Count;
                }
            }
            if (!itemInCart)
            {
                cartItems.Add(new ShoppingCartViewModel()
                {
                    ProductId = cartToAdd.ProductId,
                    ProductPriceId = cartToAdd.ProductPriceId,
                    Count = cartToAdd.Count,
                    Product = cartToAdd.Product,
                    ProductPrice = cartToAdd.ProductPrice
                });
            }
            await localStorage.SetItemAsync(Constants.ShoppingCart, cartItems);
            OnChange.Invoke();
        }
    }
}
