using IKEA.DAL.Presistance.Repositry.Departments;
using IKEA.DAL.Presistance.Repositry.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.UnitOfWork
{
   public interface IUnitOFWork: IAsyncDisposable
    {
        public IEmployeeRepository  EmployeeRepository { get; }
        public IDepartmentRepository  DepartmentRepository { get;}
       Task< int> CompleteAsync();
    }
}
