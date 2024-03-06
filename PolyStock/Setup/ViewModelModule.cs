using System.Linq;
using Autofac;
using PolyhydraGames.Extensions;
using PolyStock.Views.Stock.Add;
using PolyStock.Views.Stock.AddBasis;

namespace PolyStock.Setup
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var exceptionList = new[] {typeof(AddBasisViewModel)};
            //builder.RegisterType<DataLoader>().AsImplementedInterfaces().SingleInstance();
            //builder.RegisterType<Constants>().AsImplementedInterfaces().SingleInstance();
            //builder.RegisterType<ResourceDB>().AsSelf().SingleInstance();
            //builder.Register((x) => (App)Application.Current).As<IApp>().SingleInstance();
            var assembly = typeof(ServicesModule).Assembly;
            var types = assembly.CreatableTypes().EndingWith("ViewModel").Except(exceptionList).ToArray();
            builder.RegisterTypes(types).AsImplementedInterfaces().AsSelf().SingleInstance();
            builder.RegisterTypes(exceptionList).AsImplementedInterfaces().AsSelf();

        }
    }
}

