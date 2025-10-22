using lab1mvc.Models;
using System.ComponentModel.DataAnnotations;

namespace lab1mvc.Models
{
    public enum Branch
    {
        Cairo,
        Alexandria,
        Giza,
        fayoum,
        Aswan,
        Tanta
    }
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Department Name must be between 3 and 50 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Manager Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Manager Name must be between 3 and 50 characters")]

        public string? Manager { get; set; }
        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]

        public string? Location { get; set; }
        [Required(ErrorMessage = "Please select a Branch")]
        [EnumDataType(typeof(Branch), ErrorMessage = "Invalid branch selected")]
        public Branch Branch { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}













//from lab 1 to lab 4
//    public class Department
//    {
//        public int Id { get; set; }
//        public string? Name { get; set; }
//        public string? Manager { get; set; }
//        public string? Location { get; set; }
//        public Branch Branch { get; set; }
//        public ICollection<Student> Students { get; set; } = new List<Student>();

//        public ICollection<Course> Courses { get; set; } = new List<Course>();
//        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
//    }
//}
