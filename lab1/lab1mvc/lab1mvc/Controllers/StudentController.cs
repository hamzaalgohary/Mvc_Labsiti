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
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddNew(Student student)
        {
            if (student.Name != null)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("getall");

            }
            return View("Add", student);
        }

        public IActionResult Edit(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);
            var departments = _context.Departments.ToList();

            ViewBag.Departments = departments;
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {


            _context.Update(student);
            _context.SaveChanges();

            return RedirectToAction("getall");
        }

        public IActionResult Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);


            _context.Students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("GetAll");
        }



    }
}
