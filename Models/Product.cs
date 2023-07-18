using System.ComponentModel.DataAnnotations;
using MoneyFlow.Constants;

namespace MoneyFlow.Models
{
    public class Product : Entity
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public ProductTypeEnum ProductType{ get; set; }

        [Required]
        public int Price { get; set; }
    }
}