namespace PolyStock.Interfaces
{
    public interface IPopulationSource : IDataSource
    {
        int Population(string state, string county);
       
    }
}