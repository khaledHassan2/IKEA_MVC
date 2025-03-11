using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositry._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositry.Departments
{
    public class DepartmentRepository :GenericRepository<Department>,IDepartmentRepository
    {
        
    public DepartmentRepository(ApplicationDbContext dbContext):base(dbContext)
    {
       
    }
}
}
