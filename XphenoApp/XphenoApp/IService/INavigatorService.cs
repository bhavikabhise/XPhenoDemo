using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XphenoApp.IService
{
    public interface INavigatorService
    {
        INavigation Navigation { get; }

        Task PopToRootAsync(bool animate = true);

        Task PushPageAsync(Page page, bool animate = true);

        Task PopPageAsync(bool animate = true);

        Task PushModalPageAsync(Page page, bool animate = true);

        Task PopModalPageAsync(bool animate = true);

        void RemovePage(Page page);
    }
}

