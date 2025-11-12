using StudentManagementSystem.Models;
using StudentManagementSystem_Console.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem_WPF
{
    public partial class View_All_StudentPage : Page
    {
        private List<Student> _students;

        public View_All_StudentPage()
        {
            InitializeComponent();
            LoadStudentsAsync();
        }

        private async void LoadStudentsAsync()
        {
            try
            {
                using var context = new StudentDbContext();
                _students = await context.Students.ToListAsync();

                // Bind directly to DataGrid
                StudentsDataGrid.ItemsSource = _students;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }

        private void ViewStudent_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Student student)
            {
                int age = DateTime.Now.Year - student.DateOfBirth.Year;
                string fullName = $"{student.FirstName} {student.LastName}";

                // Show card window
                var cardWindow = new Window
                {
                    Title = $"Student: {fullName}",
                    Width = 300,
                    Height = 220,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = new StackPanel
                    {
                        Margin = new Thickness(20),
                        Children =
                        {
                            new TextBlock { Text = $"ID: {student.Id}", FontWeight = FontWeights.Bold, Margin = new Thickness(0,0,0,10) },
                            new TextBlock { Text = $"Full Name: {fullName}", FontWeight = FontWeights.Bold, Margin = new Thickness(0,0,0,10) },
                            new TextBlock { Text = $"Date of Birth: {student.DateOfBirth:d}", FontWeight = FontWeights.Bold, Margin = new Thickness(0,0,0,10) },
                            new TextBlock { Text = $"Age: {age}", FontWeight = FontWeights.Bold }
                        }
                    }
                };

                cardWindow.ShowDialog();
            }
        }
    }
}
