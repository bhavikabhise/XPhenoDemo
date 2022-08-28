using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using XphenoApp.Controls;
using XphenoApp.Interface;
using XphenoApp.IService;
using XphenoApp.Locale;
using XphenoApp.Model;
using XphenoApp.Service;
using XphenoApp.SQLRepository.ISQLServices;
using XphenoApp.Views;

namespace XphenoApp.ViewModel
{
    public class EmployeeListViewModel : BaseViewModel
    {
        private ObservableCollection<EmployeeModel> employeeList;
        private readonly ISqlEmployeeService sqlEmployeeService;
        private readonly ISqlService sqlService;
        
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand SelectedEmployeeCommand { get; set; }

        public ObservableCollection<EmployeeModel> EmployeeList
        {
            get => employeeList;
            set => SetProperty(ref employeeList, value);
        }

        public EmployeeListViewModel(ISqlEmployeeService sqlEmployeeService, ISqlService sqlService)
        {
            IsBusy = false;
            this.sqlEmployeeService = sqlEmployeeService;
            this.sqlService = sqlService;
            SelectedEmployeeCommand = new AsyncCommand<EmployeeModel>((employee) => GoToSelectedEmployee(employee), CanExecuteSubmit);
            DeleteEmployeeCommand = new BlockingUIDelegateCommand<EmployeeModel>(async (employee) => await DeleteEmployeeAsync(employee));
        }

        public async void Intitialise()
        {
            var employees = await sqlEmployeeService.GetEmployeesAsync();
            EmployeeList = new ObservableCollection<EmployeeModel>(employees);
        }

        public override async Task OnSaveButtonPressed(object parameter)
        {
            base.OnSaveButtonPressed(parameter);
            try
            {
                IsBusy = true;
                await dialog.ShowDialog(AppResource.Loading);
                await Navigator.GoToPageWithVmAsync<EmployeeDetailView, EmployeeViewModel>(
                   new List<TypedParameter>
                   {
                            new TypedParameter(typeof(EmployeeModel), null)
                   });
            }
            catch(Exception ex)
            {
                exceptionLogService.LogException(ex);
            }
            finally
            {
                IsBusy = false;
                await dialog.HideDialog();
            }
        }

        private async Task DeleteEmployeeAsync(EmployeeModel employee)
        {
            try
            {
                IsBusy = true;
                await dialog.ShowDialog(AppResource.Loading);
                await sqlEmployeeService.DeleteEmployeeAsync(employee);
                Intitialise();
                await dialog.HideDialog();
                await dialog.ShowUnSuccessfullSnackBar(AppResource.DeleteEmployeeSuccessful);
            }
            catch(Exception ex)
            {
                exceptionLogService.LogException(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

        private async Task GoToSelectedEmployee(EmployeeModel employee)
        {
            try
            {
                IsBusy = true;
                await dialog.ShowDialog(AppResource.Loading);
                await Navigator.GoToPageWithVmAsync<EmployeeDetailView, EmployeeViewModel>(
                   new List<TypedParameter>
                   {
                            new TypedParameter(typeof(EmployeeModel), employee)
                   });
            }
            catch (Exception ex)
            {
                exceptionLogService.LogException(ex);
            }
            finally
            {
                IsBusy = false;
                dialog.HideDialog();
            }
        }
    }
}

