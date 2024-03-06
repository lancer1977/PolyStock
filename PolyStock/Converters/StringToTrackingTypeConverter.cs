using PolyhydraGames.Extensions;
using PolyStock.Models;

namespace PolyStock.Converters
{
    public class  StringToTrackingTypeConverter : BaseConverter<string, TrackingType> 
    {
        protected override string FromT2(TrackingType t2)
        {
            return t2.ToString();
        }

        protected override TrackingType FromT(string t2)
        {
            return t2.ToEnum<TrackingType>();
        }
    }
}