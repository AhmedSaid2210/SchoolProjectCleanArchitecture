

using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Repositories
{
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
    {
        private readonly DbSet<Instructor> _instructors;
        public InstructorRepository(AppDbContext dbContext) : base(dbContext)
        {
            _instructors = dbContext.Set<Instructor>();
        }
    }
}
