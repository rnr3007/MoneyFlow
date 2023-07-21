using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Utils
{
    public class DataExtractor
    {
        public static List<string> GetEnumDataList<TEnum>() where TEnum : Enum
        {
            List<string> enumDataList = new List<string>();
            Array enumDatas = Enum.GetValues(typeof(TEnum));
            foreach (var enumData in enumDatas)
            {
                enumDataList.Add(enumData.ToString());
            }
            return enumDataList;
        }
    }
}