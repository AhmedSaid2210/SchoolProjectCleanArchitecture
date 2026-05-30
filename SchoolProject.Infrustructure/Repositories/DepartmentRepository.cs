

using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Repositories
{
    public class DepartmentRepository:GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _departmentSet;
        public DepartmentRepository(AppDbContext appDbContext):base(appDbContext)
        {
            _departmentSet = appDbContext.Set<Department>();
        }
        public  async Task<List<Department>> GetAllAsync()
        {
            return await _departmentSet.Include(d => d.Students).ToListAsync();
        }

      
    }
}
