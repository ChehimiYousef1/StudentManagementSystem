using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem_Console.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StudentManagementSystem_WPF
{
    public partial class EditStudentPage : Page
    {
        private List<Student> _students = new List<Student>();

        public EditStudentPage()
        {
            InitializeComponent();
            LoadStudentsAsync();
        }

        private async void LoadStudentsAsync()
        {
            using (var context = new StudentDbContext())
            {
                _students = await context.Students.ToListAsync();
            }

            // Display all students initially
            SearchResultsList.ItemsSource = _students;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Optional: live search while typing
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                // Show all students if empty
                SearchResultsList.ItemsSource = _students;
                return;
            }

            var results = _students
                .Where(s => s.Id.ToString().Contains(query) ||
                            s.FirstName.ToLower().Contains(query.ToLower()) ||
                            s.LastName.ToLower().Contains(query.ToLower()))
                .ToList();

            SearchResultsList.ItemsSource = results;

            if (!results.Any())
            {
                MessageBox.Show("No student found with the given information.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditStudent_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int studentId)
            {
                // Navigate to the detailed edit page
                NavigationService.Navigate(new EditStudentDetailsPage(studentId));
            }
        }
    }
}
