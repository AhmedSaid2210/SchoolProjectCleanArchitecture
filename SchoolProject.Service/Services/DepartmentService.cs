using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<string> AddAsync(Department department)
        {
            var departmentExist = await _departmentRepository
                .GetTableNoTracking()
                .Where(i => i.IsDeleted.Equals(false))
                .Where(x=>x.DName == department.DName)
                .FirstOrDefaultAsync();

            if (departmentExist != null) return "Exsit";

            await _departmentRepository.AddAsync(department);
           
            return "Success";
        }

        public async Task<string> DeleteById(int id)
        {
            var tran = _departmentRepository.BeginTransaction();
            try
            {
                var departmentExist = await _departmentRepository
                                            .GetTableNoTracking()
                                            .Include(s=>s.Students)
                                            .Include(i=>i.Instructors)
                                            .Include(i => i.InstructorManager)
                                            .Where(x => x.Id == id)
                                            .FirstOrDefaultAsync();

                if (departmentExist == null) return "Department Not Exist";


                if (departmentExist.Students.Any()) return "Error Delete Department Has Students";

                if (departmentExist.Instructors.Any()) return "Error Delete Department Has Instructors";

                if (departmentExist.InstructorManager != null) return "Error Delete Department Has InstructorManager";

                await _departmentRepository.DeleteAsync(departmentExist);

                await tran.CommitAsync();

                return "Delete Success";

            }
            catch (Exception ex)
            {

                tran.Rollback();
                return "Error Delete";
            }

        }

        public async Task<List<Department>> GetAllDepartments()
        {
            var departmentList = await _departmentRepository.GetTableNoTracking()
                                                            .Include(s=>s.Students.Where(d => d.IsDeleted.Equals(false)))
                                                            .Include(i=>i.Instructors.Where(d => d.IsDeleted.Equals(false)))
                                                            .Include(i => i.InstructorManager)
                                                            .Where(d => d.IsDeleted.Equals(false))
                                                            .ToListAsync();

            return departmentList;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var departmentExist = await _departmentRepository
                                            .GetTableNoTracking()
                                            .Include(s => s.Students.Where(d => d.IsDeleted.Equals(false)))
                                            .Include(i => i.Instructors.Where(d => d.IsDeleted.Equals(false)))
                                            .Include(i => i.InstructorManager)
                                            .Include(d => d.DepartmentSubjects)
                                            .ThenInclude(s => s.Subjects)
                                            .Where(x => x.Id == id)
                                            .Where (x => x.IsDeleted.Equals(false))
                                            .FirstOrDefaultAsync();

            

            return departmentExist;
        }

        public IQueryable<Department> GetDepartmentsByFilterQueryable(DepartmentOrderingEnum orderingEnum, string? search = null)
        {
            var departmentExist =  _departmentRepository
                                           .GetTableNoTracking()
                                           .Include(s => s.Students.Where(d => d.IsDeleted.Equals(false)))
                                           .Include(i => i.Instructors.Where(d => d.IsDeleted.Equals(false)))
                                           .Include(i => i.InstructorManager)
                                           .Where(x => x.IsDeleted.Equals(false))
                                           .AsQueryable();

            if (search != null) departmentExist.Where(d => d.DName.Contains(search));

            switch (orderingEnum)
            {
                case DepartmentOrderingEnum.Id:
                    departmentExist = departmentExist.OrderBy(d=>d.Id);
                    break;
                case DepartmentOrderingEnum.Name:
                    departmentExist = departmentExist.OrderBy(d => d.DName);
                    break;
            }

            return departmentExist;

        }

        public IQueryable<Department> GetDepartmentsQueryable()
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

        public async Task<string> UpdateAsync(Department department)
        {
            var departmentExist = await _departmentRepository
                 .GetTableNoTracking()
                 .Where(x => x.Id == department.Id)
                 .FirstOrDefaultAsync();

            if (departmentExist == null) return "Not Exsit";

            var departmentNameExist = await _departmentRepository
              .GetTableNoTracking()
              .Where(x => x.DName == department.DName)
              .FirstOrDefaultAsync();

            if (departmentNameExist != null) return "Name Is Exsit";

            departmentExist.DName = department.DName;
            
            await _departmentRepository.UpdateAsync(departmentExist);
            await _departmentRepository.SaveChangesAsync();

            return "Success";
        }

       
    }
}
