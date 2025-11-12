using StudentManagementSystem.Models;
using StudentManagementSystem_Console.Data;
using StudentManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementSystem.IStudentRepositorys
{
    // Implement the IStudentRepository interface
    public class StudentRepositorys : IStudentRepository
    {
        public StudentRepositorys() { }

        public void Add(Student student)
        {
            using var context = new StudentDbContext();
            context.Students.Add(student);
            context.SaveChanges();
        }

        public void Edit(Student student)
        {
            using var context = new StudentDbContext();
            context.Students.Update(student);
            context.SaveChanges();
        }

        public void Delete(int studentId)
        {
            using var context = new StudentDbContext();
            var student = context.Students.Find(studentId);
            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
        }

        public List<Student> GetStudentByName(string firstName, string lastName)
        {
            using var context = new StudentDbContext();
            return context.Students
                          .Where(s => s.FirstName == firstName || s.LastName == lastName)
                          .ToList();
        }

        public List<Student> Search(string searchTerm)
        {
            using var context = new StudentDbContext();
            return context.Students
                          .Where(s => s.FirstName.Contains(searchTerm) || s.LastName.Contains(searchTerm))
                          .ToList();
        }

        public List<Student> GetAllStudents()
        {
            using var context = new StudentDbContext();
            return context.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            using var context = new StudentDbContext();
            return context.Students.Find(id);
        }
    }
}
