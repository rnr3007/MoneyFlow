using System.ComponentModel.DataAnnotations;
using MoneyFlow.Constants;
using MoneyFlow.Utils.Validator;

namespace MoneyFlow.Data
{
    public class User : Entity
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = ErrorMessage.USERNAME_EMPTY)]
        [StringLength(64)]
        [UsernameValidation(ErrorMessage = ErrorMessage.FULLNAME_FORMAT_INVALID)]
        public string Username { get; set;} = "";

        [Required(AllowEmptyStrings = true, ErrorMessage = ErrorMessage.FULLNAME_EMPTY)]
        [StringLength(255)]
        [FullNameValidation(ErrorMessage = ErrorMessage.FULLNAME_FORMAT_INVALID)]
        public string FullName { get; set;} = "";

        [Required(AllowEmptyStrings = true, ErrorMessage = ErrorMessage.PASSWORD_EMPTY),]
        [StringLength(64)]
        [PasswordValidation(ErrorMessage = ErrorMessage.PASSWORD_FORMAT_INVALID)]
        public string Password { get; set;} = "";

        [Required(AllowEmptyStrings = true, ErrorMessage = ErrorMessage.EMAIL_EMPTY)]
        [StringLength(255)]
        [EmailValidation(ErrorMessage = ErrorMessage.EMAIL_FORMAT_INVALID)]
        public string Email { get; set;} = "";
    }
}