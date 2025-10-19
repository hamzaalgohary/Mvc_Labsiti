using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using lab1mvc.Validations.LessThanAttribute; // make sure this is here

namespace lab1mvc.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Remote(action: "CheckNameUnique", controller: "Course", AdditionalFields = "Id", ErrorMessage = "Course name already exists!")]

        public string? Name { get; set; }
        public string? Topic { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Degree must be between 0 and 100")]

        public int Degree { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Min degree must be between 0 and 100")]
        [LessThan("Degree", ErrorMessage = "Min degree must be less than Degree")]


        public int MinDegree { get; set; }
        [Required(ErrorMessage = "Please select a department")]

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
    }
}



// form lab1 to lab4
//    public class Course
//    {
//        public int Id { get; set; }
//        public string? Name { get; set; }
//        public string? Topic { get; set; }
//        public int Degree { get; set; }
//        public int MinDegree { get; set; }
//        public int? DepartmentId { get; set; }
//        public Department? Department { get; set; }
//        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
//        public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
//    }
//}
