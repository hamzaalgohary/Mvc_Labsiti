using lab1mvc.Models;

namespace lab1mvc.Models
{
    public class CourseInstructor
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public decimal? RateHour { get; set; }
    }
}
