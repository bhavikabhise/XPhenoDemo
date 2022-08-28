using System;
using Autofac;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XphenoApp.Controls;
using XphenoApp.Service;
using XphenoApp.IService;

namespace XphenoApp.ViewModel
{
    public class BaseViewModel : BaseNotifyPropertyChanged
    {
        public IShowDialogs dialog = App.DiContainer.Resolve<IShowDialogs>();
        public IExceptionLogService exceptionLogService = App.DiContainer.Resolve<IExceptionLogService>();

        public ICommand OnPageAppearingCommand { get; set; }
        public ICommand OnPageDisappearingCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }
        public ICommand SaveButtonCommand { get; set; }

        public BaseViewModel()
        {
            OnPageAppearingCommand = new Command(() => ExecuteOnAppearing());
            OnPageDisappearingCommand = new Command(() => ExecuteOnDisappearing());
            CancelButtonCommand = new BlockingUIDelegateCommand(async () => OnBackButtonPressed());
            SaveButtonCommand = new BlockingUIDelegateCommand<object>(async (obj) => await OnSaveButtonPressed(obj));
        }

        #region Private Properties

        private bool isBusy;
        private string title = string.Empty;

        #endregion Private Properties

        #region Public Properties

        public bool IsBusy

        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        #endregion Public Properties

        public bool CanExecuteSubmit(object p = null)
        {
            return !IsBusy;
        }

        public bool CanExecuteSubmit()
        {
            return !IsBusy;
        }

        public virtual async void OnBackButtonPressed() => await Navigator.GoBackAsync();

        public virtual async Task OnSaveButtonPressed(object parameter)
        {
        }

        public virtual void ExecuteOnDisappearing()
        {
        }

        public virtual void ExecuteOnAppearing()
        {
        }
    }
}

