using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PowerslideWebsite.Utilities
{
    public class FileUploadHelper
    {
        public static async Task<bool> ProcessFormFile(IFormFile file, string dirName)
        {
            if (file.Length > 0)
            {
                var filePath = Path.Combine(dirName, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    return true;
                }
            }

            return false;
        }

        public static async Task<bool> ProcessBeatmapUpload(IList<IFormFile> files, string dirName)
        {
            foreach (var file in files)
            {
                var filePath = Path.Combine(dirName, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return false;
        }

        // Creates the downloadable zip that players can use.
        // Broken (Broken, No Files in the Archive...)
        public static void CreateZippedFile(string dirName)
        {
            string zipName = Path.Combine(dirName, "beatmap.zip");

            using (ZipArchive zip = ZipFile.Open(zipName, ZipArchiveMode.Create))
            {
                foreach (var file in Directory.GetFiles(dirName))
                {

                    var filePath = Path.Combine(dirName, file);
                    try
                    {
                        zip.CreateEntryFromFile(filePath, file);
                    }
                    
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        // Broken (Access to the path 'E:\Development\PowerslideWebsite\PowerslideWebsite\wwwroot\downloads' is denied)
        public static void CreateZipFromDirectory(string dirName, string downloadDirectory)
        {
            string zipName = Path.Combine(downloadDirectory, "beatmap.zip");

            ZipFile.CreateFromDirectory(dirName, zipName);
        }
    }
}

