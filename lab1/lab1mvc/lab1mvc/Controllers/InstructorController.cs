using lab1mvc.context;
using lab1mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;


namespace lab1mvc.Controllers
{
    public class InstructorController : Controller
    {
        private readonly dblab1 _context = new dblab1 ();

        //getall
        public IActionResult GetAll()
        {
            var instructors = _context.Instructors.ToList();


            return View(instructors);
        }

        //get id
        public IActionResult GetById(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);

            if (instructor == null)
                return NotFound();

            return View("Details", instructor);
        }
        //getbyname
        public IActionResult GetByName(string name)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Name == name);

            if (instructor == null)
                return NotFound();

            return View("Details", instructor);
        }
        //add
        public IActionResult Add()
        {
            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Instructor instructor)
        {
            if (instructor.Name != null)
            {
                _context.Instructors.Add(instructor);
                _context.SaveChanges();
                return RedirectToAction("GetAll");
            }

            ViewBag.Departments = _context.Departments.ToList();
            return View("Add", instructor);
        }

        //edit
        public IActionResult Edit(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);


            ViewBag.Departments = _context.Departments.ToList();
            return View(instructor);
        }
        [HttpPost]
        public IActionResult Edit(Instructor updatedInstructor)
        {
            if (updatedInstructor == null)
            {
                return NotFound();
            }

            _context.Update(updatedInstructor);
            _context.SaveChanges();

            return RedirectToAction("GetAll");
        }
        public IActionResult Delete(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);


            _context.Instructors.Remove(instructor);
            _context.SaveChanges();
            return RedirectToAction("GetAll");
        }


    }
}


