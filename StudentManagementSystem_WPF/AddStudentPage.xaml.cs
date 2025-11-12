using System;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using StudentManagementSystem.Models;
using StudentManagementSystem.Interfaces;
using StudentManagementSystem.IStudentRepositorys;  // IStudentRepository

namespace StudentManagementSystem_WPF
{
    public partial class AddStudentPage : Page
    {
        private readonly StudentRepositorys _repository;

        public AddStudentPage()
        {
            InitializeComponent();

            // Initialize repository
            _repository = new StudentRepositorys(); // Your EF repository
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                    !DateOfBirthPicker.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newStudent = new Student
                {
                    FirstName = FirstNameTextBox.Text.Trim(),
                    LastName = LastNameTextBox.Text.Trim(),
                    DateOfBirth = DateOfBirthPicker.SelectedDate.Value
                };

                _repository.Add(newStudent); // <-- use Add() as defined in IStudentRepository

                MessageBox.Show("Student added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                FirstNameTextBox.Clear();
                LastNameTextBox.Clear();
                DateOfBirthPicker.SelectedDate = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
