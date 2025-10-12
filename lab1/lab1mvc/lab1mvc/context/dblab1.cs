using lab1mvc.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace lab1mvc.context
{
    //class declartion  and inheritance
    public class dblab1 : DbContext
    {
        //DbSet Property
        public DbSet<Student> Students { get; set; }


        ///Database Configuration to connect database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=GOHARY\\SQLEXPRESS;Database=lab1mvc;Trusted_Connection=True; TrustServerCertificate=True");
        }

        //Model Configuration (Fluent API)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(s => s.SSN);
            modelBuilder.Entity<Student>().Property(s => s.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Student>().Property(s => s.Age).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.Address).HasMaxLength(200);
            modelBuilder.Entity<Student>().Property(s => s.Image).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.Email).HasMaxLength(100);

            //Seed Data (Data Seeding

            modelBuilder.Entity<Student>().HasData(
                //here my data input in sql
                new Student
                {
                    SSN = 1,
                    Name = "Hamza Khaled",
                    Age = 20,
                    Image = "hamza.jpg",
                    Address = "giza,egypt",
                    Email = "hamza@yahoo.com"
                },
                new Student
                {
                    SSN = 2,
                    Name = "Gohary",
                    Age = 22,
                    Image = "Gohary.jpg",
                    Address = "fayuom",
                    Email = "Gohary@yahoo.com"
                }
            );
        }
    }
}
