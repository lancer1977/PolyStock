using System.Collections.Generic;
using System.Threading.Tasks;
using PolyStock.Models;

namespace PolyStock.Interfaces
{
    public interface ILocationSource : IDataSource
    { 
        int GetId(string state, string county); 
        IEnumerable<string> Counties(string state);
        IEnumerable<string> States(); 
        Task LoadAsync();
        int GetFips(string state, string county);
    }
}