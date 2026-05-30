

using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;

namespace SchoolProject.Infrustructure.Repositories
{
    public class SubjectRepository : GenericRepositoryAsync<Subjects>, ISubjectRepository
    {
        private readonly DbSet<Subjects> _subjects;
        public SubjectRepository(AppDbContext dbContext) : base(dbContext)
        {
            _subjects = dbContext.Set<Subjects>();
        }
    }
}
