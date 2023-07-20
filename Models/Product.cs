using System.ComponentModel.DataAnnotations;
using MoneyFlow.Constants;

namespace MoneyFlow.Models
{
    public class Product : Entity
    {
        [Required(AllowEmptyStrings = true)]
        [StringLength(255)]
        public string Name { get; set; } = "";

        [Required(AllowEmptyStrings = true)]
        public ProductTypeEnum ProductType{ get; set; } = 0;

        [Required(AllowEmptyStrings = true)]
        public int Price { get; set; } = 0;

        [Required(AllowEmptyStrings = true)]
        public string ImageUrl { get; set; } = "";
    }
}