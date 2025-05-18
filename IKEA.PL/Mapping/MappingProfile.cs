using AutoMapper;
using IKEA.BLL.Models.Departments;
using IKEA.BLL.Models.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.PL.Models.Departments;

namespace IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Employee
            CreateMap<Employee, UpdatedEmployeeDto>();

            #endregion
            #region Deparoment
            CreateMap<DepartmentDetailsToReturnDTO, DepartmentEditViewModel>();//.ReverseMap();
            CreateMap<DepartmentEditViewModel,UpdatedDepartmentDTO >();
            #endregion
        }
    }
}
