using DataBase_Project2.Data;
using DataBase_Project2.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataBase_Project2
{
    internal class ViewingInfo
    {
        private SqlConnection sqlCon = new SqlConnection("Data Source=BLUEBOX; Initial Catalog=HighSchoolHigh; Integrated Security=true");
        internal SqlDataAdapter? sqlDa;
        internal DataTable? dataT;

        // Get list of all Employees (SQL)
        internal void ViewListOfEmployees()
        {
            sqlDa = new SqlDataAdapter("SELECT FirstName, LastName, Titel, Department, " +
                "DATEDIFF(year, FirstDayDate, GETDATE()) as YearsWorked FROM Employees", sqlCon);

            dataT = new DataTable();

            sqlDa.Fill(dataT);
            
            foreach (DataRow drEmp in dataT.Rows)
            {
                Console.WriteLine("-----------------------------------");
                Console.Write(drEmp["FirstName"] + ", ");
                Console.Write(drEmp["LastName"] + " - ");
                Console.WriteLine(drEmp["Titel"]);
                Console.WriteLine("Har arbetat på skolan i: " + drEmp["YearsWorked"] + " år");
            }
        }
        
        // Get list of all Employees with specific title (SQL)
        internal void ViewTitleEmployee()
        {
            Console.Clear();
            Console.WriteLine("Skriv in vilken titel du söker efter:\n(Lärare, Administratör, Rektor, Lokalvårdare, Specialpedagog)\n");
            var inputTitle = NullCheck();
            var firstletter = inputTitle[..1].ToUpper();
            var everthingelse = inputTitle[1..].ToLower();
            var title = firstletter + everthingelse;

            sqlDa = new SqlDataAdapter("Select * From Employees Where Titel ='" + title + "'", sqlCon);
            dataT = new DataTable();
            sqlDa.Fill(dataT);

            Console.Clear();
            if (title == "Lärare" | title == "Administratör" | title == "Rektor"
                | title == "Lokalvårdare" | title == "Specialpedagog")
            {
                Console.WriteLine("Titel \t\tNamn\n");
                foreach (DataRow drEmpTitel in dataT.Rows)
                {
                    Console.Write(drEmpTitel["Titel"] + "\t\t");
                    Console.Write(drEmpTitel["FirstName"] + ", ");
                    Console.Write(drEmpTitel["LastName"]);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Tyvärr denna yrkestitel finns inte på skolan, försök igen genom att trycka Enter.");
                Console.ReadKey();
                ViewTitleEmployee();
            }
        }
        
        // Get list of all students (Entity framwork)
        // Sorted by FirstName Ascending
        internal void ViewStudentsSortFNameAsc()
        {
            using (var context = new HSHContext())
            {
                var myStudents = context.Students.OrderBy(s => s.FirstName);
                Console.WriteLine("StudentID \t Klasskod \t Namn");
                foreach (var students in myStudents)
                {
                    Console.WriteLine($"{students.StudentId} \t\t {students.FkClassCode} \t\t {students.FirstName}, {students.LastName}");
                }
            }
        }
        
        // Get list of all students (Entity framwork)
        // Sorted by FirstName Descending
        internal void ViewStudentsSortFNameDesc()
        {
            using (var context = new HSHContext())
            {
                var myStudents = context.Students.OrderByDescending(s => s.FirstName);
                Console.WriteLine("StudentID \t Klasskod \t Namn");
                foreach (var students in myStudents)
                {
                    Console.WriteLine($"{students.StudentId} \t\t {students.FkClassCode} \t\t {students.FirstName}, {students.LastName}");
                }
            }

        }
        
        // Get list of all students (Entity framwork)
        // Sorted by LastName Ascending
        internal void ViewStudentsSortLNameAsc()
        {
            using (var context = new HSHContext())
            {
                var myStudents = context.Students.OrderBy(s => s.FirstName);
                Console.WriteLine("StudentID \t Klasskod \t Namn");
                foreach (var students in myStudents)
                {
                    Console.WriteLine($"{students.StudentId} \t\t {students.FkClassCode} \t\t {students.LastName}, {students.FirstName}");
                }
            }
        }
        
        // Get list of all students (Entity framwork)
        // Sorted by LastName Descending
        internal void ViewStudentsSortLNameDesc()
        {
            using (var context = new HSHContext())
            {
                var myStudents = context.Students.OrderByDescending(s => s.LastName);
                Console.WriteLine("StudentID \t Klasskod \t Namn");
                foreach (var students in myStudents)
                {
                    Console.WriteLine($"{students.StudentId} \t\t {students.FkClassCode} \t\t {students.LastName}, {students.FirstName}");
                }
            }
        }

        // Get list of all students (Entity Framework)
        internal void ViewAllStudents()
        {
            using (var context = new HSHContext())
            {
                var allStudents = context.Students.OrderBy(s => s.FkClassCode);

                Console.WriteLine("StudentID \t Klasskod \tNamn\n");
                foreach (var students in allStudents)
                {
                    Console.WriteLine($"{students.StudentId} \t\t {students.FkClassCode} \t\t{students.FirstName}, {students.LastName}");
                }
            }
        }
        
        // Show list of classes and get all students in specific class (Entity)
        internal void ViewClasses()
        {
            Console.Clear();
            Application app = new Application();
            using (var context = new HSHContext())
            {
                Console.WriteLine("Här är en lista med alla klasser i High School High\n");
                var myClasses = context.Classes;

                Console.WriteLine("Klasskod\tKlass");
                foreach (var schoolClass in myClasses)
                {
                    Console.WriteLine($"{schoolClass.ClassCode}\t\t{schoolClass.ClassName}");
                }

                Console.WriteLine("\nVänligen skriv in vilken klasskod du vill se studenter från:");
                var inputClassCode = NullCheck().ToUpper();

                if (inputClassCode == "ES20C" | inputClassCode == "ES21B" | inputClassCode == "ES22A")
                {
                    var myStudentsInClass = context.Students
                                .Where(s => s.FkClassCode == inputClassCode);
                    Console.Clear();
                    Console.WriteLine("\nStudentID \t Klasskod \t Namn");
                    foreach (var studentClass in myStudentsInClass)
                    {
                        Console.WriteLine($"{studentClass.StudentId} \t\t {studentClass.FkClassCode} \t\t {studentClass.FirstName}, {studentClass.LastName}");
                    }
                }
                else
                {
                    Console.WriteLine("\nTyvärr klassen du har valt finns inte, tryck Enter för att försöka igen.");
                    Console.ReadKey();
                    ViewClasses();
                }
                app.PromptAfterList();
                app.PromptClass();
            }
        }
        
        // Get all grades set in the past month (SQL)
        internal void AllGradesLastMonth()
        {
            var currentDate = DateTime.Now;
            var last2Month = currentDate.AddMonths(-2);
            var gradeInfo = @"
                        SELECT * FROM GradeLists
                        JOIN Students ON StudentID = GradeLists.FK_StudentID
                        JOIN Courses ON CourseCode = GradeLists.FK_CourseCode
                        WHERE GradeDate > @last2Month";
            var command = new SqlCommand(gradeInfo, sqlCon);

            command.Parameters.AddWithValue("@last2Month", last2Month.ToShortDateString());
            sqlDa = new SqlDataAdapter(command);
            dataT = new DataTable();

            sqlDa.Fill(dataT);

            Console.Clear();
            Console.WriteLine("\nKurs \t\t\tBetyg \tDatum \t\tStudent");
            foreach (DataRow drGrades in dataT.Rows)
            {
                Console.Write(drGrades["CourseName"] + "\t");
                Console.Write(drGrades["Grade"] + "\t");
                Console.Write(Convert.ToDateTime(drGrades["GradeDate"]).ToShortDateString() + "\t");
                Console.Write(drGrades["FirstName"] + ", ");
                Console.Write(drGrades["LastName"]);
                Console.WriteLine();
            }
        }

        // List of average, highest and lowest grade grouped by course (SQL)
        internal void AverageGradeHighLow()
        {
            var averageGrade = @"
                        SELECT CourseName, AVG(CASE 
                                WHEN Grade = 'A' THEN 5
                                WHEN Grade = 'B' THEN 4
                                WHEN Grade = 'C' THEN 3
                                WHEN Grade = 'D' THEN 2
                                WHEN Grade = 'E' THEN 1
                                WHEN Grade = 'F' THEN 0
                                ELSE 0 END) AS AverageGrade, 
                        MIN(Grade) AS HighGrade, MAX(Grade) AS LowGrade
                        FROM GradeLists
                        JOIN Students ON StudentID = GradeLists.FK_StudentID
                        JOIN Courses ON CourseCode = GradeLists.FK_CourseCode
                        GROUP BY CourseName, CourseName";
            var command = new SqlCommand(averageGrade, sqlCon);

            sqlDa = new SqlDataAdapter(command);
            dataT = new DataTable();

            sqlDa.Fill(dataT);
            Console.WriteLine(@"
Betygen kommer att numreras för att få fram ett snittvärde
(A = 5, B = 4, C = 3, D = 2, E = 1, F = 0)");
            Console.WriteLine("\nSnittbetyg \tLängsta/högsta betyget \tKursnamn\n");
            foreach (DataRow drGradesA in dataT.Rows)
            {
                Console.Write(drGradesA["AverageGrade"] + "\t\t");
                Console.Write(drGradesA["LowGrade"] + "  /  ");
                Console.Write(drGradesA["HighGrade"] + "\t\t\t");
                Console.Write(drGradesA["CourseName"]);
                Console.WriteLine();
            }
        }

        // Shows teachers in specific department (Entity Framework)
        internal void TeachersDep()
        {
            using (var context = new HSHContext())
            {

                var teachersByDepartment = from e in context.Employees
                                           where new[] { "Lärare", "Specialpedagog" }.Contains(e.Titel)
                                           group e by e.Department into g
                                           select new { Department = g.Key, NumberOfTeachers = g.Count() };

                foreach (var item in teachersByDepartment)
                {
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine($"Avdelning: {item.Department}");
                    Console.WriteLine($"Antal personal: {item.NumberOfTeachers} st");
                }
            }
        }

        // Salary of every department: total (SQL)
        internal void SalaryDep()
        {
            var totalSalary = @"
                        SELECT Department, SUM(MonthlySalary) AS total
                        FROM Employees
                        JOIN Salarys ON EmployeeID = Salarys.FK_EmployeeID
                        GROUP BY Department";
            var command = new SqlCommand(totalSalary, sqlCon);

            sqlDa = new SqlDataAdapter(command);
            dataT = new DataTable();

            sqlDa.Fill(dataT);
            foreach (DataRow drDep in dataT.Rows)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine($"Avdelning: {drDep["Department"]}");
                Console.WriteLine($"Total lön: {Math.Round((decimal)drDep["total"], 2)} kr");
            }
        }

        // Salary of every department: average (SQL)
        internal void SalaryDepAve()
        {
            var averageSalary = @"
                        SELECT Department, AVG(MonthlySalary) AS average
                        FROM Employees
                        JOIN Salarys ON EmployeeID = Salarys.FK_EmployeeID
                        GROUP BY Department";
            var command = new SqlCommand(averageSalary, sqlCon);

            sqlDa = new SqlDataAdapter(command);
            dataT = new DataTable();

            sqlDa.Fill(dataT);
            foreach (DataRow drDep in dataT.Rows)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine($"Avdelning: {drDep["Department"]}");
                Console.WriteLine($"Medellönen: {Math.Round((decimal)drDep["average"], 2)} kr");
            }
        }

        // Shows info aboout specific student id (SQL) Calling stored procedure
        internal void StudentInfoId()
        {
            Console.WriteLine("\nSkriv in ett student ID för att se info om denna student:");
            var studentID = NullCheck();

            using (sqlCon)
            {
                sqlCon.Open();

                using (SqlCommand command = new SqlCommand("spGetStudentInfo", sqlCon))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter studentIdParameter = new SqlParameter();
                    studentIdParameter.ParameterName = "@StudentID";
                    studentIdParameter.Value = studentID;
                    command.Parameters.Add(studentIdParameter);

                    sqlDa = new SqlDataAdapter(command);
                    dataT = new DataTable();
                    sqlDa.Fill(dataT);

                    foreach (DataRow drStud in dataT.Rows)
                    {
                        Console.Clear();
                        Console.WriteLine("-----------------------------------------------------------------------");
                        Console.WriteLine("Student ID: " + drStud["StudentID"]);
                        Console.WriteLine("Klass: " + drStud["FK_ClassCode"]);
                        Console.WriteLine("Personnr: " + drStud["SocialSecurityNr"]);
                        Console.Write("Namn: " + drStud["FirstName"]);
                        Console.WriteLine(", " + drStud["LastName"]);
                    }
                }
            }
            sqlCon.Close();
        }

        // List of all active courses (Entity Framework)
        internal void ListActiveCourses()
        {
            using (var context = new HSHContext())
            {
                var activeCourses = context.Courses.Where(c => c.Status == "Active").ToList();

                Console.WriteLine("Aktiva kurser:\n" +
                    "");
                foreach (var course in activeCourses)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine($"Kurskod: {course.CourseCode} \nKursnamn: {course.CourseName}");
                }
            }
        }

        // Checks if unser input is null
        internal static string NullCheck()
        {
            while (true)
            {
                var userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput) | string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Du måste skriva något, försök igen");
                }
                else
                {
                    return userInput;
                }
            }
        }
    }
}
