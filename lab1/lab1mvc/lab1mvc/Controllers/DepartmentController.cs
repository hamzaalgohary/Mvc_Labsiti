using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using lab1mvc.context;
//using lab1mvc.Migrations;
using lab1mvc.Models;
using lab1mvc.Filters;
using lab1mvc.Repository;


namespace lab1mvc.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IGenericRepository<Department> _departmentRepo;
        private readonly IGenericRepository<Student> _studentRepo;

        public DepartmentController(
            IGenericRepository<Department> departmentRepo,
            IGenericRepository<Student> studentRepo)
        {
            _departmentRepo = departmentRepo;
            _studentRepo = studentRepo;
        }

        [Route("allDepartments")]
        //[TypeFilter(typeof(CachResourceFilter))]
        [HeaderAuthorizeFilter("X-Secret-Key", "hamza123")]
        public IActionResult GetAll()
        {
            var departments = _departmentRepo.GetAll(
                d => d.Students,
                d => d.Courses,
                d => d.Instructors
            );

            return View(departments);
        }

        public IActionResult GetByName(string name)
        {
            var dept = _departmentRepo.Find(d => d.Name == name).FirstOrDefault();
            if (dept == null)
                return NotFound();

            return View("DepartmentDetails", dept);
        }

        public IActionResult GetById(int id)
        {
            var dept = _departmentRepo.GetById(id);
            if (dept == null)
                return NotFound();

            return View("DepartmentDetails", dept);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateLocation]
        public IActionResult Add(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepo.Add(department);
                _departmentRepo.Save();
                return RedirectToAction(nameof(GetAll));
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dept = _departmentRepo.GetById(id);
            if (dept == null)
                return NotFound();

            return View(dept);
        }

        [HttpPost]
        [ValidateLocation]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepo.Update(department);
                _departmentRepo.Save();
                return RedirectToAction(nameof(GetAll));
            }

            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var dept = _departmentRepo.GetById(id);
            if (dept == null)
                return Json(new { success = false, message = "Department not found." });

            var students = _studentRepo.Find(s => s.DepartmentId == id);
            if (students.Any())
                return Json(new { success = false, message = "❌ Cannot delete department because it has students." });

            _departmentRepo.Delete(dept);
            _departmentRepo.Save();

            return Json(new { success = true, message = "✅ Department deleted successfully." });
        }

        public IActionResult Index() => View();
    }
}







//untill lab 6
//        dblab1 _context = new dblab1();
//        [Route("allDepartments")]
//        [TypeFilter(typeof(CachResourceFilter))]
//        [HeaderAuthorizeFilter("X-Secret-Key", "hamza123")]

//        public IActionResult GetAll()
//        {
//            var depts = _context.Departments
//                         .Include(d => d.Students)
//                         .Include(d => d.Courses)
//                         .Include(d => d.Instructors)
//                         .ToList();

//            return View(depts);
//        }


//        public IActionResult GetByName(string name)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Name == name);
//            if (dept == null)
//            {
//                return NotFound();

//            }
//            return View("DepartmentDetails", dept);

//        }
//        public IActionResult Getbyid(int id)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
//            if (dept == null)
//            {
//                return NotFound();

//            }
//            return View("DepartmentDetails", dept);

//        }
//        public IActionResult Add()
//        {
//            return View();
//        }
//        [HttpPost]
//        [ValidateLocation]

//        public IActionResult Add(Department department)
//        {
//            if (department.Name != null)
//            {
//                _context.Departments.Add(department);
//                _context.SaveChanges();
//                return RedirectToAction(actionName: "GetAll");


//            }
//            return View(viewName: "Add", department);

//        }


//        public IActionResult Edit(int id)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
//            if (dept == null) return NotFound();
//            return View(dept);
//        }

//        [HttpPost]
//        [ValidateLocation] // <-- our custom filter

//        public IActionResult Edit(Department department)
//        {


//            _context.Update(department);
//            _context.SaveChanges();

//            return RedirectToAction("GetAll");
//        }

//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            var dept = _context.Departments
//                .Include(d => d.Students)
//                .Include(d => d.Courses)
//                .FirstOrDefault(d => d.Id == id);

