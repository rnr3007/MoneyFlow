using System;
using System.Collections.Generic;

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

        public static string GetLocaleDateTimeString(DateTime dateTime)
        {
            string[] months = new string[]{"", "Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul", "Agu", "Sep", "Okt", "Nov", "Des"};
            string[] days = new string[]{"", "Senin", "Selasa", "Rabu", "Kamis", "Jumat", "Sabtu", "Minggu"};
            int year = dateTime.ToLocalTime().Year;
            int date = dateTime.ToLocalTime().Day;
            int day = (int)dateTime.ToLocalTime().DayOfWeek;
            int month = dateTime.ToLocalTime().Month;
            int hour = dateTime.ToLocalTime().TimeOfDay.Hours;
            int minute = dateTime.ToLocalTime().TimeOfDay.Minutes;
            return $"{days[day]}, {date} {months[month]} {year}, {hour}:{minute}";
        }
    }
}