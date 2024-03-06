using PolyStock.Setup;
using PolyhydraGames.Core.Droid;

namespace PolyStock.Droid
{
    public class DroidBootstrapper : CoreBootstrapper
    {
        public static void Run()
        {
            var item = new DroidBootstrapper();
            item.Setup();
        }

        public DroidBootstrapper()
        {
            Assemblies.Add(typeof(DroidBootstrapper).Assembly);
            Modules.Add(typeof(DeviceModule));
            Modules.Add(typeof(DroidModule));
            Modules.Add(typeof(ServicesModule));
            Modules.Add(typeof(ViewModelModule)); 
        }

    }
}