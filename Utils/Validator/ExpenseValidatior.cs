using System;
using System.ComponentModel.DataAnnotations;
using MoneyFlow.Constants.Enum;

namespace MoneyFlow.Utils.Validator
{
    public class ExpenseCostValidation : ValidationAttribute
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

    public class ExpenseCostTypeValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                return !value.Equals(CostTypeEnum.NONE);
            } catch (Exception)
            {
                return false;
            }
        }
    }
}