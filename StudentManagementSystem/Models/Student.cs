using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name must contain only letters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name must contain only letters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Student), nameof(ValidateDOB))]
        public DateTime DateOfBirth { get; set; }

        //Computed property to get the age of the student.
        public int Age
        {
            get
            {
                // Calculate age based on DateOfBirth
                //first get today's date
                var today = DateTime.Today;
                //then calculate the age
                //subtract the year of birth from the current year
                var age = today.Year - DateOfBirth.Year;
                //if the birthday hasn't occurred yet this year, subtract one from the age
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
        // Override ToString method to display student information on the console
        public override string ToString() => $"ID: {Id}, Name: {FirstName} {LastName}, Age: {Age}";

        // Custom validation method for Date of Birth
        public static ValidationResult ValidateDOB(DateTime dob, ValidationContext context)
        {
            if (dob >= DateTime.Today)
                return new ValidationResult("Date of Birth must be in the past");
            return ValidationResult.Success;
        }

    }
}
