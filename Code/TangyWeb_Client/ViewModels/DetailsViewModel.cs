using System.ComponentModel.DataAnnotations;
using Tangy_Models.Dtos;

namespace TangyWeb_Client.ViewModels
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
            ProductPrice = new();
            Count = 1;
        }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public int Count { get; set; }
        [Required]
        public int SelectedProductPriceId { get; set; }
        public ProductPriceDto ProductPrice { get; set; }
    }
}
