using System;
using System.ComponentModel.DataAnnotations;
using iv = MoneyFlow.Utils.Validator.InputValidator;

namespace MoneyFlow.Utils.Validator
{
    public class UsernameValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {   
            try 
            {
                return !string.IsNullOrWhiteSpace(value as string)
                    && iv.IsUsernameValid(value.ToString());
            } catch (Exception)
            {
                return false;
            }
        }
    }

    public class PasswordValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {   
            try
            {
                return !string.IsNullOrWhiteSpace(value as string)
                    && iv.IsPasswordValid(value.ToString());
            } catch (Exception)
            {
                return false;
            }
        }
    }

    public class EmailValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try 
            {
                return !string.IsNullOrWhiteSpace(value as string)
                    && iv.IsEmailValid(value.ToString());

            } catch (Exception)
            {
                return false;
            }
        }
    }

    public class FullNameValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                return !string.IsNullOrWhiteSpace(value as string)
                    && iv.IsFullnameValid(value.ToString());
            } catch (Exception)
            {
                return false;
            }
        }
    }
}