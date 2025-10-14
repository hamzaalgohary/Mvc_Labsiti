using lab1mvc.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;



namespace lab1mvc.context
{
    //class declartion  and inheritance
    public class dblab1 : DbContext
    {
        //DbSet Property
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        //Database Configuration to connect database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=GOHARY\\SQLEXPRESS;Database=lab1mvc;Trusted_Connection=True; TrustServerCertificate=True");
        }
        //apis
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Student>().HasKey(s => s.SSN);
                modelBuilder.Entity<Student>().Property(s => s.Name).IsRequired().HasMaxLength(100);
                modelBuilder.Entity<Student>().Property(s => s.Age).IsRequired();
                modelBuilder.Entity<Student>().Property(s => s.Address).HasMaxLength(200);
                modelBuilder.Entity<Student>().Property(s => s.Image).IsRequired();
                modelBuilder.Entity<Student>().Property(s => s.Email).HasMaxLength(100);

                modelBuilder.Entity<Student>().HasOne(s => s.Department)



             .WithMany(d => d.Students).HasForeignKey(s => s.DepartmentId).OnDelete(DeleteBehavior.SetNull);
                modelBuilder.Entity<Department>().HasKey(d => d.Id);

                modelBuilder.Entity<Department>()
                    .Property(d => d.Name).IsRequired().HasMaxLength(100);

                modelBuilder.Entity<Department>()
                    .Property(d => d.Manager).HasMaxLength(100);


                modelBuilder.Entity<Department>()
                    .Property(d => d.Location).HasMaxLength(100);



                modelBuilder.Entity<Course>().HasKey(c => c.Id);

                modelBuilder.Entity<Course>()
                    .Property(c => c.Name).IsRequired().HasMaxLength(100);

                modelBuilder.Entity<Course>().Property(c => c.Topic).HasMaxLength(100);

                modelBuilder.Entity<Course>()
         .HasOne(c => c.Department)
         .WithMany(d => d.Courses)
         .HasForeignKey(c => c.DepartmentId)
         .OnDelete(DeleteBehavior.SetNull);


                modelBuilder.Entity<Instructor>().HasKey(i => i.Id);

                modelBuilder.Entity<Instructor>()
                    .Property(i => i.Name).IsRequired().HasMaxLength(100);



                modelBuilder.Entity<Instructor>().Property(i => i.Address).HasMaxLength(200);


                modelBuilder.Entity<Instructor>().HasOne(i => i.Department).WithMany(d => d.Instructors)
                    .HasForeignKey(i => i.DepartmentId).OnDelete(DeleteBehavior.SetNull);

                modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentSSN, sc.CourseId });


                modelBuilder.Entity<StudentCourse>()
                    .HasOne(sc => sc.Student).WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.StudentSSN);


                modelBuilder.Entity<StudentCourse>().HasOne(sc => sc.Course).WithMany(c => c.StudentCourses)
                    .HasForeignKey(sc => sc.CourseId);


                modelBuilder.Entity<CourseInstructor>()
          .HasKey(ci => new { ci.CourseId, ci.InstructorId });

                modelBuilder.Entity<CourseInstructor>()
                    .HasOne(ci => ci.Course)
                    .WithMany(c => c.CourseInstructors)
                    .HasForeignKey(ci => ci.CourseId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<CourseInstructor>()
                    .HasOne(ci => ci.Instructor)
                    .WithMany(i => i.CourseInstructors)
                    .HasForeignKey(ci => ci.InstructorId)
                    .OnDelete(DeleteBehavior.NoAction);




            }
        }
    }

//lab1
//Seed Data (Data Seeding
//lab1
//modelBuilder.Entity<Student>().HasData(
//    //here my data input in sql
//    new Student
//    {
//        SSN = 1,
//        Name = "Hamza Khaled",
//        Age = 20,
//        Image = "hamza.jpg",
//        Address = "giza,egypt",
//        Email = "hamza@yahoo.com"
//    },
//    new Student
//    {
//        SSN = 2,
//        Name = "Gohary",
//        Age = 22,
//        Image = "Gohary.jpg",
//        Address = "fayuom",
//        Email = "Gohary@yahoo.com"
//    }
