using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;
using SchoolProject.Infrustructure.Abstracts;

using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Services
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Constructor
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        #endregion

        #region Handles Functions 
        public async Task<string> AddAsync(Student student)
        {
            var studentexist = await _studentRepository.GetTableNoTracking()
                .Where(s => s.Name.Equals(student.Name))
                .Where(i => i.IsDeleted.Equals(false))
                .FirstOrDefaultAsync();

            if (studentexist != null)
            {
                return "Exsit";
            }
            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> DeleteById(int id)
        {
            var tran = _studentRepository.BeginTransaction();

            try
            {
                var student = await _studentRepository.GetTableAsTracking().Where(s => s.Id == id).FirstOrDefaultAsync();

                if (student == null) return false;

                await _studentRepository.DeleteAsync(student);

                await tran.CommitAsync();

                return true;
            }
            catch
            {
                await tran.RollbackAsync();
                return false;
            }

            
        }
        public async Task<List<Student>> GetAllStudents()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = _studentRepository
                            .GetTableNoTracking()
                            .Include(d => d.Department)
                            .Include(s => s.StudentsSubjects)
                            .ThenInclude(s=>s.Subject)
                            .Where(s => s.Id.Equals(id))
                            .Where(s=>s.IsDeleted.Equals(false))
                            .FirstOrDefault();
            return student ;
        }

        public IQueryable<Student> GetStudentsByFilterQueryable(StudentOrderingEnum orderingEnum,string? search=null)
        {
            var query =  _studentRepository.GetTableNoTracking().Include(d => d.Department).AsQueryable();

            if(search != null) query = query.Where(s => s.Name.Contains(search) || s.Address.Contains(search));

            switch (orderingEnum)
            {
                case StudentOrderingEnum.StudID:
                    query = query.OrderBy(s => s.Id)  ; 
                    break;
                case StudentOrderingEnum.Name:
                    query = query.OrderBy(s => s.Name);
                    break;
                case StudentOrderingEnum.Address:
                    query = query.OrderBy(s => s.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    query = query.OrderBy(s => s.Department.DName);
                    break;
            }
            return query;
        }

        public IQueryable<Student> GetStudentsQueryable()
        {
            return _studentRepository.GetTableNoTracking().Include(d=>d.Department).AsQueryable();
        }

        public async Task<bool> IsNameExsit(string name)
        {
            var studentexist = await _studentRepository.GetTableNoTracking()
                .Where(s => s.Name.Equals(name))
                .FirstOrDefaultAsync();
            if(studentexist != null) { return true; }
            else { return false; }
        }

        public async Task<bool> IsNameExsitExcludeSelf(string name, int id)
        {
            var studentexist = await _studentRepository.GetTableNoTracking()
                .Where(s => s.Name.Equals(name)&!s.Id.Equals(id) )
                .FirstOrDefaultAsync();
            if (studentexist != null) { return true; }
            else { return false; }
        }

        public async Task<string> UpdateAsync(Student student)
        {
            var studentexist = await _studentRepository.GetTableAsTracking()
                .Where(s => s.Id==student.Id)
                .FirstOrDefaultAsync();

            if (studentexist == null)
            {
                return "NotExist";
            }
            studentexist.Name = student.Name;
            studentexist.Phone = student.Phone;
            studentexist.Address = student.Address;
            studentexist.DID = student.DID;

            await _studentRepository.UpdateAsync(studentexist);
            await _studentRepository.SaveChangesAsync();
            return "Update";
        }


        #endregion
    }
}
