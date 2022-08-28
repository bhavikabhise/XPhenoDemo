using System;
using Autofac;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using XphenoApp.IService;

namespace XphenoApp.Controls
{
    public class BlockingUIDelegateCommand : ICommand
    {
        private static SemaphoreSlim BlockUiSemaphoreSlim { get; }
        private bool isExecuting;
        private Func<Task> executeMethod;
        private Func<bool> canExecute;

        internal static readonly int UnblockDelayInMillis = 500;
        internal static int UiBlockingActiveCommandsCount { get; private set; }

        public event EventHandler CanExecuteChanged;

        static BlockingUIDelegateCommand()
        {
            BlockUiSemaphoreSlim = new SemaphoreSlim(1, 1);
        }

        public BlockingUIDelegateCommand(Func<Task> executeMethod)
        {
            this.executeMethod = executeMethod;
        }

        public BlockingUIDelegateCommand(Func<Task> executeMethod, Func<bool> canExecute)
        {
            this.executeMethod = executeMethod;
            this.canExecute = canExecute;
        }

        internal static async Task BlockUi()
        {
            try
            {
                await BlockUiSemaphoreSlim.WaitAsync();

                UiBlockingActiveCommandsCount++;

                if (UiBlockingActiveCommandsCount > 1)
                {
                    return;
                }

                App.Instance.ShouldBlockUI = true;
            }
            finally
            {
                BlockUiSemaphoreSlim.Release();
            }
        }

        internal static async Task UnblockUi()
        {
            try
            {
                await BlockUiSemaphoreSlim.WaitAsync();

                UiBlockingActiveCommandsCount--;

                if (UiBlockingActiveCommandsCount > 0)
                {
                    return;
                }

                App.Instance.ShouldBlockUI = false;
            }
            finally
            {
                BlockUiSemaphoreSlim.Release();
            }
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute != null) return this.canExecute();
            return true;
        }

        public void Execute(object parameter)
        {
            if (!isExecuting)
            {
                isExecuting = true;

                Task.Run(async () =>
                {
                    await CommitExecuteCommand(this.executeMethod);
                    isExecuting = false;
                });
            }
        }

        private static async Task CommitExecuteCommand(Func<Task> executeAction)
        {
            try
            {
                await BlockUi();
                await executeAction();
                await Task.Delay(UnblockDelayInMillis);
            }
            catch (Exception ex)
            {
                var logService = App.DiContainer.Resolve<IExceptionLogService>();
                logService.LogException(ex);
            }
            finally
            {
                await UnblockUi();
            }
        }
    }

    public class BlockingUIDelegateCommand<T> : ICommand
    {
        private Func<T, Task> executeMethod;
        private Func<T, bool> canExecute;
        private bool isExecuting;

        public event EventHandler CanExecuteChanged;

        public BlockingUIDelegateCommand(Func<T, Task> executeMethod)
        {
            this.executeMethod = executeMethod;
        }

        public BlockingUIDelegateCommand(Func<T, Task> executeMethod,
            Func<T, bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecute = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute != null) return this.canExecute((T)parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            if (!isExecuting)
            {
                isExecuting = true;

                Task.Run(async () =>
                {
                    await CommitExecuteCommand(this.executeMethod, (T)(parameter));
                    isExecuting = false;
                });
            }
        }

        private static async Task CommitExecuteCommand(Func<T, Task> executeAction, T commandParameter)
        {
            try
            {
                await BlockingUIDelegateCommand.BlockUi();
                await executeAction(commandParameter);
                await Task.Delay(BlockingUIDelegateCommand.UnblockDelayInMillis);
            }
            catch (Exception ex)
            {
                var logService = App.DiContainer.Resolve<IExceptionLogService>();
                logService.LogException(ex);
            }
            finally
            {
                await BlockingUIDelegateCommand.UnblockUi();
            }
        }
    }
}

