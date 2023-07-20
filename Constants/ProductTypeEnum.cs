using System;

namespace MoneyFlow.Constants
{
    public enum ProductTypeEnum
    {
        [StringValue("")]
        NONE,

        [StringValue("Makanan")]
        FOOD_AND_BEVERAGE,

        [StringValue("Pelayanan")]
        SERVICE,

        [StringValue("Fashion")]
        FASHION,
    }

    public class StringValueAttribute : Attribute
    {
        public string Value { get; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }
}