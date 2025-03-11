using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositry._Generic
{
   public interface IGenericRepository<T> where T:ModelBase
    {
        IEnumerable<T> GettAll(bool WithAsNoTraking = true);
        IQueryable<T> GetAllAsQuerable();
        T? GetById(int id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
