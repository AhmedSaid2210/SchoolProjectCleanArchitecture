using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        public int StudID { get; set; }
        public int SubID { get; set; }
        public decimal? Grade { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Subjects? Subject { get; set; }

    }
}
