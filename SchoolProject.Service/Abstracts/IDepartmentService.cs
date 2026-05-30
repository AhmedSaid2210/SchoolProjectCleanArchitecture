

using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;

namespace SchoolProject.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartments();
        IQueryable<Department> GetDepartmentsQueryable();
        IQueryable<Department> GetDepartmentsByFilterQueryable(DepartmentOrderingEnum orderingEnum, string? search = null);
        Task<Department> GetDepartmentById(int id);
        Task<string> AddAsync(Department department );
        Task<bool> IsNameExsit(string name);
        Task<bool> IsNameExsitExcludeSelf(string name, int id);
        Task<string> UpdateAsync(Department department);
        Task<string> DeleteById(int id);
    }
}
