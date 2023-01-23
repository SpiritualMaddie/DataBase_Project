using System;
using System.Collections.Generic;

namespace DataBase_Project2.Models
{
    public partial class CourseList
    {
        public int CourseListId { get; set; }
        public string FkCourseCode { get; set; } = null!;
        public int FkEmployeeId { get; set; }
        public int? FkStudentId { get; set; }

        public virtual Course FkCourseCodeNavigation { get; set; } = null!;
        public virtual Employee FkEmployee { get; set; } = null!;
        public virtual Student? FkStudent { get; set; }
    }
}
