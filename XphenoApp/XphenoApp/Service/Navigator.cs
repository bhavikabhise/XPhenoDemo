using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XphenoApp.IService;
using XphenoApp.ViewModel;

namespace XphenoApp.Service
{
    public static class Navigator
    {
        public static INavigatorService NavigatorService => App.DiContainer.Resolve<INavigatorService>();

        public static async Task GoBackAsync(bool animate = true)
        {
            await NavigatorService.PopPageAsync(animate);
        }

        public static async Task GoToPageAsync<T>(bool isModal = false) where T : Page
        {
            var page = App.DiContainer.Resolve<T>();
            if (isModal)
            {
                await NavigatorService.PushModalPageAsync(page);
            }
            else
            {
                await NavigatorService.PushPageAsync(page);
            }
        }

        public static async Task GoToPageWithParametersAsync<T>(IEnumerable<Autofac.Core.Parameter> parameters) where T : ContentPage
        {
            using (var lifetime = App.DiContainer.BeginLifetimeScope())
            {
                var page = lifetime.Resolve<T>(parameters);
                await NavigatorService.PushPageAsync(page);
            }
        }

        public static async Task GoToPageWithVmAsync<T, V>(
            IEnumerable<Autofac.Core.Parameter> parameters,
            bool isModal = false,
            bool animation = true, bool shouldBindVM = false
            ) where T : ContentPage where V : BaseViewModel
        {
            using (var lifetime = App.DiContainer.BeginLifetimeScope())
            {
                var vm = lifetime.Resolve<V>(parameters);
                var page = lifetime.Resolve<T>();
                if (shouldBindVM)
                {
                    page.BindingContext = vm;
                }
                if (isModal)
                {
                    await NavigatorService.PushModalPageAsync(page, animation);
                }
                else
                {
                    await NavigatorService.PushPageAsync(page, animation);
                }
            }
        }

        public static async Task InitializeMasterDetail()
        {
            try
            {
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    var navigationPage = App.DiContainer.Resolve<NavigationPage>();
                    Application.Current.MainPage = navigationPage;
                    navigationPage.BarBackgroundColor = Color.SkyBlue;
                });
            }
            catch (Exception ex)
            {
                var exceptionLogService = App.DiContainer.Resolve<IExceptionLogService>();
                exceptionLogService.LogException(ex);
            }
        }

        public static async Task PopPopupAsync()
        {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Any())
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }
        }
    }
}

