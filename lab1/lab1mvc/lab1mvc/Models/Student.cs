namespace lab1mvc.Models
{
    //class declartion
    public class Student
    {
        //properties datamembers

        public int SSN { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }

        // make override to display in the string
        public override string ToString()
        {
            return $"SSN: {SSN}, Name: {Name}, Age: {Age}, Image: {Image}, Address: {Address}, Email: {Email}";
        }
    }
}

    
