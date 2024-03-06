using System;
using PolyhydraGames.Core.Interfaces;
using PolyStock.Services;
using SQLite;

namespace PolyStock.Models
{
    public class StockBasis : IStockBasis
    {
        public double CostBasis { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public TrackingType Tracking { get; set; } 
 
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
    }

    public interface IStockBasis : IDto
    {

        public double CostBasis { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
    }

    public class TransactionEvent : ITransactionEvent
    {
        public TransactionEvent()
        {

        }
        public double Price { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public ActionType Action { get; set; }
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
    }

    public interface ITransactionEvent : IDto
    {

        public double Price { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public ActionType Action { get; set; }
    }
}