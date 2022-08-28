using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Xamarin.Forms;
using XphenoApp.IService;
using XphenoApp.Locale;
using XphenoApp.Model;
using XphenoApp.Service;
using XphenoApp.SQLRepository.ISQLServices;

namespace XphenoApp.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        private EmployeeModel employee;
        private string firstName;
        private string lastName;
        private bool isCreate;
        private string buttonText;
        private string errorText;
        private bool isError;
        private readonly ISqlEmployeeService sqlEmployeeService;

        public EmployeeModel Employee
        {
            get => employee;
            set => SetProperty(ref employee, value);
        }

        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        public string ErrorText
        {
            get => errorText;
            set => SetProperty(ref errorText, value);
        }

        public bool IsCreate
        {
            get => isCreate;
            set => SetProperty(ref isCreate, value);
        }

        public bool IsError
        {
            get => isError;
            set => SetProperty(ref isError, value);
        }

        public string ButtonText
        {
            get => buttonText;
            set => SetProperty(ref buttonText, value);
        }

        public EmployeeViewModel(EmployeeModel employeeModel, ISqlEmployeeService sqlEmployeeService)
        {
            this.Employee = employeeModel;
            this.sqlEmployeeService = sqlEmployeeService;
            Initialise();
        }

        private void Initialise()
        {
            if (Employee != null)
            {
                Title = AppResource.UpdateEmployee;
                FirstName = Employee.FirstName;
                LastName = Employee.LastName;
                ButtonText = AppResource.Update;
                IsCreate = false;
            }
            else
            {
                Title = AppResource.CreateEmployee;
                ButtonText = AppResource.Submit;
                IsCreate = true;
            }
        }

        public override async Task OnSaveButtonPressed(object parameter)
        {
            base.OnSaveButtonPressed(parameter);
            try
            {
                IsBusy = true;
                if(Validate())
                {
                    await dialog.ShowDialog(AppResource.Loading);
                    if (IsCreate)
                    {
                        await AddEmployee();
                        await dialog.HideDialog();
                        await dialog.ShowSuccessSnackBar(AppResource.AddSuccessful);
                    }
                    else
                    {

                        await UpdateEmployee();
                        await dialog.ShowSuccessSnackBar(AppResource.UpdateEmployeeSuccessful);
                    }
                    await Navigator.GoBackAsync();
                }
            }
            catch(Exception ex)
            {
                exceptionLogService.LogException(ex);
            }
            if(IsCreate)
            {
                IsBusy = false;
            }
        }

        private async Task UpdateEmployee()
        {
            Employee.FirstName = FirstName;
            Employee.LastName = LastName;
            await sqlEmployeeService.UpdateEmployeeAsync(Employee);
            await dialog.HideDialog();
        }

        private async Task AddEmployee()
        {
            EmployeeModel employeeModel = new EmployeeModel
            {
                EmployeeId = Guid.NewGuid().ToString(),
                FirstName = FirstName,
                LastName = LastName
            };
            await sqlEmployeeService.AddEmployeeAsync(employeeModel);
        }

        private bool Validate()
        {
            IsError = false;
            if(string.IsNullOrWhiteSpace(FirstName))
            {
                ErrorText = AppResource.FirstNameError;
            }
            else if(string.IsNullOrWhiteSpace(LastName))
            {
                ErrorText = AppResource.LastNameError;
            }
            else
            {
                IsError = true;
            }
            return IsError;
        }
    }
}

