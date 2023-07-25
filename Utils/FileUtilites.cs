using System;
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
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
            string filePath = Path.Combine(_environment.ContentRootPath, "UserData", ownerId, purpose);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            using (var file = File.Create($"{filePath}/{fileName}"))
            {
                await formFile.CopyToAsync(file);
            }
            return fileName;
        }
    }
}