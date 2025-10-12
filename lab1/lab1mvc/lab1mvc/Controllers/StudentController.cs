using lab1mvc.context;
using Microsoft.AspNetCore.Mvc;

namespace lab1mvc.Controllers
{
    public class StudentController : Controller
    {
        dblab1 _context = new dblab1();
        public IActionResult getall()
        {
            var data= _context.Students.ToList();
            return View(data);
        }
        public IActionResult getbyId(int id)
        {
            //var student = _context.Students.Find();
            //return student;
            var student = _context.Students.FirstOrDefault(s => s.SSN == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
    }
    }
