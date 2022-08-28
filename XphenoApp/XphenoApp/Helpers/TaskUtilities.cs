using System;
using Autofac;
using System.Threading.Tasks;
using XphenoApp.IService;

namespace XphenoApp.Helpers
{
    public static class TaskUtilities
    {
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void

        public static async void FireAndForgetSafeAsync(this Task task)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                App.DiContainer.Resolve<IExceptionLogService>().LogException(ex);
            }
        }
    }
}

