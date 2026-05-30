

using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IDepartmentRepository:IGenericRepositoryAsync<Department>
    {
        Task<List<Department>> GetAllAsync();
        
    }
}
