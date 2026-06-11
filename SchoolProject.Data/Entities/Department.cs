using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace SchoolProject.Data.Entities
{
    public class Department : AuditableEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Instructors = new HashSet<Instructor>();
            DepartmentSubjects = new HashSet<DepartmetSubject>();
        }
 
        public string DName { get; set; }

        public int? InstructorManagerID { get; set; }
        public virtual Instructor? InstructorManager { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }

        public virtual ICollection<DepartmetSubject> DepartmentSubjects { get; set; }
    }

}
