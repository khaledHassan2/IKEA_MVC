using IKEA.BLL.Common.Services;
using IKEA.BLL.Models.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositry.Employees;
using IKEA.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOFWork _unitOFWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOFWork unitOFWork,IAttachmentService attachmentService)
        {
            // Ask Clr Creating obj From Class Implementing UnitOfWork
            _unitOFWork = unitOFWork;
            _attachmentService = attachmentService;
        }
        public async Task< int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto)
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
            if (employeeDto.Image is not null)
            {
                employee.Image = _attachmentService.UploadFile(employeeDto.Image, "images");
            }

            _unitOFWork.EmployeeRepository.Add(employee);
            return await _unitOFWork.CompleteAsync();
            
        }
        public async Task< int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto)
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
             _unitOFWork.EmployeeRepository.Update(employee);
            return await _unitOFWork.CompleteAsync();
        }

        public async Task< bool > DeleteEmployeeAsync(int id)
        {
            var employee = await _unitOFWork.EmployeeRepository.GetByIdAsync(id);
            if (employee is { })
            {
               _unitOFWork.EmployeeRepository.Delete(employee);
                return await _unitOFWork.CompleteAsync()>0;
            }
            return false;
        }

        public async Task< IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string search)
        {
            return await _unitOFWork.EmployeeRepository.GetAllAsQuerable().
                Where(E=>!E.IsDeleted&& (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower()))).
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

            }).ToListAsync();
        }

        public async Task< EmployeeDetailsDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOFWork.EmployeeRepository.GetByIdAsync(id);
            if (employee is { })
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
                    //--------------------------
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    //--------------------------
                    Department = employee.Department.Name,
                    Image=employee.Image
                   
                };
            return null;
        }

        }

    }

