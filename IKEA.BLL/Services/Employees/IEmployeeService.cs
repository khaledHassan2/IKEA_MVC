using IKEA.BLL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Employees
{
   public interface IEmployeeService
    {
     Task<   IEnumerable<EmployeeDto> > GetAllEmployeesAsync(string search);
      Task<  EmployeeDetailsDto?> GetEmployeeByIdAsync(int id);
      Task<  int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto);
       Task< int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto);
       Task< bool> DeleteEmployeeAsync(int id);

    }
}
