using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SOLOS_Group_Capstone.Data;
using SOLOS_Group_Capstone.Models;

namespace SOLOS_Group_Capstone.Controllers
{
    public class EmployersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employers
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employer = _context.Developer.Where(c => c.IdentityUserId ==
            userId).SingleOrDefault();
            if (employer == null)
            {
                return RedirectToAction(nameof(Create));
            }
            // customer = _context.Customer.Include(m => m.Day);
            return View(employer);
        }

        // GET: Employers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employer = _context.Employer.SingleOrDefault(m => m.EmpId == id);            
            return View(employer);
        }

        // GET: Employers/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmpId,FirstName,LastName,Email,PhoneNumber,City,State,IdentityUserId")] Employer employer)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                employer.IdentityUserId = userId;
                _context.Add(employer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(employer);
            }
        }

        // GET: Employers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", employer.IdentityUserId);
            return View(employer);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,FirstName,LastName,Email,PhoneNumber,City,State,IdentityUserId")] Employer employer)
        {
            try
            {
                var employerInDB = _context.Developer.Single(m => m.Id == employer.EmpId);
                employerInDB.FirstName = employer.FirstName;
                employerInDB.LastName = employer.LastName;
                employerInDB.Email = employer.Email;
                employerInDB.City = employer.City;
                employerInDB.State = employer.State;


                _context.SaveChanges();
                return RedirectToAction("Index", "Developer");
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer
                .Include(e => e.IdentityUser)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // POST: Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employer = await _context.Employer.FindAsync(id);
            _context.Employer.Remove(employer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployerExists(int id)
        {
            return _context.Employer.Any(e => e.EmpId == id);
        }
    }
}
