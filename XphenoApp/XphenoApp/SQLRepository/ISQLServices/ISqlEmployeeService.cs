using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XphenoApp.Model;

namespace XphenoApp.SQLRepository.ISQLServices
{
    public interface ISqlEmployeeService
    {
        Task<int> AddEmployeeAsync(EmployeeModel employee);

        Task<List<EmployeeModel>> GetEmployeesAsync();

        Task<EmployeeModel> GetEmployeeByEmployeeIDAsync(int employeeId);

        Task<int> DeleteEmployeeAsync(EmployeeModel employee);

        Task<int> UpdateEmployeeAsync(EmployeeModel employeeModel);
    }
}

