using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PolyhydraGames.SQLite.Interfaces;
using PolyStock.Models;

namespace PolyStock.Services
{
    public class StockEventSQLService : PolyhydraGames.SQLite.SqlRepositoryLite<TransactionEvent, ITransactionEvent>, IStockEventService
    {
        public StockEventSQLService(ISQLiteFactory factory) : base(factory)
        {
        }

        public override string Title => "Stocks";
        public Dictionary<string, List<TransactionEvent>> GetEventLists()
        {
            var symbols = GetSymbols();
            var returnList = new Dictionary<string, List<TransactionEvent>>();
            try
            {
                foreach (var item in symbols)
                {
                    var sublist = Table.Where(x => x.Symbol == item).ToList();
                    returnList.Add(item, sublist);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return returnList;
        }

        public List<TransactionEvent> GetEventLists(string symbol)
        {
            return Table.Where(x => x.Symbol == symbol).ToList();
        }

        public IList<string> GetSymbols()
        {
            var results = base.Table.Select(x => x.Symbol).Distinct();
            return results.ToList();
        }
    }
}