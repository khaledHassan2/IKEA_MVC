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

        public async Task< IEnumerable<T>> GettAllAsync(bool WithAsNoTraking = true)
        {
            if (WithAsNoTraking)
            {
               return await _dbContext.Set<T>().Where(x=>!x.IsDeleted).AsNoTracking().ToListAsync();
            }
            return await _dbContext.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }
        public async  Task< T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
          
        }
        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
           
        }
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);

        }

        public IQueryable<T> GetAllAsQuerable()
        {
            return _dbContext.Set<T>();
        }
    }
}
