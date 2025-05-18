using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task< IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsAsync();
       Task< DepartmentDetailsToReturnDTO?> GetDepartmentByIDAsync(int id);
       Task< int> CreatDepartmentAsync(CreatedDepartmentDTO departmentDTO);
       Task< int> UpdateDepartmentAsync(UpdatedDepartmentDTO departmentDTO);
       Task< bool> DeleteDepartmentAsync(int id);
    }
}
