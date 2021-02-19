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
    public class DevelopersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevelopersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Developers
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var developer = _context.Developer.Where(c => c.IdentityUserId ==
            userId).SingleOrDefault();
            if (developer == null)
            {
                return RedirectToAction(nameof(Create));
            }
            var devResume = _context.Resumes.Where(c => c.Id == developer.Id).SingleOrDefault();
            if (devResume == null)
            {
                return RedirectToAction("CreateResume", new {id = developer.Id });
            }
            getJobSearchUrl(developer.State, developer.Skill,developer); // Skill needs to be added to developer model.
            return View(developer);
        }
        public void getJobSearchUrl(string state,string skill,Developer developer)
        {
            developer.url = $"https://jobs.github.com/positions.json?description={skill}&location={state}";
            _context.Update(developer); // url string in the model
            _context.SaveChanges();
        }

        // GET: Developers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var developer = _context.Developer.SingleOrDefault(m => m.Id == id);
            return View(developer);
        }

        // GET: Developers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Developers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,City,State,BookMarkedJobListing,Pending_applications,JobCastId")] Developer developer)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                developer.IdentityUserId = userId;
                _context.Add(developer);
                _context.SaveChanges();
                return RedirectToAction(nameof(CreateResume));
            }
            catch
            {
                return View(developer);
            }
        }

        // GET: Developers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var developer = await _context.Developer.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", developer.IdentityUserId);
            return View(developer);

            //return View();
        }

        // POST: Developers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Developer developer)
        {
            try
            {
                var developerInDB = _context.Developer.Single(m => m.Id == developer.Id);
                developerInDB.FirstName = developer.FirstName;
                developerInDB.LastName = developer.LastName;
                developerInDB.Email = developer.Email;
                developerInDB.City = developer.City;
                developerInDB.State = developer.State;
                developerInDB.BookMarkedJobListing = developer.BookMarkedJobListing;
                developerInDB.Pending_applications = developer.Pending_applications;
                _context.SaveChanges();
                // return RedirectToAction("Index", "Developer");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Developers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var developer = await _context.Developer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (developer == null)
            {
                return NotFound();
            }

            return View(developer);
        }

        // POST: Developers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public IActionResult Delete(int id)
        {
            var developer = _context.Developer.SingleOrDefault(m => m.Id == id);
            _context.Developer.Remove(developer);
            _context.SaveChanges();
            var developers = _context.Developer.ToList();
            return View("Index", developers);

            //public async Task<IActionResult> DeleteConfirmed(int id)
            //{
            //    var developer = await _context.Developer.FindAsync(id);
            //    _context.Developer.Remove(developer);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            //private bool DeveloperExists(int id)
            //{
            //    return _context.Developer.Any(e => e.Id == id);
            //}
        }

        public IActionResult CreateResume(int id)
        {
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateResume(Resume resume, int id)
        {
            try
            {
                resume.Id = id;
                _context.Resumes.Add(resume);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
