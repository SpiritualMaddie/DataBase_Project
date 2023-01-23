using System;
using System.Collections.Generic;

namespace DataBase_Project2.Models
{
    public partial class GradeList
    {
        public int GradeListId { get; set; }
        public string Grade { get; set; } = null!;
        public DateTime GradeDate { get; set; }
        public string FkCourseCode { get; set; } = null!;
        public int FkStudentId { get; set; }
        public int FkEmployeeId { get; set; }

        public virtual Course FkCourseCodeNavigation { get; set; } = null!;
        public virtual Employee FkEmployee { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
    }
}
