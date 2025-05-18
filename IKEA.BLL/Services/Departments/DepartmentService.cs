using IKEA.BLL.Models.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositry.Departments;
using IKEA.DAL.Presistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
       
        private readonly IUnitOFWork _unitOFWork;

        public DepartmentService(IUnitOFWork unitOFWork)
        {
           
            _unitOFWork = unitOFWork;
        }
        
        public async Task< IEnumerable<DepartmentToReturnDTO>> GetAllDepartmentsAsync()
        {
            var departments = _unitOFWork.DepartmentRepository.GetAllAsQuerable().Where(D=>!D.IsDeleted).Select(department => new DepartmentToReturnDTO
            {
                // Manull Maping (Auto Mapper)
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate
            }).AsNoTracking().ToList();
            return departments;
            
            
        }


        public async Task< DepartmentDetailsToReturnDTO? > GetDepartmentByIDAsync(int id)
        {
            var department = await _unitOFWork.DepartmentRepository.GetByIdAsync(id);
            if(department is{ } /* is not null */ )
            {

            return new DepartmentDetailsToReturnDTO
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedBy=department.CreatedBy,
                CreatedOn=department.CreatedOn,
                LastModeficationBy=department.LastModeficationBy,
                LastModeficationOn=department.LastModeficationOn
            };
            }
            return null;
        }

        public async Task< int> CreatDepartmentAsync(CreatedDepartmentDTO departmentDTO)
        {
            var Creatteddepartment = new Department()
            {
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                CreationDate = departmentDTO.CreationDate,
                CreatedBy = 1,
                LastModeficationBy = 1,
                LastModeficationOn = DateTime.UtcNow,
                //CreatedOn=DateTime.UtcNow,
            };
            _unitOFWork.DepartmentRepository.Add(Creatteddepartment);
            return await _unitOFWork.CompleteAsync();
        }


        public async Task< int> UpdateDepartmentAsync(UpdatedDepartmentDTO departmentDTO)
        {
            var updateddepartment = new Department()
            {
                Id=departmentDTO.Id,
                Code = departmentDTO.Code,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                CreationDate = departmentDTO.CreationDate,
                LastModeficationBy = 1,
                LastModeficationOn = DateTime.UtcNow,
                
            };
            _unitOFWork.DepartmentRepository.Update(updateddepartment);
            return await _unitOFWork.CompleteAsync();
        }
        public async Task< bool> DeleteDepartmentAsync(int id)
        {
            var department = await _unitOFWork.DepartmentRepository.GetByIdAsync(id);
            if(department is not null)
            {
                _unitOFWork.DepartmentRepository.Delete(department);
                return await _unitOFWork.CompleteAsync() > 0;
            }
            return false;
        }

    }
}
