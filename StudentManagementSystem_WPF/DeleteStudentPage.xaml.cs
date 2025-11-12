using StudentManagementSystem.Models;
using StudentManagementSystem_Console.Data; // Your DbContext namespace
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StudentManagementSystem_WPF
{
    public partial class DeleteStudentPage : Page
    {
        public DeleteStudentPage()
        {
            InitializeComponent();
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            string idText = IdBox.Text.Trim();
            string firstName = FirstNameBox.Text.Trim();
            string lastName = LastNameBox.Text.Trim();

            using (var context = new StudentDbContext())
            {
                Student studentToDelete = null;

                if (!string.IsNullOrEmpty(idText) && int.TryParse(idText, out int id))
                {
                    studentToDelete = context.Students.FirstOrDefault(s => s.Id == id);
                }
                else if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                {
                    studentToDelete = context.Students.FirstOrDefault(s =>
                        s.FirstName.ToLower() == firstName.ToLower() &&
                        s.LastName.ToLower() == lastName.ToLower());
                }

                if (studentToDelete != null)
                {
                    context.Students.Remove(studentToDelete);
                    context.SaveChanges(); // Commit deletion to database

                    ResultText.Text = $"Student deleted: {studentToDelete.FirstName} {studentToDelete.LastName}";

                    // Optionally, clear the input boxes
                    IdBox.Clear();
                    FirstNameBox.Clear();
                    LastNameBox.Clear();
                }
                else
                {
                    ResultText.Text = "Student not found!";
                }
            }
        }
    }
}
