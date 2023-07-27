using System;
using System.ComponentModel.DataAnnotations;
using MoneyFlow.Constants.Enum;

namespace MoneyFlow.Utils.Validator
{
    public class IncomeValidation : ValidationAttribute
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

    public class IncomeTypeValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                return !value.Equals(IncomeTypeEnum.NONE);
            } catch (Exception)
            {
                return false;
            }
        }
    }
}