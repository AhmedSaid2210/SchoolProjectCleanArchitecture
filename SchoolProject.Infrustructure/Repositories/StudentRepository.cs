using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;


namespace SchoolProject.Infrustructure.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student> ,IStudentRepository 
    {
        #region Fields
        private readonly DbSet<Student> _student;
        #endregion

        #region Constructor
        public StudentRepository(AppDbContext context):base(context)
        {
            _student = context.Set<Student>();
        }
        #endregion

        #region Handles Functions 
        public async Task<List<Student>> GetAllAsync()
        {
            return await _student.Include(d => d.Department).ToListAsync();
        }
        #endregion
    }
}
