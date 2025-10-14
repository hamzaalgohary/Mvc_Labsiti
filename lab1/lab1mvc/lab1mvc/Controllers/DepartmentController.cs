using Microsoft.AspNetCore.Mvc;
using lab1mvc.context;
using lab1mvc.Models;


namespace lab1mvc.Controllers
{
    public class DepartmentController : Controller
    {
        dblab1 _context = new dblab1();

        public IActionResult GetAllDepartments()
        {
            var depts = _context.Departments.ToList();
            return View(depts);
        }


        public IActionResult GetByName(string name)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Name == name);
            if (dept == null)
            {
                return NotFound();

            }
            return View("DepartmentDetails", dept);

        }
        public IActionResult GetDepartmentbyid(int id)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return NotFound();

            }
            return View("DepartmentDetails", dept);

        }
        public IActionResult AddDepartment()
        {
            return View();
        }
        public IActionResult AddNewDepartment(string name, string manager, string location, Branch branch)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var department = new Department
                {
                    Name = name,
                    Manager = manager,
                    Location = location,
                    Branch = branch
                };

                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("GetAllDepartments");
            }

            return View("AddDepartment");
        }
    }
}
