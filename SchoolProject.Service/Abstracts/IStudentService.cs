using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        IQueryable<Student> GetStudentsQueryable();
        IQueryable<Student> GetStudentsByFilterQueryable(StudentOrderingEnum orderingEnum ,string? search =null);
        Task<Student> GetStudentById(int id);
        Task<string> AddAsync(Student student);
        Task<bool> IsNameExsit(string name);
        Task<bool> IsNameExsitExcludeSelf(string name,int id);
        Task<string> UpdateAsync(Student student);
        Task<bool> DeleteById(int id);
    }
}
