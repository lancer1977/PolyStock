using System.Collections.Generic;
using System.Threading.Tasks;
using PolyhydraGames.Core.Interfaces;
using PolyStock.Models;

namespace PolyStock.Services
{
    public interface IStockBasisService : IRepositoryLite<IStockBasis>
    {
        List<StockBasis> GetAll();

        StockBasis GetBasis(string symbol);
        IList<string> GetSymbols();
        Task Save();
        Task Load();
    }
}