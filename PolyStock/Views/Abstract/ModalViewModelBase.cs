namespace PolyStock.Views.Abstract
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using PolyhydraGames.Core.Interfaces;
    using PolyhydraGames.Core.ViewModel;
    using ReactiveUI;

    namespace PFAssistant.Core.Views.Abstracts
    {
        public abstract class ModalViewModelBase<T> : ModalViewModelBase, ILoad<Action<T>>
        {
            public ModalViewModelBase(INavigator navigator) : base(navigator)
            {
            }
            protected Action<T> OkAction;

            public virtual void Load(Action<T> value)
            {
                OkAction = value;
            }

            protected override void OnCancel()
            {
            }

        }

        public abstract class ModalViewModelBase : ViewModelBase
        {
            protected bool Verify(string value)
            {
                return string.IsNullOrEmpty(value) == false;
            }
            readonly INavigator _navigator;
            public ModalViewModelBase(INavigator navigator)
            {
                _navigator = navigator;
                CancelCommand = ReactiveCommand.Create(async () =>
                {

                    OnCancel();
                    await PopAsync();
                });

                OKCommand = ReactiveCommand.Create(async () =>
                {
                    if (IsValid() == false || await OnOK() == false) return;
                    await PopAsync();
                });

            }

            protected async Task PopAsync()
            {
                if (IsPopup)
                {
                    await _navigator.PopPopupAsync();
                }
                else
                {
                    await _navigator.PopModalAsync();
                }
            }
            public abstract bool IsValid();
            public ICommand OKCommand { get; protected set; }
            public ICommand CancelCommand { get; protected set; }
            protected bool IsPopup { get; set; } = true;
            protected abstract void OnCancel();
            protected abstract Task<bool> OnOK();


        }
    }
}
