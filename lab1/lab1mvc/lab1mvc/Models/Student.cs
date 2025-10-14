namespace lab1mvc.Models
{
    public class Student
    {
        public int SSN { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int? DepartmentId { get; set; }       
        public Department? Department { get; set; }
        public ICollection<StudentCourse>? StudentCourses { get; set; }

        public override string ToString()
        {
            return $"SSN: {SSN}, Name: {Name}, Age: {Age}, Address: {Address}, Email: {Email}";
        }
    }



}