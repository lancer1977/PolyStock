using System;
using System.IO;
using Autofac;
using Autofac.Builder;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using PolyhydraGames.Core.Interfaces;
using PolyStock.Setup;
using PolyhydraGames.SQLite;
using PolyhydraGames.SQLite.Interfaces;

namespace PolyStock.UWP
{

    public class StatusBarService : IStatusBarManager
    {
        public void Show(bool showBar)
        { 
        }
    }
    public class UWPModule : Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            builder.Register<ISQLiteFactory>((ctx) => new SqlLiteBridge(path))
                .SingleInstance();
            builder.Register<ISettings>(ctx => CrossSettings.Current).SingleInstance();

            // ((IRegistrationBuilder<ISettings, SimpleActivatorData, SingleRegistrationStyle>)Autofac.RegistrationExtensions.Register<ISettings>(builder, ctx => CrossSettings.Current)).As<ISettings>();

        }
    }
    public class WPFBootstrapper : CoreBootstrapper
    {
        public static void Run()
        {
            var item = new WPFBootstrapper();
            item.Setup();
        }

        public WPFBootstrapper()
        {
            Assemblies.Add(typeof(WPFBootstrapper).Assembly);
            //Modules.Add(typeof(DeviceModule));
            Modules.Add(typeof(UWPModule));
            Modules.Add(typeof(ServicesModule));
            Modules.Add(typeof(ViewModelModule));
        }

    }
}

