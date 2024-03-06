using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PolyhydraGames.Core.Extensions;
using PolyhydraGames.SQLite.Interfaces;
using PolyStock.Models;
using PolyStock.Services.Settings;

namespace PolyStock.Services.Basis
{
    public class StockBasisSQLService : PolyhydraGames.SQLite.SqlRepositoryLite<StockBasis, IStockBasis>, IStockBasisService
    {
        private readonly IExportImportService _exportService; 

        public StockBasisSQLService(ISQLiteFactory factory, IExportImportService exportService) : base(factory)
        {
            _exportService = exportService;
 
        }

        public override string Title => "StocksBasis";
        public List<StockBasis> GetAll()
        {

            return Table.ToList();
        }

        public StockBasis GetBasis(string symbol)
        {
            return Table.FirstOrDefault(x => x.Symbol == symbol);
        }

        public IList<string> GetSymbols()
        {
            var results = base.Table.Select(x => x.Symbol);
            return results.ToList();
        }

        public async Task Save()
        {
            var items = GetAll().ToJson();
            Debug.WriteLine(items);
            await _exportService.Save(items);
        }

        public async Task Load()
        {
            var result = await _exportService.Load();
            Load(result);
        }

        private void Load(string result)
        {
            var items = result.FromJson<List<StockBasis>>();
            foreach (var item in items)
            {
                Update(item);
            }
        }
    }
}