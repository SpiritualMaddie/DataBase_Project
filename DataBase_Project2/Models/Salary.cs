using System;
using System.Collections.Generic;

namespace DataBase_Project2.Models
{
    public partial class Salary
    {
        public int SalaryId { get; set; }
        public decimal MonthlySalary { get; set; }
        public int FkEmployeeId { get; set; }
    }
}
