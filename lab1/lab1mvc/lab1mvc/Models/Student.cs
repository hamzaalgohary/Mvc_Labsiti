using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace lab1mvc.Models
{
    public class Student
    {
        public int SSN { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int Age { get; set; }


        public string? Image { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Remote(action: "CheckEmail", controller: "Student", AdditionalFields = "SSN", ErrorMessage = "Email already exists")]
        public string? Email { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "You must select a department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }  // <== this one is required

        public ICollection<StudentCourse>? StudentCourses { get; set; }

        public override string ToString()
        {
            return $"SSN: {SSN}, Name: {Name}, Age: {Age}, Address: {Address}, Email: {Email}";
        }
    }
}





























//lab1 to lab4
//        public int SSN { get; set; }
//        public string? Name { get; set; }
//        public int Age { get; set; }
//        public string? Image { get; set; }
//        public string? Address { get; set; }
//        public string? Email { get; set; }
//        public int? DepartmentId { get; set; }       
//        public Department? Department { get; set; }
//        public ICollection<StudentCourse>? StudentCourses { get; set; }

//        public override string ToString()
//        {
//            return $"SSN: {SSN}, Name: {Name}, Age: {Age}, Address: {Address}, Email: {Email}";
//        }
//    }
//}