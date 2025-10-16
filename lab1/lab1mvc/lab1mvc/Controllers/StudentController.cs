using lab1mvc.context;
using lab1mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace lab1mvc.Controllers
{
    public class StudentController : Controller
    {
        dblab1 _context = new dblab1();

        public IActionResult getall()
        {
            var date = _context.Students.ToList();
            return View(date);

        }
        public IActionResult getbyID(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);
            if (student == null)
            {
                return NotFound();
            }
            ContentResult result = new ContentResult();
            result.Content = " hi from another workd we are in hell there ";
            return View(student);
        }

        public IActionResult Add()
        {
            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "Name");

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Student student, int[] selectedCourses)
        {
            if (student.Name != null)
            {
                _context.Students.Add(student);
                _context.SaveChanges();

                foreach (var courseId in selectedCourses)
                {
                    var sc = new StudentCourse
                    {
                        StudentSSN = student.SSN,
                        CourseId = courseId
                    };
                    _context.StudentCourses.Add(sc);
                }

                _context.SaveChanges();
                return RedirectToAction("GetAll");
            }

            ViewBag.Departments = new SelectList(_context.Departments.ToList(), "Id", "Name");
            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "Name");
            return View(student);
        }

        public IActionResult Edit(int id)
        {
            var student = _context.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefault(s => s.SSN == id);

            if (student == null)
                return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", student.DepartmentId);
            ViewBag.Courses = new MultiSelectList(
                _context.Courses.ToList(),
                "Id",
                "Name",
                student.StudentCourses.Select(sc => sc.CourseId)
            );

            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student, int[] selectedCourses)
        {
            if (student == null)
                return NotFound();

            _context.Update(student);
            _context.SaveChanges();

            var oldRelations = _context.StudentCourses
                .Where(sc => sc.StudentSSN == student.SSN)
                .ToList();

            _context.StudentCourses.RemoveRange(oldRelations);
            _context.SaveChanges();

            foreach (var courseId in selectedCourses)
            {
                var newRelation = new StudentCourse
                {
                    StudentSSN = student.SSN,
                    CourseId = courseId
                };
                _context.StudentCourses.Add(newRelation);
            }

            _context.SaveChanges();

            return RedirectToAction("GetAll");
        }

        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);


            _context.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("GetAll");
        }
        public IActionResult Details(int id)
        {
            var student = _context.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.SSN == id);

            if (student == null)
                return NotFound();

            return View(student);
        }


        [HttpPost]
        public IActionResult UpdateGrade(int studentSSN, int courseId, double grade)
        {
            var record = _context.StudentCourses
                .FirstOrDefault(sc => sc.StudentSSN == studentSSN && sc.CourseId == courseId);

            if (record == null)
                return NotFound();

            record.Grade = grade;
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = studentSSN });
        }
    }
}
