using System;
using Autofac;
using PolyhydraGames.SQLite;
using PolyhydraGames.SQLite.Interfaces;
using PolyhydraGames.Core.Forms.Interfaces;
using System.IO;

namespace PolyStock.iOS.Setup
{
    public class iOSModule : Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => (App)Xamarin.Forms.Application.Current).As<IApp>().SingleInstance(); 

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder

            builder.Register<ISQLiteFactory>((ctx) => new SqlLiteBridge(libraryPath))
                .SingleInstance();
        }
    }
}
