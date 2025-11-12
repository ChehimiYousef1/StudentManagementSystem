using StudentManagementSystem.Models;
using System.Collections.Generic;

namespace StudentManagementSystem.Interfaces
{
    internal interface IStudentRepository
    {
        // For adding Student to the database
        void Add(Student student);

        // Update a student in the database
        void Edit(Student student);

        // Delete a student from the database
        void Delete(int studentId);

        // Retrieve students by first name or last name
        List<Student> GetStudentByName(string firstName, string lastName);

        // Search for students in the database
        // The user can search by student ID, first name, or last name
        List<Student> Search(string searchTerm);

        // View all students
        List<Student> GetAllStudents();

        // Optional: get student by ID
        Student GetStudentById(int id);
    }
}
