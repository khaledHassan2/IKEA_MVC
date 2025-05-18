using IKEA.DAL.Presistance.Data;
using IKEA.DAL.Presistance.Repositry.Departments;
using IKEA.DAL.Presistance.Repositry.Employees;

namespace IKEA.DAL.Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOFWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return new EmployeeRepository(_dbContext);
            }
        }

        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                return new DepartmentRepository(_dbContext);
            }
        }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            // Ask Clr Create Obj From ApplicationDbContext Implicitly
            _dbContext = dbContext;
        }

        public async Task< int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
           await _dbContext.DisposeAsync();
        }
    }
}
