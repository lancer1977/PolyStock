using System.Threading.Tasks;

namespace PolyStock.Services
{
    public interface IStockReaderService
    {
        Task<QuoteDto> GetStockAsync(string name);
    }
}