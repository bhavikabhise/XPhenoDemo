using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XphenoApp.DataStore;
using XphenoApp.Model;
using XphenoApp.SQLRepository.ISQLServices;

namespace XphenoApp.SQLRepository.SQLServices
{
    public class SqlEmployeeService : ISqlEmployeeService
    {
        public Task<int> AddEmployeeAsync(EmployeeModel employee)
        {
            return SqlDataStore.SharedInstance.Database.InsertAsync(employee);
        }

        public Task<int> DeleteEmployeeAsync(EmployeeModel employee)
        {
            return SqlDataStore.SharedInstance.Database.DeleteAsync<EmployeeModel>(employee.EmployeeId);
        }

        public Task<EmployeeModel> GetEmployeeByEmployeeIDAsync(int employeeId)
        {
            return SqlDataStore.SharedInstance.Database.FindAsync<EmployeeModel>(employeeId);
        }

        public Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            return SqlDataStore.SharedInstance.Database.Table<EmployeeModel>().ToListAsync();
        }

        public Task<int> UpdateEmployeeAsync(EmployeeModel employeeModel)
        {
            return SqlDataStore.SharedInstance.Database.UpdateAsync(employeeModel, typeof(EmployeeModel));
        }
    }
}

