using System.ComponentModel.DataAnnotations;
using MoneyFlow.Utils.Validator;
using MoneyFlow.Constants;
using MoneyFlow.Constants.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyFlow.Data
{
    public class Expense : Entity
    {
        [Required(ErrorMessage = ErrorMessage.USER_NOT_FOUND)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserData { get; set; }

        [Required(ErrorMessage = ErrorMessage.EXPENSE_NAME_EMPTY)]
        [StringLength(255, ErrorMessage = ErrorMessage.EXPENSE_NAME_INVALID)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorMessage.EXPENSE_COST_EMPTY)]
        [ExpenseCostValidation(ErrorMessage = ErrorMessage.EXPENSE_COST_INVALID)]
        public long Cost { get; set; }

        [StringLength(255, ErrorMessage = ErrorMessage.EXPENSE_RECEIPT_INVALID)]
        public string ReceiptFile { get; set; }

        [Required(ErrorMessage = ErrorMessage.EXPENSE_TYPE_EMPTY)]
        [ExpenseCostTypeValidation(ErrorMessage = ErrorMessage.EXPENSE_TYPE_EMPTY)]
        public CostTypeEnum CostType { get; set; }
    }
}