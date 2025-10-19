using Humanizer;
using lab1mvc.context;
using lab1mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace lab1mvc.Controllers
{
    public class CourseController : Controller
    {
        private readonly dblab1 _context = new dblab1();
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckNameUnique(string name, int id = 0)
        {
            bool exists = _context.Courses
                .Any(c => c.Name.ToLower() == name.ToLower() && c.Id != id);

            if (exists)
                return Json($"Course name '{name}' already exists!");

            return Json(true);
        }
        public IActionResult GetAll()
        {
            var courses = _context.Courses
                .Include(c => c.Department)
                .ToList();

            return View(courses);
        }

        public IActionResult GetById(int id)
        {
            var course = _context.Courses
                .Include(c => c.Department)
                .FirstOrDefault(c => c.Id == id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        public IActionResult GetByName(string name)
        {
            var courses = _context.Courses
                .Include(c => c.Department)
                .Where(c => c.Name.Contains(name))
                .ToList();

            return View("GetAll", courses);
        }

        public IActionResult Add()
        {
            ViewBag.Departments = new SelectList(_context.Departments.ToList(), "Id", "Name");
            return View(new Course());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Course course)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_context.Departments.ToList(), "Id", "Name", course.DepartmentId);
                return View(course);
            }

            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null)
                return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments.ToList(), "Id", "Name", course.DepartmentId);
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_context.Departments.ToList(), "Id", "Name", course.DepartmentId);
                return View(course);
            }

            _context.Courses.Update(course);
            _context.SaveChanges();
            return RedirectToAction(nameof(GetAll));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
                return Json(new { success = false, message = "Course not found." });

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return Json(new { success = true, message = "✅ Course deleted successfully." });
        }
        public IActionResult ThrowError()
        {
            throw new Exception("💥 Test exception from controller!");
        }
    }
}























//To lab 4
//       dblab1 _context = new dblab1();

//        public IActionResult GetAll()
//        {
//            var courses = _context.Courses
//                .Include(c => c.Department).ToList();


//            return View(courses);
//        }

//        public IActionResult GetById(int id)
//        {
//            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
//            if (course == null)
//                return NotFound();

//            return View(course);
//        }

//        public IActionResult GetByName(string name)
//        {
//            var courses = _context.Courses
//                .Where(c => c.Name.Contains(name))
//                .ToList();

//            return View("GetAll", courses);
//        }
//        public IActionResult Add()
//        {
//            ViewBag.Departments = _context.Departments.ToList();
//            return View();
//        }
//        [HttpPost]
//        public IActionResult Add(Course course)
//        {
//            _context.Courses.Add(course);
//            _context.SaveChanges();
//            return RedirectToAction("GetAll");
//        }
//        public IActionResult Edit(int id)
//        {
//            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
//            if (course == null)
//                return NotFound();
//            ViewBag.Departments = _context.Departments.ToList();

//            return View(course);
//        }

//        [HttpPost]
//        public IActionResult Edit(Course course)
//        {
//            _context.Courses.Update(course);
//            _context.SaveChanges();
//            return RedirectToAction("GetAll");
//        }

//        public IActionResult Delete(int id)
//        {
//            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
//            if (course == null)
//                return NotFound();

//            _context.Courses.Remove(course);
//            _context.SaveChanges();
//            return RedirectToAction("GetAll");
//        }


//    }
//}
