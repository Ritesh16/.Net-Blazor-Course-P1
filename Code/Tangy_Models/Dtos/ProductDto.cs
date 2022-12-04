using System.ComponentModel.DataAnnotations;

namespace Tangy_Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool ShopFavorites { get; set; }
        public bool CustomerFavorites { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }

    }
}
