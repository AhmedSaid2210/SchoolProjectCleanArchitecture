using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Data.Entities
{
    public class Instructor : AuditableEntity
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();

        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }


        public int? DID { get; set; }
        public virtual Department? Department { get; set; }

        public virtual Department? DepartmentManger { get; set; }


        public virtual int? SupervisorID { get; set; }
        public virtual Instructor? InstructorSupervisor { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }


        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }



    }
}
