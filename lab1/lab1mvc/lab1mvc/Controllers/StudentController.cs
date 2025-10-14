using Microsoft.AspNetCore.Mvc;
using lab1mvc.context;
using lab1mvc.Models;

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
            return View();
        }
        public IActionResult AddNew(Student student)
        {
            if (student.Name != null)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("getall");

            }
            return View("Add");
        }



    }
}
