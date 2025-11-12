using StudentManagementSystem.Models;
using StudentManagementSystem_Console.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem_WPF
{
    public partial class SearchStudentPage : Page
    {
        public SearchStudentPage()
        {
            InitializeComponent();
        }

        private async void SearchStudent_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Please enter a Student ID or Name to search.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                using (var context = new StudentDbContext())
                {
                    var results = await context.Students
                        .Where(s => s.Id.ToString().Contains(query) ||
                                    (s.FirstName + " " + s.LastName).Contains(query) ||
                                    s.FirstName.Contains(query) ||
                                    s.LastName.Contains(query))
                        .ToListAsync();

                    ResultsDataGrid.ItemsSource = results;

                    if (!results.Any())
                    {
                        MessageBox.Show("No student found with the given information.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching students: " + ex.Message);
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Optional: implement live search here
        }
    }
}
