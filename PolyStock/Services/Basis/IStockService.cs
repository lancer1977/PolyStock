using System.Collections.Generic;
using PolyhydraGames.Core.Interfaces;
using PolyStock.Models;

namespace PolyStock.Services
{
    public interface IStockEventService : IRepositoryLite<ITransactionEvent>
    {
        Dictionary<string, List<TransactionEvent>> GetEventLists();
        List<TransactionEvent> GetEventLists(string symbol);
    }
}