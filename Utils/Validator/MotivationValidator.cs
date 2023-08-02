using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Utils.Validator
{
    public class TargetNameValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try 
            {
                return value is string && value.ToString().Length <= 255;
            } catch (Exception)
            {
                return false;
            } 
        }
    }

    public class TargetPriceValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                return (long)value > 0;
            } catch (Exception)
            {
                return false;
            }
        }
    }
}