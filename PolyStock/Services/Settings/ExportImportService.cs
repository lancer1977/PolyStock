using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PolyStock.Services.Settings
{
    public class ExportImportService : IExportImportService
    {
        public async Task<string> Load()
        {
            var result = await GetFile(PickOptions.Default);
            using var stream = await FileSystem.OpenAppPackageFileAsync(result.FileName);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
        public async Task Save(string value)
        {
            var result = await GetFile(PickOptions.Default);
            using var stream = await FileSystem.OpenAppPackageFileAsync(result.FileName);
            using var reader = new StreamWriter(stream);
            await reader.WriteAsync(value);
        }

        public async Task<FileResult> GetFile(PickOptions options)
        { 
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }
    }
}