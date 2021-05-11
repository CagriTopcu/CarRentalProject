using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        static string directory = "wwwroot";

        public static string Add(string fileName, IFormFile file)
        {
            string imageExtension = Path.GetExtension(file.FileName);
            string newFile = Guid.NewGuid().ToString("N") + imageExtension;
            string imageFolder = Path.Combine(directory, fileName);
            string imagePath = Path.Combine(imageFolder, newFile);
            string webImagePath = string.Format("/" + fileName + "/{0}", newFile);

            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            using(FileStream fileStream = File.Create(imagePath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            return webImagePath;
        }

        public static bool Delete(string fileName)
        {
            string fullPath = Path.Combine(fileName);
            if(File.Exists(directory + fullPath))
            {
                File.Delete(directory + fullPath);
                return true;
            }
            return false;
        }
    }
}
