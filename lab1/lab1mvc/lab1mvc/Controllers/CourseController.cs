using lab1mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using lab1mvc.context;
using lab1mvc.Models;

namespace lab1mvc.Controllers
{
    public class CourseController : Controller
    {
       dblab1 _context = new dblab1();

        public IActionResult GetAll()
        {
            var courses = _context.Courses
                .Include(c => c.Department).ToList();


            return View(courses);
        }

        public IActionResult GetById(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        public IActionResult GetByName(string name)
        {
            var courses = _context.Courses
                .Where(c => c.Name.Contains(name))
                .ToList();

            return View("GetAll", courses);
        }
        public IActionResult Add()
        {
            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("GetAll");
        }
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();
            ViewBag.Departments = _context.Departments.ToList();

            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
            return RedirectToAction("GetAll");
        }

        public IActionResult Delete(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("GetAll");
        }


    }
}
