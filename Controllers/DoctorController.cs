using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovaMed.Data;
using NovaMed.Models;

namespace NovaMed.Controllers
{
    public class DoctorController : Controller
    {
        private readonly NovaMedContext _context;
        public DoctorController(NovaMedContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _context.Doctors.ToListAsync();
            return View(doctors);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new DoctorForCreation();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorForCreation model)
        {
            var doctor = new Doctor
            {
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            var model = new DoctorForModification
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorForModification model)
        {
            var doctor = new Doctor
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            _context.Remove(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
