using System.ComponentModel.Design;
using Tangy_Models.Dtos;

namespace TangyWeb_Client.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task<SuccessModelDto> Checkout(StripePaymentDto paymentDto);
    }
}
