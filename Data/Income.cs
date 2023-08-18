using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MoneyFlow.Constants;
using MoneyFlow.Constants.Enum;
using MoneyFlow.Utils.Validator;

namespace MoneyFlow.Data
{
    public class Income : Entity
    {
        [Required(ErrorMessage = ErrorMessage.USER_NOT_FOUND)]
        [StringLength(100)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserData { get; set; }

        [Required(ErrorMessage = ErrorMessage.INCOME_TYPE_EMPTY)]
        [IncomeValidation]
        public long IncomeMoney { get; set; }

        [IncomeTypeValidation(ErrorMessage = ErrorMessage.EXPENSE_TYPE_EMPTY)]
        [Required(ErrorMessage = ErrorMessage.INCOME_TYPE_EMPTY)]
        public IncomeTypeEnum IncomeType { get; set; }
    }
}