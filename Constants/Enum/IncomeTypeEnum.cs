using System.Collections.Generic;

namespace MoneyFlow.Constants.Enum
{
    public enum IncomeTypeEnum
    {
        NONE,
        SALARY,
        INVESTMENT,
        BUSINESS
    }

    public class IncomeType
    {
        public static List<string> GetIncomeTypeValues()
        {
            return new List<string>{
                "Gaji",
                "Investasi",
                "Bisnis"
            };
        }

        public static string GetIncomeTypeValue(int id)
        {
            return id switch
            {
                (int)IncomeTypeEnum.SALARY => "Gaji",
                (int)IncomeTypeEnum.INVESTMENT => "Investasi",
                (int)IncomeTypeEnum.BUSINESS => "Bisnis",
                _ => ""
            };
        }
    }
}