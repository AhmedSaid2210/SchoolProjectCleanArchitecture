using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Data.Entities
{
    public class InstructorSubject
    {
        public int InsId { get; set; }
        public int SubId { get; set; }
        public virtual Instructor? Instructor { get; set; }
        public virtual Subjects? Subjects { get; set; }

    }
}
