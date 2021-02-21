using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SOLOS_Group_Capstone.Data;
using SOLOS_Group_Capstone.Models;

namespace SOLOS_Group_Capstone.Controllers
{
    public class DevelopersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;

        public DevelopersController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
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

            
            if (developer.HasResume == false)
            {
                return RedirectToAction("CreateResume");
            }

            
            List<Developer> developers = new List<Developer>(); // Is this needed anymore ??????
            developers.Add(developer); // Is this needed anymore also ??????
            EmployerDeveloperResume employerDeveloperResume = new EmployerDeveloperResume();

            employerDeveloperResume.Developers = developer; // set employerDeveloperResume Developer to developer value --- N.E.T. -- 

            employerDeveloperResume.Resume = _context.Resumes.Where(r => r.Id == developer.Id).SingleOrDefault(); // set EDR resume to same index value as developer - should be identical id's --- N.E.T. --

            return View(employerDeveloperResume); // was passing developers but the view took a list of resume model types- changed the view to take a single Model EmployerDeveloperResume --- N.E.T. --

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
        public async Task <IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,City,State,BookMarkedJobListing,Pending_applications,JobCastId,IdentityUserId,IdentityUser")] Developer developer)
        {
            APICalling newCall = new APICalling(); // Created new class APICalling to handle all the calling instead of it being called on the developer controller --- N.E.T. --
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                developer.IdentityUserId = userId;
                _context.Add(developer);

                await _context.SaveChangesAsync();
                newCall.getJobSearchUrl(developer, _context); // calling api build functions --- N.E.T. --
                await newCall.APIJobsBuilder(developer, _context); // calling api build functions --- N.E.T. --
                return RedirectToAction(nameof(Index));
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

        public IActionResult CreateResume()
        {
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateResume(Resume resume)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var developer = _context.Developer.Where(d => d.IdentityUserId == userId).SingleOrDefault();

                resume.DevId = developer.Id;
                developer.HasResume = true;
                _context.Resumes.Add(resume);
                _context.Developer.Update(developer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult SingleFile(IFormFile file)
        {
            var dir = _env.ContentRootPath;
            using (var fileStream = new FileStream(Path.Combine(dir, "resume.doc"), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
            return View("CreateResume");
        }

        public IActionResult AddBookmark(int id)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBookmark(Jobs job)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var developer = _context.Developer.Where(d => d.IdentityUserId == userId).SingleOrDefault();

                var developerInDB = _context.Developer.Single(m => m.Id == developer.Id);
                developerInDB.Bookmarks.Add(job);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

                // Create a button to add jobs to the list of jobs ---J.A.P.-->
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewBookmarks(Developer developer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            developer = _context.Developer.Where(d => d.IdentityUserId == userId).SingleOrDefault();

            var bookmarks = developer.Bookmarks;
            
            return View(bookmarks);
            // Make a List view to show the list of bookmarks ---J.A.P.-->
            // Create button to take you to see that list ^
        }
    }

    
}
