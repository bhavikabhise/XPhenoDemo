using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XphenoApp.IService;

namespace XphenoApp.Service
{
    public class NavigatorService : INavigatorService
    {
        private readonly INavigation navigation;
        private readonly IExceptionLogService exceptionLogService;

        public NavigatorService(INavigation navigation, IExceptionLogService exceptionLogService)
        {
            this.navigation = navigation;
            this.exceptionLogService = exceptionLogService;
        }

        public INavigation Navigation => navigation;

        public async Task PopModalPageAsync(bool animate = true)
        {
            await MainThread.InvokeOnMainThreadAsync(async () => await navigation.PopModalAsync(animate)).ConfigureAwait(false);
        }

        public async Task PopPageAsync(bool animate = true)
        {
            await MainThread.InvokeOnMainThreadAsync(async () => await navigation.PopAsync(animate)).ConfigureAwait(false);
        }

        public async Task PopToRootAsync(bool animate = true)
        {
            await MainThread.InvokeOnMainThreadAsync(async () => await navigation.PopToRootAsync()).ConfigureAwait(false);
        }

        public async Task PushModalPageAsync(Page page, bool animate = true)
        {
            await MainThread.InvokeOnMainThreadAsync(async () => await navigation.PushModalAsync(page, animate)).ConfigureAwait(false);
        }

        public async Task PushPageAsync(Page page, bool animate = true)
        {
            try
            {
                await MainThread.InvokeOnMainThreadAsync(async () => await navigation.PushAsync(page, animate)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemovePage(Page page)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    navigation.RemovePage(page);
                }
                catch (Exception exception)
                {
                    exceptionLogService.LogException(exception);
                }
            });
        }
    }
}

