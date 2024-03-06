using System;

namespace PolyStock.Views.Stock
{
    public struct UpdateChanges
    {
        public UpdateChanges(int fips, DateTime? dateTime)
        {
            Fips = fips;
            Date = dateTime;
        }
        public int Fips;
        public DateTime? Date;
    }
}