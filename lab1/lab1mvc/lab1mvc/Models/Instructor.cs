using System.ComponentModel.DataAnnotations;

namespace lab1mvc.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }
        [Range(18, 70, ErrorMessage = "Age must be between 18 and 70.")]

        public int Age { get; set; }
        [Range(1000, 100000, ErrorMessage = "Salary must be between 1000 and 50000.")]

        public decimal Salary { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "You must select a Department.")]

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();

    }
}

























//lab 1 to lab4
//        public int Id { get; set; }
//        public string? Name { get; set; }
//        public int Age { get; set; }
//        public decimal Salary { get; set; }
//        public string? Image { get; set; }
//        public string? Address { get; set; }

//        public int? DepartmentId { get; set; }
//        public Department? Department { get; set; }
//        public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();

//    }
//}
