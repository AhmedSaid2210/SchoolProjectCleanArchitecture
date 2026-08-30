

using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<string> AddAsync(Subjects Subjects)
        {
            var subjectExist = await _subjectRepository.GetTableNoTracking()
                                                       .Where(s=>s.IsDeleted.Equals(false))
                                                       .Where(s=>s.SubjectName.Equals(Subjects.SubjectName))
                                                       .FirstOrDefaultAsync();
            if (subjectExist != null) return "Exist";

            await _subjectRepository.AddAsync(Subjects);

            return "Success";
        }

        public Task<string> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Subjects>> GetAllSubjects()
        {
            throw new NotImplementedException();
        }

        public Task<Subjects> GetSubjectById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Subjects> GetSubjectsByFilterQueryable(SubjectsOrderingEnum orderingEnum, string? search = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Subjects> GetSubjectsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNameExsit(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNameExsitExcludeSelf(string name, int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(Subjects Subjects)
        {
            throw new NotImplementedException();
        }
    }
}
