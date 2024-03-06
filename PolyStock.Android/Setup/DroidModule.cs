using System;
using Autofac;
using PolyStock.Setup;
using PolyhydraGames.SQLite;
using PolyhydraGames.SQLite.Interfaces;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.Core.Forms.Interfaces;

namespace PolyStock.Droid
{
  

    public class DroidModule : Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => (App)Xamarin.Forms.Application.Current).As<IApp>().SingleInstance();
            builder.Register((ctx) => new SqlLiteBridge(Environment.GetFolderPath(Environment.SpecialFolder.Personal))).As<ISQLiteFactory>().SingleInstance();

           // builder.RegisterType<PopupDialogService>().AsImplementedInterfaces().SingleInstance();
        }
    }
}