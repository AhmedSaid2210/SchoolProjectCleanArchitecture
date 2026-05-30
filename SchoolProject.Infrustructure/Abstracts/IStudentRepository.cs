using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Abstracts
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        Task<List<Student>> GetAllAsync();
    }
}
