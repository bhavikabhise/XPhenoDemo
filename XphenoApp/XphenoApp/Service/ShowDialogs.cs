using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using XphenoApp.IService;

namespace XphenoApp.Service
{
    public class ShowDialogs : IShowDialogs
    {
        public async Task HideDialog()
        {
            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
            {
                UserDialogs.Instance.HideLoading();
            });
        }

        public async Task ShowDialog(string Title)
        {
            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
            {
                UserDialogs.Instance.ShowLoading(Title);
            });
        }

        public async Task ShowInfoSnackBar(string Message)
        {
            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
            {
                ToastConfig config = new ToastConfig(Message);
                config.MessageTextColor = Color.White;
                config.BackgroundColor = Color.BlueViolet;
                config.Duration = TimeSpan.FromSeconds(1);
                UserDialogs.Instance.Toast(config);
            });
        }

        public async Task ShowSuccessSnackBar(string Message)
        {
            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
            {
                ToastConfig config = new ToastConfig(Message);
                config.MessageTextColor = Color.White;
                config.BackgroundColor = Color.Green;
                config.Duration = TimeSpan.FromSeconds(1);
                UserDialogs.Instance.Toast(config);
            });
        }

        public async Task ShowUnSuccessfullSnackBar(string Message)
        {
            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(() =>
            {
                ToastConfig config = new ToastConfig(Message);
                config.MessageTextColor = Color.White;
                config.BackgroundColor = Color.Red;
                config.Duration = TimeSpan.FromSeconds(1);
                UserDialogs.Instance.Toast(config);
            });
        }
    }
}

