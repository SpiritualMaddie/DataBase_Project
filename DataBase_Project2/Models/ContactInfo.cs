using System;
using System.Collections.Generic;

namespace DataBase_Project2.Models
{
    public partial class ContactInfo
    {
        public int ContactInfoId { get; set; }
        public string? Street { get; set; }
        public string? PostCode { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public int? FkStudentId { get; set; }
        public int? FkEmployeeId { get; set; }

        public virtual Employee? FkEmployee { get; set; }
        public virtual Student? FkStudent { get; set; }
    }
}
