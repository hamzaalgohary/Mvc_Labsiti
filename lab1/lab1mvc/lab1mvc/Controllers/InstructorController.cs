using lab1mvc.context;
using lab1mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using lab1mvc.Filters;
using lab1mvc.Repository;


namespace lab1mvc.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IGenericRepository<Instructor> _instructorRepo;
        private readonly IGenericRepository<Department> _departmentRepo;

        public InstructorController(IGenericRepository<Instructor> instructorRepo, IGenericRepository<Department> departmentRepo)
        {
            _instructorRepo = instructorRepo;
            _departmentRepo = departmentRepo;
        }

        [Route("allInstructors")]
        //[TypeFilter(typeof(CachResourceFilter))]
        public IActionResult GetAll()
        {
            var instructors = _instructorRepo.GetAll();
            return View(instructors);
        }

        public IActionResult GetById(int id)
        {
            var instructor = _instructorRepo.GetById(id);
            if (instructor == null)
                return NotFound();

            return View("Details", instructor);
        }

        public IActionResult GetByName(string name)
        {
            var instructor = _instructorRepo
                .Find(i => i.Name == name)
                .FirstOrDefault();

            if (instructor == null)
                return NotFound();

            return View("Details", instructor);
        }

        public IActionResult Add()
        {
            ViewBag.Departments = _departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentRepo.GetAll();
                return View(instructor);
            }

            _instructorRepo.Add(instructor);
            _instructorRepo.Save();

            return RedirectToAction("GetAll");
        }

        public IActionResult Edit(int id)
        {
            var instructor = _instructorRepo.GetById(id);
            if (instructor == null)
                return NotFound();

            ViewBag.Departments = _departmentRepo.GetAll();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor updatedInstructor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentRepo.GetAll();
                return View(updatedInstructor);
            }

            _instructorRepo.Update(updatedInstructor);
            _instructorRepo.Save();

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var instructor = _instructorRepo.GetById(id);
            if (instructor == null)
                return Json(new { success = false, message = "Instructor not found." });

            _instructorRepo.Delete(instructor);
            _instructorRepo.Save();

            return Json(new { success = true, message = "✅ Instructor deleted successfully." });
        }
        [HttpPost]
        public IActionResult SaveToSession(string userInput)
        {
            if (!string.IsNullOrEmpty(userInput))
            {
                HttpContext.Session.SetString("UserValue", userInput);
                return Content($" Value '{userInput}' has been saved in session.");
            }

            return Content("⚠️ Please enter a valid value.");
        }
        public IActionResult ShowSessionValue()
        {
            var value = HttpContext.Session.GetString("UserValue");

            if (string.IsNullOrEmpty(value))
                return Content("⚠ No value found in session.");

            return Content($" Value from session: {value}");
        }

    }
}





//until to lab 6
//        private readonly dblab1 _context = new dblab1 ();
//        [Route("allInstructors")]
//        [TypeFilter(typeof(CachResourceFilter))]

//        public IActionResult GetAll()
//        {
//            var instructors = _context.Instructors.ToList();


//            return View(instructors);
//        }

//        public IActionResult GetById(int id)
//        {
//            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);

//            if (instructor == null)
//                return NotFound();

//            return View("Details", instructor);
//        }
//        public IActionResult GetByName(string name)
//        {
//            var instructor = _context.Instructors.FirstOrDefault(i => i.Name == name);

//            if (instructor == null)
//                return NotFound();

//            return View("Details", instructor);
//        }
//        public IActionResult Add()
//        {
//            ViewBag.Departments = _context.Departments.ToList();
//            return View();
//        }
//        [HttpPost]
//        public IActionResult Add(Instructor instructor)
//        {
//            if (instructor.Name != null)
//            {
//                _context.Instructors.Add(instructor);
//                _context.SaveChanges();
//                return RedirectToAction("GetAll");
//            }

//            ViewBag.Departments = _context.Departments.ToList();
//            return View("Add", instructor);
//        }

//        public IActionResult Edit(int id)
//        {
//            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);


//            ViewBag.Departments = _context.Departments.ToList();
//            return View(instructor);
//        }
//        [HttpPost]

//        public IActionResult Edit(Instructor updatedInstructor)
//        {
//            if (updatedInstructor == null)
//            {
//                return NotFound();
//            }

//            _context.Update(updatedInstructor);
//            _context.SaveChanges();

//            return RedirectToAction("GetAll");
//        }
//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);

//            if (instructor == null)
//                return Json(new { success = false, message = "Instructor not found." });

//            _context.Instructors.Remove(instructor);
//            _context.SaveChanges();

//            return Json(new { success = true, message = "✅ Instructor deleted successfully." });
//        }


//        [HttpPost]
//        public IActionResult SaveToSession(string userInput)
//        {
//            if (!string.IsNullOrEmpty(userInput))
//            {
//                HttpContext.Session.SetString("UserValue", userInput);
//                return Content($" Value '{userInput}' has been saved in session.");
//            }

//            return Content("⚠️ Please enter a valid value.");
//        }
//        public IActionResult ShowSessionValue()
//        {
//            var value = HttpContext.Session.GetString("UserValue");

//            if (string.IsNullOrEmpty(value))
//                return Content("⚠ No value found in session.");

//            return Content($" Value from session: {value}");
//        }

//    }
//}

























//lab5
//        //getall
//        public IActionResult GetAll()
//        {
//            var instructors = _context.Instructors.ToList();


//            return View(instructors);
//        }

//        public IActionResult GetById(int id)
//        {
//            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);

//            if (instructor == null)
//                return NotFound();

//            return View("Details", instructor);
//        }
//        public IActionResult GetByName(string name)
//        {
//            var instructor = _context.Instructors.FirstOrDefault(i => i.Name == name);

//            if (instructor == null)
//                return NotFound();

//            return View("Details", instructor);
//        }
//        public IActionResult Add()
//        {
//            ViewBag.Departments = _context.Departments.ToList();
//            return View();
//        }
//        [HttpPost]
//        public IActionResult Add(Instructor instructor)
//        {
//            if (instructor.Name != null)
//            {
//                _context.Instructors.Add(instructor);
//                _context.SaveChanges();
//                return RedirectToAction("GetAll");
//            }

//            ViewBag.Departments = _context.Departments.ToList();
//            return View("Add", instructor);
//        }

//        public IActionResult Edit(int id)
//        {
//            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);


//            ViewBag.Departments = _context.Departments.ToList();
//            return View(instructor);
//        }
//        [HttpPost]
//        public IActionResult Edit(Instructor updatedInstructor)
//        {
//            if (updatedInstructor == null)
//            {
//                return NotFound();
//            }

//            _context.Update(updatedInstructor);
//            _context.SaveChanges();

//            return RedirectToAction("GetAll");
//        }
//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);

//            if (instructor == null)
//                return Json(new { success = false, message = "Instructor not found." });

//            _context.Instructors.Remove(instructor);
//            _context.SaveChanges();

//            return Json(new { success = true, message = "✅ Instructor deleted successfully." });
//        }



//    }
//}
