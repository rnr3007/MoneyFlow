using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MoneyFlow.Utils
{
    public class FileUtilites
    {
        private static IWebHostEnvironment _environment;

        public static void Configure(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public static async Task<string> SaveFile(string purpose, IFormFile formFile, string ownerId = "")
        {
            string filePath = $"UserData/{ownerId}/{purpose}";
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
            string storagePath = Path.Combine(_environment.ContentRootPath, filePath);
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }
            using (var file = File.Create($"{storagePath}/{fileName}"))
            {
                await formFile.CopyToAsync(file);
            }
            return fileName;
        }

        public static async Task<string> UpdateFile(string purpose, IFormFile formFile, string fileName, string ownerId = "")
        {
            string filePath = $"UserData/{ownerId}/{purpose}";
            if (fileName == null || fileName == "")
            {
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
            }
            string storagePath = Path.Combine(_environment.ContentRootPath, filePath);
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }
            using (var file = File.Create($"{storagePath}/{fileName}"))
            {
                await formFile.CopyToAsync(file);
            }
            return fileName;
        }

        public static async Task<byte[]> GetFileByte(string filePath)
        {
            string storedFilePath = Path.Combine(_environment.ContentRootPath, filePath);
            byte[] file = await File.ReadAllBytesAsync(storedFilePath);
            return file;
        }

        public static string GetFileType(string fileExtension)
        {
            List<string> imageExt = new List<string>{".png", ".jpg", ".jpeg"};
            List<string> pdfExt = new List<string>{".pdf", ".pdfa", ".pdfx", ".pdfe", ".pdfua"};
            List<string> excelExt = new List<string>{".xls", ".xlsx", ".xlsm", ".xlsb", ".csv", ".xltx", ".xltm", ".xlshtml"};
            if (imageExt.Exists(x => x == fileExtension))
            {
                return "image";
            } else if (pdfExt.Exists(x => x == fileExtension))
            {
                return "pdf";
            } else if (excelExt.Exists(x => x == fileExtension))
            {
                return "excel";
            }
            return "none";
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }
    }
}