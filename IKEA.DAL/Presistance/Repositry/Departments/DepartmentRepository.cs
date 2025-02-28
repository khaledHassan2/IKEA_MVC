using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositry.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Department> GettAll(bool WithAsNoTraking = true)
        {
            if (WithAsNoTraking)
            {
               _dbContext.Departments.AsNoTracking().ToList();
            }
            return _dbContext.Departments.ToList();
        }
        public Department? GetById(int id)
        {
            var department = _dbContext.Departments.Find(id);
            return department;
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();

        }

        public IQueryable<Department> GetAllAsQuerable()
        {
            return _dbContext.Departments;
        }
    }
}
