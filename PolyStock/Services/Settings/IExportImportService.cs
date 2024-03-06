using System.Threading.Tasks;

namespace PolyStock.Services.Settings
{
    public interface IExportImportService
    {
        Task<string> Load();
        Task Save(string value);
    }
}