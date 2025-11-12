using StudentManagementSystem.Models;
using StudentManagementSystem_Console.Data; // Your DbContext namespace
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StudentManagementSystem_WPF
{
    public partial class EditStudentDetailsPage : Page
    {
        private readonly int _studentId;
        private Student _student;

        public EditStudentDetailsPage(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            using (var context = new StudentDbContext())
            {
                _student = context.Students.FirstOrDefault(s => s.Id == _studentId);
            }

            if (_student != null)
            {
                FirstNameBox.Text = _student.FirstName;
                LastNameBox.Text = _student.LastName;
                DOBPicker.SelectedDate = _student.DateOfBirth;
            }
            else
            {
                MessageBox.Show("Student not found!");
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (_student == null) return;

            _student.FirstName = FirstNameBox.Text.Trim();
            _student.LastName = LastNameBox.Text.Trim();
            if (DOBPicker.SelectedDate.HasValue)
                _student.DateOfBirth = DOBPicker.SelectedDate.Value;

            using (var context = new StudentDbContext())
            {
                context.Students.Update(_student);
                context.SaveChanges();
            }

            MessageBox.Show("Student updated successfully!");

            // Navigate back to EditStudentPage
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
