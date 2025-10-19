using lab1mvc.context;
using lab1mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;


namespace lab1mvc.Controllers
{
    public class StudentController : Controller
    {
        dblab1 _context = new dblab1();
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckEmail(string email, int ssn)
        {
            bool emailExists = _context.Students.Any(s => s.Email == email && s.SSN != ssn);

            if (emailExists)
            {
                return Json($"Email '{email}' is already in use.");
            }

            return Json(true);
        }
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
        public IActionResult Add(Student student, List<int>? selectedCourses)
        {
            bool emailExists = _context.Students.Any(s => s.Email == student.Email);
            if (emailExists)
            {
                ModelState.AddModelError("Email", "This email is already used by another student.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_context.Departments.ToList(), "Id", "Name", student.DepartmentId);
                ViewBag.Courses = new MultiSelectList(_context.Courses.ToList(), "Id", "Name", selectedCourses);
                return View(student);
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            if (selectedCourses != null && selectedCourses.Any())
            {
                var studentCourses = selectedCourses.Select(cid => new StudentCourse
                {
                    StudentSSN = student.SSN,
                    CourseId = cid
                }).ToList();

                _context.StudentCourses.AddRange(studentCourses);
                _context.SaveChanges();
            }

            return RedirectToAction("GetAll");
        }


        public IActionResult Edit(int id)
        {
            var student = _context.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefault(s => s.SSN == id);

            if (student == null)
                return NotFound();

            ViewBag.Departments = new SelectList(
                _context.Departments.ToList(),
                "Id",
                "Name",
                student.DepartmentId
            );

            ViewBag.Courses = new MultiSelectList(
                _context.Courses.ToList(),
                "Id",
                "Name",
                student.StudentCourses?.Select(sc => sc.CourseId)
            );

            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student, int[]? selectedCourses)
        {
            var existingStudent = _context.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefault(s => s.SSN == student.SSN);

            if (existingStudent == null)
                return NotFound();

            bool emailExists = _context.Students
                .Any(s => s.Email.ToLower() == student.Email.ToLower() && s.SSN != student.SSN);

            if (emailExists)
            {
                ModelState.AddModelError("Email", $"Email '{student.Email}' is already in use.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_context.Departments.ToList(), "Id", "Name", student.DepartmentId);
                ViewBag.Courses = new MultiSelectList(_context.Courses.ToList(), "Id", "Name", selectedCourses);
                return View(student);
            }

            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Email = student.Email;
            existingStudent.Image = student.Image;
            existingStudent.Address = student.Address;
            existingStudent.DepartmentId = student.DepartmentId;

            existingStudent.StudentCourses.Clear();
            if (selectedCourses != null && selectedCourses.Any())
            {
                foreach (var courseId in selectedCourses)
                {
                    existingStudent.StudentCourses.Add(new StudentCourse
                    {
                        StudentSSN = existingStudent.SSN,
                        CourseId = courseId
                    });
                }
            }

            _context.SaveChanges();

            return RedirectToAction("GetAll");
        }



        [HttpPost]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);

            if (student == null)
                return Json(new { success = false, message = "Student not found." });

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Json(new { success = true, message = "✅ Student deleted successfully." });
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






















// to lab 4
//        dblab1 _context = new dblab1();

//        public IActionResult getall()
//        {
//            var date = _context.Students.ToList();
//            return View(date);

//        }
//        public IActionResult getbyID(int id)
//        {
//            var student = _context.Students.FirstOrDefault(s => s.SSN == id);
//            if (student == null)
//            {
//                return NotFound();
//            }
//            ContentResult result = new ContentResult();
//            result.Content = " hi from another workd we are in hell there ";
//            return View(student);
//        }

//        public IActionResult Add()
//        {
//            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "Name");

//            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Add(Student student, int[] selectedCourses)
//        {
//            if (student.Name != null)
//            {
//                _context.Students.Add(student);
//                _context.SaveChanges();

//                foreach (var courseId in selectedCourses)
//                {
//                    var sc = new StudentCourse
//                    {
//                        StudentSSN = student.SSN,
//                        CourseId = courseId
//                    };
//                    _context.StudentCourses.Add(sc);
//                }

//                _context.SaveChanges();
//                return RedirectToAction("GetAll");
//            }

//            ViewBag.Departments = new SelectList(_context.Departments.ToList(), "Id", "Name");
//            ViewBag.Courses = new SelectList(_context.Courses.ToList(), "Id", "Name");
//            return View(student);
//        }

//        public IActionResult Edit(int id)
//        {
//            var student = _context.Students
//                .Include(s => s.StudentCourses)
//                .FirstOrDefault(s => s.SSN == id);

//            if (student == null)
//                return NotFound();

//            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", student.DepartmentId);
//            ViewBag.Courses = new MultiSelectList(
//                _context.Courses.ToList(),
//                "Id",
//                "Name",
//                student.StudentCourses.Select(sc => sc.CourseId)
//            );

//            return View(student);
//        }
//        [HttpPost]
//        public IActionResult Edit(Student student, int[] selectedCourses)
//        {
//            if (student == null)
//                return NotFound();

//            _context.Update(student);
//            _context.SaveChanges();

//            var oldRelations = _context.StudentCourses
//                .Where(sc => sc.StudentSSN == student.SSN)
//                .ToList();

//            _context.StudentCourses.RemoveRange(oldRelations);
//            _context.SaveChanges();

//            foreach (var courseId in selectedCourses)
//            {
//                var newRelation = new StudentCourse
//                {
//                    StudentSSN = student.SSN,
//                    CourseId = courseId
//                };
//                _context.StudentCourses.Add(newRelation);
//            }

//            _context.SaveChanges();

//            return RedirectToAction("GetAll");
//        }

//        public IActionResult Delete(int id)
//        {
//            var student = _context.Students.FirstOrDefault(s => s.SSN == id);


//            _context.Students.Remove(student);
//            _context.SaveChanges();

//            return RedirectToAction("GetAll");
//        }
//        public IActionResult Details(int id)
//        {
//            var student = _context.Students
//                .Include(s => s.StudentCourses)
//                .ThenInclude(sc => sc.Course)
//                .FirstOrDefault(s => s.SSN == id);

//            if (student == null)
//                return NotFound();

//            return View(student);
//        }


//        [HttpPost]
//        public IActionResult UpdateGrade(int studentSSN, int courseId, double grade)
//        {
//            var record = _context.StudentCourses
//                .FirstOrDefault(sc => sc.StudentSSN == studentSSN && sc.CourseId == courseId);

//            if (record == null)
//                return NotFound();

//            record.Grade = grade;
//            _context.SaveChanges();

//            return RedirectToAction("Details", new { id = studentSSN });
//        }
//    }
//}
