namespace PolyStock.Views.Abstract
{
    using System;
    using System.Threading.Tasks;
    using PolyhydraGames.Core.Interfaces;
    using PolyhydraGames.Core.IOC;
    using PolyhydraGames.Core.Services.Interfaces;

    namespace PFAssistant.Core.Views.Abstracts
    {
        public abstract class ModelModalViewModel<T> : ModalViewModelBase, IViewModelModelLoad<T> where T : IUpdate<T>, new()
        {
            protected ModelModalViewModel(INavigator navigator) : base(navigator)
            {
                Model = new T();
            }
            private T _model;
            private Action<T> _action;

            public T Model
            {
                get => _model;
                set => SetValue(ref _model, value);
            }

            public async Task LoadAsync(T value)
            {
                if (!value.Equals(default(T)))
                    Model.Update(value);
                _action = value.Update;
            }

            public async Task LoadAsync(Action<T> value)
            {
                _action = value;
            }


            protected override async Task<bool> OnOK()
            {

                if (_model.Validate() == false)
                {
                    await IOC.Get<IDialogService>().NotificationAsync(Model.InvalidMessage());
                    return false;
                }
                _action.Invoke(Model);
                return true;

            }
            protected override void OnCancel()
            {
                _action?.Invoke(default(T));
            }
            public override bool IsValid()
            {
                return true;
            }
        }
    }
}
