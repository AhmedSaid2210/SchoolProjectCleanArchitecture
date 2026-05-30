

namespace SchoolProject.Data.Entities
{
    public class Subjects: AuditableEntity
    {
        public Subjects()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmetsSubjects = new HashSet<DepartmetSubject>();
            InstructorSubjects = new HashSet<InstructorSubject>();

        }
        public string SubjectName { get; set; }
        public DateTime Period { get; set; }

        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        public virtual ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }

    }
}
