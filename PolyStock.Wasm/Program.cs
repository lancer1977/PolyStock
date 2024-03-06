using Microsoft.Extensions.Logging;
using PolyStock.Interfaces;
using PolyStock.Setup;
using Uno.UI.Wasm;

namespace PolyStock.Wasm
{
    public class Program
    {
        static int Main(string[] args)
        {
            //ConfigureFilters(Uno.Extensions.LogExtensionPoint.AmbientLoggerFactory);
            HttpService.GetHandler = () => new WasmHttpHandler();

            IOC.Instance.Setup(new[] { typeof(ServicesModule), typeof(ViewModelModule), typeof(InMemoryCovidModule) });
            Windows.UI.Xaml.Application.Start(param => new PolyStock.UWP.App());

            return 0;
        }

    }
}
