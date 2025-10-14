using lab1mvc.Models;

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
        public string? Name { get; set; }
        public string? Manager { get; set; }
        public string? Location { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
