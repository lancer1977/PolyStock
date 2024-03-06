using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using PolyStock.Services;
using PolyhydraGames.Extensions;
using PolyhydraGames.Core.Forms.Setup;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.Forms.Interfaces;
using PolyStock.Views.Loading;
using System.Reflection;
using System.Diagnostics;

namespace PolyStock.Setup
{
    public class ServicesModule : Autofac.Module
    {
        private IEnumerable<Type> _except;

        public   ServicesModule()
        { 
        }
        protected override void Load(ContainerBuilder builder)
        { 
            var assembly = typeof(ServicesModule).Assembly;
            var services = assembly.CreatableTypes().EndingWith("Service").ToArray();
            builder.RegisterTypes(services).AsImplementedInterfaces().AsSelf().SingleInstance();

     //       var sources = assembly.CreatableTypes().EndingWith("Source").Except(_except).ToArray();
      //      builder.RegisterTypes(sources).AsImplementedInterfaces().AsSelf().SingleInstance();
        }
    }

    public abstract class CoreBootstrapper : FormsIOCBootstrapper
    {
        private IList<Type> ExcludedPages() => new List<Type> { };// typeof(VerbosePickerPage) };//, typeof(FilteredVerbosePickerPage) };
        protected override void FollowupAction() { }
        protected override void ViewFactoryRegistration()
        {
            var pages = Pages(Assemblies.ToArray()).Except(ExcludedPages()).ToArray();
            var viewModels = ViewModels(Assemblies.ToArray());
            PairMVVM(pages, viewModels.ToArray());
            PairMVVMInterfaces(pages, viewModels.ToArray());
            var vf = IOC.Get<IViewFactory>();
            vf.Register<IMainMenu, Views.MenuPage>();
            vf.Register<IFirstPageViewModel, LoadingPage>();
            if (!vf.IsRegistered<IMainMenu>())
            {
                throw new Exception("IMainMenu is not registered!");
            };
            if (!vf.IsRegistered<IFirstPageViewModel>())
            {
                throw new Exception("IFirstPageViewModel is not registered!");
            };
        }

        protected void PairMVVMInterfaces(IEnumerable<Type> pages, Type[] viewModels)
        {
            var factory = IOC.Get<IViewFactory>();
            foreach (var page in pages)
            {
                var prefix = page.GetTypeInfo().Name.Replace("Page", "");
                var viewModel =
                    viewModels.FirstOrDefault(i => i.GetTypeInfo().Name.Replace("ViewModel", "") == prefix);
                if (viewModel == null) throw new Exception($"No viewmodel found for type of {page.Name}");
                var iviewmodel = typeof(IViewModel);
                var implementedInterfaces = viewModel.GetInterfaces().Where(p => iviewmodel.IsAssignableFrom(p)).Except(typeof(IViewModel));
                foreach (var t in implementedInterfaces)
                {
                    Debug.WriteLine($"Registring {t.Name} to {page.Name}");
                    factory.Register(t, page);
                }
            }
        }

        protected CoreBootstrapper()
        {
            Assemblies.Add(typeof(CoreBootstrapper).GetAssembly()); 
            Modules.Add(typeof(ServicesModule));

        }

    }

}

