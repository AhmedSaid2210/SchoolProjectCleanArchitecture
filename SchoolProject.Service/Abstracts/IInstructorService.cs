

using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;

namespace SchoolProject.Service.Abstracts
{
    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllInstructors();
        IQueryable<Instructor> GetInstructorsQueryable();
        IQueryable<Instructor> GetInstructorsByFilterQueryable(InstructorOrderingEnum orderingEnum, string? search = null);
        Task<Instructor> GetInstructorById(int id);
        Task<string> AddAsync(Instructor instructor);
        Task<bool> IsNameExsit(string name);
        Task<bool> IsNameExsitExcludeSelf(string name, int id);
        Task<string> UpdateAsync(Instructor instructor);
        Task<string> DeleteById(int id);
    }
}
