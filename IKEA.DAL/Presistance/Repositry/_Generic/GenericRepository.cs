using IKEA.DAL.Models;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositry._Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T:ModelBase
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GettAll(bool WithAsNoTraking = true)
        {
            if (WithAsNoTraking)
            {
                _dbContext.Set<T>().Where(x=>!x.IsDeleted).AsNoTracking().ToList();
            }
            return _dbContext.Set<T>().Where(x => !x.IsDeleted).ToList();
        }
        public T? GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();

        }

        public IQueryable<T> GetAllAsQuerable()
        {
            return _dbContext.Set<T>();
        }
    }
}
