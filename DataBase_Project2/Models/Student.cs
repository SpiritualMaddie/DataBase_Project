using System;
using System.Collections.Generic;

namespace DataBase_Project2.Models
{
    public partial class Student
    {
        public Student()
        {
            ClassLists = new HashSet<ClassList>();
            ContactInfos = new HashSet<ContactInfo>();
            CourseLists = new HashSet<CourseList>();
            GradeLists = new HashSet<GradeList>();
        }

        public int StudentId { get; set; }
        public string SocialSecurityNr { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FkClassCode { get; set; } = null!;

        public virtual Class FkClassCodeNavigation { get; set; } = null!;
        public virtual ICollection<ClassList> ClassLists { get; set; }
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
        public virtual ICollection<CourseList> CourseLists { get; set; }
        public virtual ICollection<GradeList> GradeLists { get; set; }
    }
}
