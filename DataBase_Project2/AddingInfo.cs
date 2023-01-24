using DataBase_Project2.Data;
using DataBase_Project2.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace DataBase_Project2
{
    internal class AddingInfo
    {
        // Connection string, Data Adapter and Data table
        private SqlConnection sqlCon = new SqlConnection("Data Source=BLUEBOX; Initial Catalog=HighSchoolHigh; Integrated Security=true");
        internal SqlDataAdapter? sqlDa;
        internal DataTable? dataT;

        // Adds new employee to the database (SQL)
        internal void AddEmployee()
        {
            Application app = new Application();
            Console.Clear();
            Console.WriteLine("Här kan du lägga till en ny anställd.");

            Console.WriteLine("\nPersonnr (ÅÅÅÅMMDD-NNNN):");
            string socialSecNr = NullCheck();

            Console.WriteLine("\nFörnamn:");
            string inputFirstName = NullCheck();
            string firstName = BigFirstLetter(inputFirstName);

            Console.WriteLine("\nEfternamn:");
            string inputLastName = NullCheck();
            string lastName = BigFirstLetter(inputLastName);

            Console.WriteLine("\nTitel: \n(Lärare, Administratör, Rektor, Lokalvårdare, Specialpedagog)");
            string inputTitle = NullCheck();
            string title = BigFirstLetter(inputTitle);

            Console.WriteLine("\nFörsta arbetsdagen (ÅÅÅÅ-MM-DD):");
            var firstDayDate = NullCheck();

            Console.WriteLine("\nAvdelning: \n(Bild, Musik, Foto/Film, Estetiska programmet");
            Console.WriteLine("(Om ingen specifik avdelning finns, skriv Ospec)");
            string inputDepartment = NullCheck();
            string department = BigFirstLetter(inputDepartment);

            var newEmployee = "INSERT INTO Employees (SocialSecurityNr, FirstName, LastName, Titel, FirstDayDate, Department) " +
                "VALUES (@socialSecNr, @firstName, @lastName, @title, @firstDayDate, @department)";

            using (SqlCommand sqlCom = new SqlCommand(newEmployee, sqlCon))
            {
                sqlCom.Parameters.AddWithValue("@socialSecNr", socialSecNr);
                sqlCom.Parameters.AddWithValue("@firstName", firstName);
                sqlCom.Parameters.AddWithValue("@lastName", lastName);
                sqlCom.Parameters.AddWithValue("@title", title);
                sqlCom.Parameters.AddWithValue("@firstDayDate", firstDayDate);
                sqlCom.Parameters.AddWithValue("@department", department);

                sqlCon.Open();
                sqlCom.ExecuteNonQuery();
                sqlCon.Close();
            }
            Console.WriteLine("\nDu har lagt till en ny anställd till databasen!\nTryck Enter för att komma vidare.");
            Console.ReadKey();
        }
        
        // Adding new Student (SQL)
        internal void AddStudents()
        {
            Application app = new Application();
            Console.Clear();
            Console.WriteLine("Här kan du lägga till en ny student.");
            Console.WriteLine("\nPersonnr(ÅÅÅÅMMDD-NNNN):");
            var socialSecNr = NullCheck();
            Console.WriteLine("\nFörnamn:");
            var firstName = NullCheck();
            Console.WriteLine("\nEfternamn:");
            var lastName = NullCheck();
            Console.WriteLine("\nKlasskod:\n(ES20C - Bild, ES21B - Foto/Film, ES22A - Musik)");
            var studClassCode = NullCheck().ToUpper();

            var newStudent = "INSERT INTO Students (SocialSecurityNr, FirstName, LastName, FK_ClassCode) " +
                "VALUES (@socialSecNr, @firstName, @lastName, @studClassCode)";

            using (SqlCommand sqlCom = new SqlCommand(newStudent, sqlCon))
            {
                sqlCom.Parameters.AddWithValue("@socialSecNr", socialSecNr);
                sqlCom.Parameters.AddWithValue("@firstName", firstName);
                sqlCom.Parameters.AddWithValue("@lastName", lastName);
                sqlCom.Parameters.AddWithValue("@studClassCode", studClassCode);

                sqlCon.Open();
                sqlCom.ExecuteNonQuery();
                sqlCon.Close();
            }
            Console.WriteLine("\nDu har lagt till en ny student till databasen!\nTryck Enter för att komma vidare.");
            Console.ReadKey();
        }

        // Sets new grade (SQL)
        internal void SetGrade()
        {
            Console.Clear();
            var courseCode = CourseCodeInDbOrNot();
            
            ListOfStudentsCourse(courseCode);
            Console.WriteLine("\nSkriv in ID:et på den student du vill betygsätta:");
            var studentID = NullCheck();
            
            Console.WriteLine("\nBetyg (A, B, C, D, E, F):");
            var grade = NullCheck().ToUpper();

            // Make sures the grade can only be A, B, C, D, E, F. But then there will be no error message from transaction
            //GradeCheck(grade);
            var gradeDate = DateTime.Now;

            ListOfTeachersCourse(courseCode);
            Console.WriteLine("\nSkriv in lärarens ID som satt betyget:");
            var employeeID = NullCheck();

            using (SqlConnection sqlCon = new SqlConnection("Data Source=BLUEBOX; Initial Catalog=HighSchoolHigh; Integrated Security=true"))
            {
                sqlCon.Open();

                // Start a new transaction
                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                {
                    try
                    {
                        // Insert the new grade for the student
                        using (SqlCommand command = new SqlCommand("INSERT INTO GradeLists " +
                            "(Grade, GradeDate, FK_CourseCode, FK_StudentID, FK_EmployeeID) " +
                            "VALUES (@grade, @gradeDate, @courseCode, @studentID, @employeeID)", sqlCon))
                        {
                            // Assign transaction
                            command.Transaction = transaction;

                            // Add the input parameters
                            command.Parameters.Add("@grade", SqlDbType.VarChar).Value = grade;
                            command.Parameters.Add("@gradeDate", SqlDbType.Date).Value = gradeDate.ToShortDateString();
                            command.Parameters.Add("@courseCode", SqlDbType.VarChar).Value = courseCode;
                            command.Parameters.Add("@studentID", SqlDbType.Int).Value = studentID;
                            command.Parameters.Add("@employeeID", SqlDbType.Int).Value = employeeID;
                            command.ExecuteNonQuery();
                        }

                        // If everything goes well, commit the transaction
                        transaction.Commit();
                        Console.WriteLine("Betyget har lagts till. Tryck enter för att komma vidare.");
                        Console.ReadKey();
                    }
                    catch (SqlException ex)
                    {
                        // If an exception is thrown, the transaction is automatically rolled back
                        Console.WriteLine("\nNågot gick fel.");
                        Console.WriteLine($"[ Error message: {ex.Message} ]");
                        Console.WriteLine("\n-----------------------------------------------------------------------");
                        Console.WriteLine("Tryck enter för att komma tillbaka till huvudmenyn");
                        Console.ReadKey();
                    }
                    Application app = new Application();
                    app.Start();
                }
                sqlCon.Close();
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

        // Checks if user input "grade" is correct
        internal static string GradeCheck(string userInput)
        {
            while (true)
            {
                userInput = Console.ReadLine();
                if (userInput != "A" | userInput != "B" | userInput != "C" | userInput != "D" | userInput != "E" | userInput != "F")
                {
                    Console.WriteLine("Du har valt ett betyg som inte finns, försök igen");
                }
                else
                {
                    return userInput;
                }
            }
        }
        
        // Makes the user input have the first letter in uppercase
        internal static string BigFirstLetter(string input)
        {
            var Firstletter = input[..1].ToUpper();
            var Everthingelse = input[1..].ToLower();
            var output = Firstletter + Everthingelse;
            return output;
        }

        // Checks if the course code is in the database
        internal string CourseCodeInDbOrNot()
        {
            while (true)
            {
                Console.WriteLine("Här kan du lägga till ett nytt betyg.");
                Console.WriteLine("\nKurskod:\n(Aktiva kurser: MUHI22, PIANO23, RITA23)");
                var courseCode = NullCheck().ToUpper();
                using (SqlConnection sqlCon = new SqlConnection("Data Source=BLUEBOX; Initial Catalog=HighSchoolHigh; Integrated Security=true"))
                {
                    sqlCon.Open();
                    string query = "SELECT * FROM CourseLists WHERE FK_CourseCode = @courseCode";
                    using (SqlCommand command = new SqlCommand(query, sqlCon))
                    {
                        command.Parameters.AddWithValue("@courseCode", courseCode);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                sqlCon.Close();
                                return courseCode;
                            }
                            else
                            {
                                Console.WriteLine("Kurskoden '{0}' finns inte i databasen.", courseCode);
                                Console.WriteLine("Försök igen.");
                                Thread.Sleep(2500);
                                Console.Clear();
                            }
                        }
                    }
                }
            }
        }

        // Gets a list of the students in a specific course
        internal void ListOfStudentsCourse(string courseCode)
        {
            Console.WriteLine("\nHär kommer en lista på alla studenter i den kursen:");
            Console.WriteLine("-----------------------------------------------------------------------");
            using (SqlConnection sqlCon = new SqlConnection("Data Source=BLUEBOX; Initial Catalog=HighSchoolHigh; Integrated Security=true"))
            {
                sqlCon.Open();
                var studentCourseList = @"
                                    SELECT FirstName, LastName, StudentID FROM CourseLists                               
                                    JOIN Students ON StudentID = CourseLists.FK_StudentID
                                    WHERE FK_CourseCode = @courseCode";

                var command = new SqlCommand(studentCourseList, sqlCon);

                command.Parameters.AddWithValue("@courseCode", courseCode);
                sqlDa = new SqlDataAdapter(command);
                dataT = new DataTable();
                //sqlDa = new SqlDataAdapter(studentCourseList, sqlCon);
                //sqlDa.SelectCommand.Parameters.AddWithValue("@courseCode", courseCode);
                //dataT = new DataTable();
                
                sqlDa.Fill(dataT);

                Console.WriteLine("Kurskod \tID \t\tNamn\n");
                foreach (DataRow drStudCourse in dataT.Rows)
                {
                    Console.Write(courseCode + "\t\t");
                    Console.Write(drStudCourse["StudentID"] + "\t\t");
                    Console.Write(drStudCourse["FirstName"] + ", ");
                    Console.Write(drStudCourse["LastName"]);
                    Console.WriteLine();
                }
                sqlCon.Close();
            }
        }

        // Gets a list of the teachers in a specific course
        internal void ListOfTeachersCourse(string courseCode)
        {
            Console.WriteLine("\nHär är läraren i den kursen:");
            Console.WriteLine("-----------------------------------------------------------------------");
            using (SqlConnection sqlCon = new SqlConnection("Data Source=BLUEBOX; Initial Catalog=HighSchoolHigh; Integrated Security=true"))
            {
                sqlCon.Open();
                var teacherCourseList = @"
                                SELECT DISTINCT FirstName, LastName, EmployeeID FROM CourseLists                               
                                JOIN Employees ON EmployeeID = CourseLists.FK_EmployeeID
                                WHERE FK_CourseCode = @courseCode";

                sqlDa = new SqlDataAdapter(teacherCourseList, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@courseCode", courseCode);
                dataT = new DataTable();
                sqlDa.Fill(dataT);

                Console.WriteLine("Kurskod \tID \t\tNamn\n");
                foreach (DataRow drTeachCourse in dataT.Rows)
                {
                    Console.Write(courseCode + "\t\t");
                    Console.Write(drTeachCourse["EmployeeID"] + "\t\t");
                    Console.Write(drTeachCourse["FirstName"] + ", ");
                    Console.Write(drTeachCourse["LastName"]);
                    Console.WriteLine();
                }
                sqlCon.Close();
            }
        }
    }
}
