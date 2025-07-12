using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovaMed.Data;
using NovaMed.Models;

namespace NovaMed.Controllers
{
    public class StudentController : Controller
    {
        private readonly NovaMedContext _context;
        public StudentController(NovaMedContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new StudentForCreation();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentForCreation model)
        {
            var student = new Student
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            var model = new StudentForModification
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentForModification model)
        {
            var student = new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            _context.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
