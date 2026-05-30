
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;

namespace SchoolProject.Service.Abstracts
{
    public interface ISubjectService
    {
        Task<List<Subjects>> GetAllSubjects();
        IQueryable<Subjects> GetSubjectsQueryable();
        IQueryable<Subjects> GetSubjectsByFilterQueryable(SubjectsOrderingEnum orderingEnum, string? search = null);
        Task<Subjects> GetSubjectById(int id);
        Task<string> AddAsync(Subjects Subjects);
        Task<bool> IsNameExsit(string name);
        Task<bool> IsNameExsitExcludeSelf(string name, int id);
        Task<string> UpdateAsync(Subjects Subjects);
        Task<string> DeleteById(int id);
    }
}
