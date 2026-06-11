using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;



namespace SchoolProject.Data.Entities
{
    public class Student: AuditableEntity
    {
        public Student()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
        }
        public string Name { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(500)]
        public string Phone { get; set; }
        public int? DID { get; set; }

        public int DID { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        
    }

   
}
