using System;
using System.Text.RegularExpressions;

namespace MoneyFlow.Utils.Validator
{
    public class InputValidator
    {
        public static bool IsPasswordValid(string value)
        {
            string regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[_\W]).{8,64}$";
            return Regex.IsMatch(value, regex);
        }

        public static bool IsUsernameValid(string value)
        {
            string regex = @"^[\w]{3,64}$";
            return Regex.IsMatch(value, regex);
        }

        public static bool IsEmailValid(string value)
        {
            string regex = @"^[a-zA-Z0-9_.]+@[a-zA-Z0-9]+\.[a-zA-Z0-9.]+$";
            return Regex.IsMatch(value, regex);
        }

        public static bool IsFullnameValid(string value)
        {
            string regex = @"^[a-zA-Z ]{1,255}$";
            return Regex.IsMatch(value, regex);
        }

        public static int GetValidIntegerFromString(object value, int defaultValue)
        {
            try 
            {
                return int.Parse((string)value);
            } catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}