using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Utils
{
    public class PriceValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (
                (int)value > 0
            );
        }
    }
}