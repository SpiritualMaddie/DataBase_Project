using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataBase_Project2.Models;

namespace DataBase_Project2.Data
{
    public partial class HSHContext : DbContext
    {
        public HSHContext()
        {
        }

        public HSHContext(DbContextOptions<HSHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<ClassList> ClassLists { get; set; } = null!;
        public virtual DbSet<ContactInfo> ContactInfos { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseList> CourseLists { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<GradeList> GradeLists { get; set; } = null!;
        public virtual DbSet<Salary> Salarys { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=BLUEBOX; Initial Catalog=HighSchoolHigh; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.ClassCode)
                    .HasName("PK_Class");

                entity.Property(e => e.ClassCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName).HasMaxLength(25);
            });

            modelBuilder.Entity<ClassList>(entity =>
            {
                entity.Property(e => e.ClassListId).HasColumnName("ClassListID");

                entity.Property(e => e.FkClassCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FK_ClassCode");

                entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");

                entity.HasOne(d => d.FkClassCodeNavigation)
                    .WithMany(p => p.ClassLists)
                    .HasForeignKey(d => d.FkClassCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassList_Class");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.ClassLists)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassList_Employee");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.ClassLists)
                    .HasForeignKey(d => d.FkStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassList_Student");
            });

            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.Property(e => e.ContactInfoId).HasColumnName("ContactInfoID");

                entity.Property(e => e.City)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Street)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.ContactInfos)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .HasConstraintName("FK_ContactInfo_Employee");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.ContactInfos)
                    .HasForeignKey(d => d.FkStudentId)
                    .HasConstraintName("FK_ContactInfo_Student");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseCode)
                    .HasName("PK_Course");

                entity.Property(e => e.CourseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CourseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CourseList>(entity =>
            {
                entity.Property(e => e.CourseListId).HasColumnName("CourseListID");

                entity.Property(e => e.FkCourseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FK_CourseCode");

                entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");

                entity.HasOne(d => d.FkCourseCodeNavigation)
                    .WithMany(p => p.CourseLists)
                    .HasForeignKey(d => d.FkCourseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseList_Course");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.CourseLists)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseList_Employee");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.CourseLists)
                    .HasForeignKey(d => d.FkStudentId)
                    .HasConstraintName("FK_CourseList_Student");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstDayDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(25);

                entity.Property(e => e.LastName).HasMaxLength(25);

                entity.Property(e => e.SocialSecurityNr)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Titel)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GradeList>(entity =>
            {
                entity.Property(e => e.GradeListId).HasColumnName("GradeListID");

                entity.Property(e => e.FkCourseCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FK_CourseCode");

                entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");

                entity.Property(e => e.Grade)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.GradeDate).HasColumnType("date");

                entity.HasOne(d => d.FkCourseCodeNavigation)
                    .WithMany(p => p.GradeLists)
                    .HasForeignKey(d => d.FkCourseCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeList_Course");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.GradeLists)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeList_Employee");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.GradeLists)
                    .HasForeignKey(d => d.FkStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeList_Student");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.Property(e => e.SalaryId).HasColumnName("SalaryID");

                entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");

                entity.Property(e => e.MonthlySalary).HasColumnType("smallmoney");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.FirstName).HasMaxLength(25);

                entity.Property(e => e.FkClassCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FK_ClassCode");

                entity.Property(e => e.LastName).HasMaxLength(25);

                entity.Property(e => e.SocialSecurityNr)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkClassCodeNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.FkClassCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Class");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
