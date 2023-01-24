using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase_Project2
{
    internal class Application
    {
        internal void Start()
        {
            Console.Title = "High School High - app";
            string prompt = $"Välkommen till High School Highs applikation.\n(Använd pilarna för att navigera menyn och gör val med Enter.)\n";
            string[] options = {
                "Personal", "Studenter", "Betyg", "Löner", "Aktiva kurser", "Avsluta" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();
            ViewingInfo viewingInfo = new ViewingInfo();

            switch (menuSelect)
            {
                case 0:
                    EmployeesMenu();
                    break;
                case 1:
                    StudentsMenu();
                    break;
                case 2:
                    GradesMenu();
                    break;
                case 3:
                    SalaryMenu();
                    break;
                case 4:
                    Console.Clear();
                    viewingInfo.ListActiveCourses();
                    PromptAfterList();
                    Start();
                    break;
                case 5:
                    Exit();
                    break;
            }
        }
        internal void EmployeesMenu()
        {
            string prompt = $"Personal-meny.\n";
            string[] options = {
                "Visa all personal", "Visa personal efter titel", "Lägg till ny personal",
                "Avdelningar", "Huvudmeny" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();
            AddingInfo addInfo = new AddingInfo();
            ViewingInfo viewingInfo = new ViewingInfo();

            switch (menuSelect)
            {
                case 0:
                    Console.Clear();
                    viewingInfo.ViewListOfEmployees();
                    PromptAfterList();
                    EmployeesMenu();
                    break;
                case 1:
                    Console.Clear();
                    viewingInfo.ViewTitleEmployee();
                    PromptAfterList();
                    EmployeesMenu();
                    break;
                case 2:
                    Console.Clear();
                    addInfo.AddEmployee();
                    EmployeesMenu();
                    break;
                case 3:
                    Console.Clear();
                    viewingInfo.TeachersDep();
                    PromptAfterList();
                    EmployeesMenu();
                    break;
                case 4:
                    Start();
                    break;
            }
        }
        internal void StudentsMenu()
        {
            string prompt = $"Student-meny.\n";
            string[] options = {
                "Alla studenter", "Sorterade på namn", "Sök student med ID",
                "Lägg till ny student", "Huvudmeny" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();
            ViewingInfo viewingInfo = new ViewingInfo();
            AddingInfo addingInfo = new AddingInfo();

            switch (menuSelect)
            {
                case 0:
                    Console.Clear();
                    viewingInfo.ViewAllStudents();
                    PromptAfterList();
                    StudentsMenu();
                    break;
                case 1:
                    Console.Clear();
                    SortStudentsName();
                    break;
                case 2:
                    Console.Clear();
                    viewingInfo.ViewAllStudents();
                    viewingInfo.StudentInfoId();
                    PromptAfterList();
                    StudentsMenu();
                    break;
                case 3:
                    Console.Clear();
                    addingInfo.AddStudents();
                    StudentsMenu();
                    break;
                case 4:
                    Start();
                    break;
            }
        }
        internal void GradesMenu()
        {
            string prompt = $"Betyg-meny.\n";
            string[] options = {
                "Alla betyg satta senaste 2 mån", "Snittbetyg & högsta vs lägsta", "Sätt betyg", "Huvudmeny" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();
            AddingInfo addingInfo = new AddingInfo();
            ViewingInfo viewingInfo = new ViewingInfo();

            switch (menuSelect)
            {
                case 0:
                    Console.Clear();
                    viewingInfo.AllGradesLastMonth();
                    PromptAfterList();
                    GradesMenu();
                    break;
                case 1:
                    Console.Clear();
                    viewingInfo.AverageGradeHighLow();
                    PromptAfterList();
                    GradesMenu();
                    break;
                case 2:
                    Console.Clear();
                    addingInfo.SetGrade();
                    PromptAfterList();
                    GradesMenu();
                    break;
                case 3:
                    Start();
                    break;
            }
        }
        internal void SalaryMenu()
        {
            string prompt = $"Löner-meny.\n";
            string[] options = {
                "Lön/avdelning", "Medellön/avdelning", "Huvudmeny" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();
            ViewingInfo viewingInfo = new ViewingInfo();

            switch (menuSelect)
            {
                case 0:
                    Console.Clear();
                    viewingInfo.SalaryDep();
                    PromptAfterList();
                    SalaryMenu();
                    break;
                case 1:
                    Console.Clear();
                    viewingInfo.SalaryDepAve();
                    PromptAfterList();
                    SalaryMenu();
                    break;
                case 2:
                    Start();
                    break;
            }
        }
        internal void SortStudentsName()
        {
            string prompt = $"Välj om du vill sortera studenterna efter för eller efternamn i stigande eller fallande ordning.\n";
            string[] options = {
                "Förnamn (A-Ö)", "Förnamn (Ö-A)",
                "Efternamn (A-Ö)", "Efternamn (Ö-A)", "Studentmeny" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();
            ViewingInfo viewingInfo = new ViewingInfo();

            switch (menuSelect)
            {
                case 0:
                    Console.Clear();
                    viewingInfo.ViewStudentsSortFNameAsc();
                    PromptAfterList();
                    PromptSort();
                    break;
                case 1:
                    Console.Clear();
                    viewingInfo.ViewStudentsSortFNameDesc();
                    PromptAfterList();
                    PromptSort();
                    break;
                case 2:
                    Console.Clear();
                    viewingInfo.ViewStudentsSortLNameAsc();
                    PromptAfterList();
                    PromptSort();
                    break;
                case 3:
                    Console.Clear();
                    viewingInfo.ViewStudentsSortLNameDesc();
                    PromptAfterList();
                    PromptSort();
                    break;
                case 4:
                    StudentsMenu();
                    break;
            }
        }
        internal void PromptSort()
        {
            string prompt = $"Vill du se andra sorteringar?\n(Väljer du Nej skickas du tillbaka till studentmenyn.)\n";
            string[] options = { "Ja", "Nej" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();

            switch (menuSelect)
            {
                case 0:
                    SortStudentsName();
                    break;
                case 1:
                    StudentsMenu();
                    break;
            }
        }
        internal void PromptClass()
        {
            string prompt = $"Vill du se andra klasser?\n(Väljer du Nej skickas du tillbaka till huvudmenyn.)\n";
            string[] options = { "Ja", "Nej" };
            Menu menu = new Menu(prompt, options);
            int menuSelect = menu.Run();
            ViewingInfo viewingInfo = new ViewingInfo();

            switch (menuSelect)
            {
                case 0:
                    viewingInfo.ViewClasses();
                    break;
                case 1:
                    Start();
                    break;
            }
        }
        internal void PromptAfterList()
        {
            Console.WriteLine("\n-----------------------------------------------------------------------");
            Console.WriteLine("Tryck Enter för att komma vidare.");
            Console.ReadKey();
        }
        internal void Exit()
        {
            Console.Clear();
            Console.WriteLine("Du har valt att avsluta \nAppen stängs.");
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}
