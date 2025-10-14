using lab1mvc.Models;

namespace lab1mvc.Models
{
    public class StudentCourse
    {
        public int StudentSSN { get; set; }
        public int CourseId { get; set; }

        public double? Grade { get; set; }

        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}

