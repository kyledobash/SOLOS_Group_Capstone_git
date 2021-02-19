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
            var employer = _context.Employer.Where(c => c.IdentityUserId ==
            userId).SingleOrDefault();
            if (employer == null)
            {
                return RedirectToAction(nameof(Create));
            }
            var employerJobs = _context.Jobs.Where(c => c.EmployerId == employer.EmpId).ToList();
            if (employerJobs.Count == 0)
            {
                return RedirectToAction("CreateJob", new {id = employer.EmpId});
            }
            return View(employerJobs);
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
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,City,State")] Employer employer)
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

            //return View();
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employer employer)
        {
            try
            {
                var employerInDB = _context.Employer.Single(m => m.EmpId == employer.EmpId);
                employerInDB.FirstName = employer.FirstName;
                employerInDB.LastName = employer.LastName;
                employerInDB.Email = employer.Email;
                employerInDB.City = employer.City;
                employerInDB.State = employer.State;
                employerInDB.PhoneNumber = employer.PhoneNumber;

                _context.SaveChanges();
                // return RedirectToAction("Index", "Employer");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employers/Delete/5
        public IActionResult Delete(int id)
        {
            var developer = _context.Developer.SingleOrDefault(m => m.Id == id);
            _context.Developer.Remove(developer);
            _context.SaveChanges();
            var developers = _context.Developer.ToList();
            return View("Index", developers);            
        } 


        public IActionResult CreateJob(int id)
        {
             // _context.Jobs.Where(c => c.EmployerId == id);
            
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public IActionResult CreateJob(Jobs job, int id)
        {
            try
            {
                job.EmployerId = id;
=======
        public IActionResult CreateJob(Jobs job, int employerId)
        {
            try
            {
                job.EmployerId = employerId;
>>>>>>> 45d05fc192d92fa140f8cce2164e053b47b86812
                _context.Jobs.Add(job);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));                
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
