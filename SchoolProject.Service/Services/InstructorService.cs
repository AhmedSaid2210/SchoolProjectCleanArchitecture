
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<string> AddAsync(Instructor instructor)
        {
            var InstructorExist = await _instructorRepository.GetTableNoTracking()
                                                             .Where(i=>i.IsDeleted.Equals(false))
                                                             .Where(i=>i.Name.Equals(instructor.Name))
                                                             .ToListAsync();

            if (InstructorExist.Any()) return "Exist";

            await _instructorRepository.AddAsync(instructor);

            return "Success";
        }

        public async Task<string> DeleteById(int id)
        {
            var instructor = await GetInstructorById(id);

            if (instructor == null) return "Instructor Is Not Exist";

            if (instructor.Instructors.Any()) return "Instructor Supervisor Has Instructors";

            if (instructor.DepartmentManger != null) return "Instructor Is Manager For Deparment";


            await _instructorRepository.DeleteAsync(instructor);

            return "Success";

        }

        public async Task<List<Instructor>> GetAllInstructors()
        {
            var instructors = await _instructorRepository.GetTableNoTracking()
                                                        .Include(i => i.Department)
                                                        .Include(d => d.DepartmentManger)
                                                        .Include(d => d.InstructorSupervisor)
                                                        .Include(i => i.Instructors)
                                                        .Where(i=> i.IsDeleted.Equals(false))
                                                        .ToListAsync();
            return instructors;
        }

        public async Task<Instructor> GetInstructorById(int id)
        {
            var instructor = await _instructorRepository.GetTableNoTracking()
                                                      .Include(i => i.Department)
                                                      .Include(d => d.DepartmentManger)
                                                      .Include(d => d.InstructorSupervisor)
                                                      .Include(i => i.Instructors)
                                                      .Include(s=>s.InstructorSubjects)
                                                      .ThenInclude(s=>s.Subjects)
                                                      .Where(i => i.IsDeleted.Equals(false))
                                                      .Where(i=>i.Id == id)
                                                      .FirstOrDefaultAsync();
            if(instructor == null) return null;



            return instructor;
        }

        public IQueryable<Instructor> GetInstructorsByFilterQueryable(InstructorOrderingEnum orderingEnum, string? search = null)
        {
            var instructors =  _instructorRepository.GetTableNoTracking()
                                                       .Include(i => i.Department)
                                                       .Include(d => d.DepartmentManger)
                                                       .Include(d => d.InstructorSupervisor)
                                                       .Include(i => i.Instructors.Where(i => i.IsDeleted.Equals(false)))
                                                       .Where(i => i.IsDeleted.Equals(false))
                                                       .AsQueryable();

            if (search != null) instructors.Where(i => i.Name.Contains(search) || i.Position.Contains(search));

            switch (orderingEnum)
            {
                case InstructorOrderingEnum.Id:
                    instructors = instructors.OrderBy(i => i.Id);
                    break;
                case InstructorOrderingEnum.Position:
                    instructors = instructors.OrderBy(i => i.Position);
                    break;
                case InstructorOrderingEnum.Address:
                    instructors = instructors.OrderBy(i => i.Address);
                    break;
                case InstructorOrderingEnum.Name:
                    instructors = instructors.OrderBy(i => i.Name);
                    break;
                case InstructorOrderingEnum.Salary:
                    instructors = instructors.OrderBy(i => i.Salary);
                    break;
            }
            return instructors;
        }

        public IQueryable<Instructor> GetInstructorsQueryable()
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

        public async Task<string> UpdateAsync(Instructor instructor)
        {
            var instructorExist = await GetInstructorById(instructor.Id);

            if (instructorExist == null) return "Instructor Is Not Exist";

            var instructorNameExist = await _instructorRepository.GetTableNoTracking()
                                                      .Where(i => i.IsDeleted.Equals(false))
                                                      .Where(i => i.Name == instructor.Name)
                                                      .FirstOrDefaultAsync();

            if (instructorNameExist != null) return "Instructor Name Is Aready Exist";


            await _instructorRepository.UpdateAsync(instructor);

            return "Success";
        }
    }
}
