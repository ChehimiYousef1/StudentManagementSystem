using StudentManagementSystem.Models;
using StudentManagementSystem_Console.Data;
using StudentManagementSystem.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StudentManagementSystem
{
    internal class Program
    {
        //statically  implementation from interface IStudentRepository
        private static  IStudentRepository repository = new IStudentRepositorys.StudentRepositorys();
        //lets get to be inside the main method of our consols applications
        static void Main(String[] args)
        {
            //define a boolean variable to exit the application
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to Student Mangement Systems Consols Application");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Edit Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. View All Students");
                Console.WriteLine("5. Search Students");
                Console.WriteLine("6. Get Students By Name");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                //define a choice variable to capture user input
                string choice = Console.ReadLine();

                //get a swich to swich between the options 
                //each options should takes a function to get executes it
                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": EditStudent(); break;
                    case "3": DeleteStudent(); break;
                    case "4": ViewAllStudents(); break;
                    case "5": SearchStudents(); break;
                    case "6": GetStudentsByName(); break;
                    case "0": exit = true; break;
                    default: Console.WriteLine("Invalid choice! Press any key..."); Console.ReadKey(); break;
                }
            }
        }
        private static void AddStudent()
        {
            Console.Clear();
            Console.WriteLine("=== Add New Student ===");
            Console.Write("First Name: "); string firstName = Console.ReadLine();
            Console.Write("Last Name: "); string lastName = Console.ReadLine();
            Console.Write("Date of Birth (yyyy-MM-dd): "); DateTime dob = DateTime.Parse(Console.ReadLine());

            repository.Add(new Student { FirstName = firstName, LastName = lastName, DateOfBirth = dob });
            Console.WriteLine("Student added successfully!");
            Console.ReadKey();
        }
        private static void EditStudent()
        {
            Console.Clear();
            Console.Write("Enter Student ID to edit: ");
            int id = int.Parse(Console.ReadLine());
            var student = repository.GetStudentById(id);
            if (student == null) { Console.WriteLine("Not found!"); Console.ReadKey(); return; }

            Console.Write($"First Name ({student.FirstName}): "); string fn = Console.ReadLine();
            Console.Write($"Last Name ({student.LastName}): "); string ln = Console.ReadLine();
            Console.Write($"DOB ({student.DateOfBirth:yyyy-MM-dd}): "); string dobInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(fn)) student.FirstName = fn;
            if (!string.IsNullOrWhiteSpace(ln)) student.LastName = ln;
            if (!string.IsNullOrWhiteSpace(dobInput)) student.DateOfBirth = DateTime.Parse(dobInput);

            repository.Edit(student);
            Console.WriteLine("Updated successfully!");
            Console.ReadKey();
        }
        private static void DeleteStudent()
        {
            Console.Clear();
            Console.Write("Enter Student ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            repository.Delete(id);
            Console.WriteLine("Deleted (if existed)!");
            Console.ReadKey();
        }
        private static void ViewAllStudents()
        {
            Console.Clear();
            Console.WriteLine("=== All Students ===");
            foreach (var s in repository.GetAllStudents()) Console.WriteLine(s);
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        private static void SearchStudents()
        {
            Console.Clear();
            Console.Write("Enter first name or last name to search: ");
            string term = Console.ReadLine();

            var students = repository.Search(term); // Search by first or last name

            if (students.Count == 0)
            {
                Console.WriteLine("No students found with this name.");
            }
            else
            {
                Console.WriteLine("\n--- Students Found ---");
                foreach (var s in students)
                {
                    // Display all information
                    Console.WriteLine($"ID: {s.Id}");
                    Console.WriteLine($"Name: {s.FirstName} {s.LastName}");
                    Console.WriteLine($"Date of Birth: {s.DateOfBirth.ToShortDateString()}");
                    Console.WriteLine($"Age: {s.Age}");
                    Console.WriteLine("-------------------------");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        private static void GetStudentsByName()
        {
            Console.Clear();
            Console.Write("First Name: "); string fn = Console.ReadLine();
            Console.Write("Last Name: "); string ln = Console.ReadLine();
            foreach (var s in repository.GetStudentByName(fn, ln)) Console.WriteLine(s);
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

    }
}