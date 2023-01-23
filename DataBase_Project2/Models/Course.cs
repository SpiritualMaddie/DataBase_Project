using System;
using System.Collections.Generic;

namespace DataBase_Project2.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseLists = new HashSet<CourseList>();
            GradeLists = new HashSet<GradeList>();
        }

        public string CourseCode { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string? Status { get; set; }

        public virtual ICollection<CourseList> CourseLists { get; set; }
        public virtual ICollection<GradeList> GradeLists { get; set; }
    }
}
