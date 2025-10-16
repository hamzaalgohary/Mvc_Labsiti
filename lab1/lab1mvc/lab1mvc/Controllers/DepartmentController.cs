using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using lab1mvc.context;
//using lab1mvc.Migrations;
using lab1mvc.Models;


namespace lab1mvc.Controllers
{
    public class DepartmentController : Controller
    {
        dblab1 _context = new dblab1();

        //getalldepartment
        public IActionResult GetAllDepartments()
        {
            var depts = _context.Departments
                         .Include(d => d.Students)
                         .Include(d => d.Courses)
                         .Include(d => d.Instructors)
                         .ToList();

            return View(depts);
        }

        //getbyname 
        public IActionResult GetByName(string name)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Name == name);
            if (dept == null)
            {
                return NotFound();

            }
            return View("DepartmentDetails", dept);

        }
        //get id
        public IActionResult Getbyid(int id)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (dept == null)
            {
                return NotFound();

            }
            return View("DepartmentDetails", dept);

        }
        //add
        public IActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]

        //add new
        public IActionResult AddNewDepartment(Department department)
        {
            if (department.Name != null)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction(actionName: "GetAllDepartments");


            }
            return View(viewName: "Add", department);

        }

        //edit
        public IActionResult Edit(int id)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost]
        //edit
        public IActionResult Edit(Department department)
        {


            _context.Update(department);
            _context.SaveChanges();

            return RedirectToAction("GetAllDepartments");
        }

        //delete
        public IActionResult Delete(int id)
        {
            var dept = _context.Departments.FirstOrDefault(d => d.Id == id);


            _context.Departments.Remove(dept);
            _context.SaveChanges();

            return RedirectToAction("GetAllDepartments");
        }

    }
}
