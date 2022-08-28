using System;
using System.Threading.Tasks;

namespace XphenoApp.IService
{
    public interface IShowDialogs
    {
        Task ShowDialog(string Title);

        Task HideDialog();

        Task ShowSuccessSnackBar(string Message);

        Task ShowUnSuccessfullSnackBar(string Message);

        Task ShowInfoSnackBar(string Message);
    }
}

