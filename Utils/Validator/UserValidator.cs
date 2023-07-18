using System.ComponentModel.DataAnnotations;
using iv = MoneyFlow.Utils.InputValidator;

namespace MoneyFlow.Utils.Validator
{
    public class UsernameValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {   
            return !string.IsNullOrWhiteSpace(value as string)
                && iv.IsUsernameValid(value.ToString());
        }
    }

    public class PasswordValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {   
            return !string.IsNullOrWhiteSpace(value as string)
                && iv.IsPasswordValid(value.ToString());
        }
    }

    public class EmailValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return !string.IsNullOrWhiteSpace(value as string)
                && iv.IsEmailValid(value.ToString());
        }
    }

    public class FullNameValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return !string.IsNullOrWhiteSpace(value as string)
                && iv.IsFullnameValid(value.ToString());
        }
    }
}