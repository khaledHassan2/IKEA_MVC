using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositry.Departments
{
    interface IDepartmentRepository
    {
        IEnumerable<Department> GettAll(bool WithAsNoTraking = true);
        Department? GetById(int id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);
    }
}
