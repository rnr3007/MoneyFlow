using System.Collections.Generic;

namespace MoneyFlow.Constants.Enum
{
    public enum CostTypeEnum
    {
        NONE,
        FOOD,
        PERSONAL_CARE_PRODUCT,
        TRANSPORT,
        ENTERTAINMENT,
        UTILITY,
        MISC
    }

    public class CostType
    {
        public static List<string> GetCostTypeValues()
        {
            return new List<string>{
                "",
                "Makanan",
                "Perawatan diri",
                "Transportasi",
                "Hiburan",
                "Keperluan",
                "Lain-lain"
            };
        }

        public static string GetCostTypeValue(int id)
        {
            return id switch
            {
                (int)CostTypeEnum.FOOD => "Makanan",
                (int)CostTypeEnum.PERSONAL_CARE_PRODUCT => "Perawatan diri",
                (int)CostTypeEnum.TRANSPORT => "Transportasi",
                (int)CostTypeEnum.ENTERTAINMENT => "Hiburan",
                (int)CostTypeEnum.UTILITY => "Keperluan",
                (int)CostTypeEnum.MISC => "Lain-lain",
                _ => "",
            };
        }
    }
}