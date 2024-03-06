using System.Threading.Tasks;

namespace PolyStock.Interfaces
{
    public interface IDataSource
    {
        LoadedState LoadState { get; } 
        Task LoadAsync();
        Task Reload();
    }
}