//            if (dept == null)
//                return Json(new { success = false, message = "Department not found." });

//            if (dept.Students.Any())
//            {
//                return Json(new { success = false, message = "❌ Cannot delete this department because it has students." });
//            }

//            _context.Departments.Remove(dept);
//            _context.SaveChanges();

//            return Json(new { success = true, message = "✅ Department deleted successfully. All related courses were deleted." });
//        }
//        public IActionResult Index()
//        {

//            return View();
//        }

//    }
//}
























//lab5
//        public IActionResult GetAll()
//        {
//            var depts = _context.Departments
//                         .Include(d => d.Students)
//                         .Include(d => d.Courses)
//                         .Include(d => d.Instructors)
//                         .ToList();

//            return View(depts);
//        }


//        public IActionResult GetByName(string name)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Name == name);
//            if (dept == null)
//            {
//                return NotFound();

//            }
//            return View("DepartmentDetails", dept);

//        }
//        public IActionResult Getbyid(int id)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
//            if (dept == null)
//            {
//                return NotFound();

//            }
//            return View("DepartmentDetails", dept);

//        }
//        public IActionResult Add()
//        {
//            return View();
//        }
//        [HttpPost]

//        public IActionResult Add(Department department)
//        {
//            if (department.Name != null)
//            {
//                _context.Departments.Add(department);
//                _context.SaveChanges();
//                return RedirectToAction(actionName: "GetAll");


//            }
//            return View(viewName: "Add", department);

//        }


//        public IActionResult Edit(int id)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
//            if (dept == null) return NotFound();
//            return View(dept);
//        }

//        [HttpPost]
//        public IActionResult Edit(Department department)
//        {


//            _context.Update(department);
//            _context.SaveChanges();

//            return RedirectToAction("GetAll");
//        }

//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            var dept = _context.Departments
//                .Include(d => d.Students)
//                .Include(d => d.Courses)
//                .FirstOrDefault(d => d.Id == id);

//            if (dept == null)
//                return Json(new { success = false, message = "Department not found." });

//            if (dept.Students.Any())
//            {
//                return Json(new { success = false, message = "❌ Cannot delete this department because it has students." });
//            }

//            _context.Departments.Remove(dept);
//            _context.SaveChanges();

//            return Json(new { success = true, message = "✅ Department deleted successfully. All related courses were deleted." });
//        }
//        public IActionResult Index()
//        {

//            return View();
//        }

//    }
//}










//lab1mvc to lab4

//        //getalldepartment
//        public IActionResult GetAll()
//        {
//            var depts = _context.Departments
//                         .Include(d => d.Students)
//                         .Include(d => d.Courses)
//                         .Include(d => d.Instructors)
//                         .ToList();

//            return View(depts);
//        }

//        //getbyname 
//        public IActionResult GetByName(string name)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Name == name);
//            if (dept == null)
//            {
//                return NotFound();

//            }
//            return View("DepartmentDetails", dept);

//        }
//        //get id
//        public IActionResult Getbyid(int id)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
//            if (dept == null)
//            {
//                return NotFound();

//            }
//            return View("DepartmentDetails", dept);

//        }
//        //add
//        public IActionResult AddDepartment()
//        {
//            return View();
//        }
//        [HttpPost]

//        //add new
//        public IActionResult AddNewDepartment(Department department)
//        {
//            if (department.Name != null)
//            {
//                _context.Departments.Add(department);
//                _context.SaveChanges();
//                return RedirectToAction(actionName: "GetAllDepartments");


//            }
//            return View(viewName: "Add", department);

//        }

//        //edit
//        public IActionResult Edit(int id)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
//            if (dept == null) return NotFound();
//            return View(dept);
//        }

//        [HttpPost]
//        //edit
//        public IActionResult Edit(Department department)
//        {


//            _context.Update(department);
//            _context.SaveChanges();

//            return RedirectToAction("GetAllDepartments");
//        }

//        //delete
//        public IActionResult Delete(int id)
//        {
//            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);


//            _context.Departments.Remove(dept);
//            _context.SaveChanges();

//            return RedirectToAction("GetAllDepartments");
//        }

//    }
//}
