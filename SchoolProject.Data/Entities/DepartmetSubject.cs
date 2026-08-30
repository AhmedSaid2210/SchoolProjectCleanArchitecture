using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolProject.Data.Entities
{
    public class DepartmetSubject
    {
        public int DID { get; set; }
        public int SubID { get; set; }
        public virtual Department? Department { get; set; }
        public virtual Subjects? Subjects { get; set; }
    }

}
