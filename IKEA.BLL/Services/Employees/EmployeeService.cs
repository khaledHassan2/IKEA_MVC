using IKEA.BLL.Models.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositry.Employees;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new IKEA.DAL.Models.Employees.Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId=employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModeficationBy = 1,
                LastModeficationOn = DateTime.UtcNow
            };
          
           return _employeeRepository.Add(employee);
            
        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId=employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModeficationBy = 1,
                LastModeficationOn = DateTime.UtcNow

            };
            return _employeeRepository.Update(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is { })
            {
                return _employeeRepository.Delete(employee) > 0;
            }
            return false;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return _employeeRepository.GetAllAsQuerable().
                Where(E=>!E.IsDeleted).
                Include(e=>e.Department).
                Select(employee => new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,

                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,


                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                Department=employee.Department.Name

            });
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is { })
            {
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    Department=employee.Department.Name
                };
            }

                return null;
        }

    }
}
