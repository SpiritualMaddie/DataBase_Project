using System;
using System.Collections.Generic;

namespace DataBase_Project2.Models
{
    public partial class Class
    {
        public Class()
        {
            ClassLists = new HashSet<ClassList>();
            Students = new HashSet<Student>();
        }

        public string ClassCode { get; set; } = null!;
        public string ClassName { get; set; } = null!;

        public virtual ICollection<ClassList> ClassLists { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
