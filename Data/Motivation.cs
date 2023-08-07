using System.ComponentModel.DataAnnotations;
using MoneyFlow.Utils.Validator;
using MoneyFlow.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyFlow.Data
{
    public class Motivation : Entity
    {
        [Required(ErrorMessage = ErrorMessage.USER_NOT_FOUND)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserData { get; set; }

        [Required(ErrorMessage = ErrorMessage.TARGET_NAME_EMPTY)]
        [TargetNameValidator(ErrorMessage = ErrorMessage.TARGET_NAME_INVALID)]
        public string TargetName { get; set; }

        [Required(ErrorMessage = ErrorMessage.TARGET_PRICE_EMPTY)]
        [TargetPriceValidator(ErrorMessage = ErrorMessage.TARGET_PRICE_INVALID)]
        public long TargetPrice { get; set; }

        [Required(ErrorMessage = ErrorMessage.TARGET_IMAGE_EMPTY, AllowEmptyStrings = true)]
        public string TargetImage { get; set; } = "";

        [StringLength(512, ErrorMessage = ErrorMessage.TARGET_DESCRIPTION_INVALID)]
        public string Description { get; set; }
    }
}