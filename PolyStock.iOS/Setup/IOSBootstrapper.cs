

using System;
using System.IO;
using PolyhydraGames.Core.iOS;
using PolyStock.Setup;

namespace PolyStock.iOS.Setup
{ 
    public class IOSBootstrapper : CoreBootstrapper
    {
        public static void Run()
        {
            var item = new IOSBootstrapper();
            item.Setup();
        }

        public IOSBootstrapper()
        {
            Assemblies.Add(typeof(IOSBootstrapper).Assembly);
            Modules.Add(typeof(DeviceModule));
            Modules.Add(typeof(iOSModule));
            Modules.Add(typeof(ServicesModule));
            Modules.Add(typeof(ViewModelModule));
        }

    }
}
