using System.ComponentModel.DataAnnotations;
using MoneyFlow.Constants;
using MoneyFlow.Utils;

namespace MoneyFlow.Models
{
    public class Product : Entity
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = ErrorMessage.PRODUCT_NAME_EMPTY)]
        [StringLength(255)]
        public string Name { get; set; } = "";

        [Required(AllowEmptyStrings = true, ErrorMessage = ErrorMessage.PRODUCT_TYPE_EMPTY)]
        public ProductTypeEnum ProductType{ get; set; } = 0;

        [Required(ErrorMessage = ErrorMessage.PRODUCT_COST_EMPTY)]
        [PriceValidation(ErrorMessage = ErrorMessage.PRODUCT_COST_INVALID)]
        public int Price { get; set; } = 0;

        public string ImageUrl { get; set; } = "";
    }
}