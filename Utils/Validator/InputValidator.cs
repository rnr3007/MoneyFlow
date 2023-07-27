using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using MoneyFlow.Constants;

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

        public static bool IsFileValid(IFormFile formFile, string[] permittedFileTypes)
        {
            List<string> imageExt = new List<string>{".png", ".jpg", ".jpeg"};
            List<string> pdfExt = new List<string>{".pdf", ".pdfa", ".pdfx", ".pdfe", ".pdfua"};
            List<string> excelExt = new List<string>{".xls", ".xlsx", ".xlsm", ".xlsb", ".csv", ".xltx", ".xltm", ".xlshtml"};
            int falseResult = 0;
            if (formFile.Length > 5242880)
            {
                throw new InvalidDataException(ErrorMessage.FILE_SIZE_INVALID);
            }
            foreach (string permittedFileType in permittedFileTypes)
            {
                switch (permittedFileType)
                {
                    case "image":
                        falseResult = imageExt.Exists(x => x == Path.GetExtension(formFile.FileName).ToLower()) 
                            ? falseResult 
                            : falseResult + 1;
                        break;
                    case "pdf":
                        falseResult = pdfExt.Exists(x => x == Path.GetExtension(formFile.FileName).ToLower()) 
                            ? falseResult 
                            : falseResult + 1;
                        break;
                    case "excel":
                        falseResult = excelExt.Exists(x => x == Path.GetExtension(formFile.FileName).ToLower()) 
                            ? falseResult 
                            : falseResult + 1;
                        break;
                }
            }
            return falseResult < permittedFileTypes.Length
                ? true 
                : throw new InvalidDataException(ErrorMessage.FILE_TYPE_INVALID);
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