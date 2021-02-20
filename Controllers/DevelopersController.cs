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
        public async Task <IActionResult> Index()
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

            getJobSearchUrl(developer);
            await APIJobsBuilder(developer);
            
            List<Developer> developers = new List<Developer>();
            developers.Add(developer);
            return View(developers);

        }
        [HttpGet]
        public IActionResult Get()
        {
            // Retrieve all apiJobCalls from db logic

            List<APIJobSearch> apiJobsAvailible = _context.ApiJobs.ToList();
            return Ok(apiJobsAvailible);
        }
        public Developer getJobSearchUrl(Developer developer)
        {
            //developer.url = $"https://jobs.github.com/positions.json?description={developer.Skill}&location={developer.State}";
            developer.url = $"https://jobs.github.com/positions.json?description=python&location=new+york";
            _context.Update(developer); 
            _context.SaveChanges();

            return developer;
        }
        public async Task APIJobsBuilder(Developer developer)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(developer.url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                APIJobSearch jobSearch = JsonConvert.DeserializeObject<APIJobSearch>(jsonResult);

                //_context.ApiJobs.Add(jobSearch);
                //_context.SaveChanges();
                //var results = _context.ApiJobs.Where(r => r.id == developer.Id.ToString());
                //ViewBag.Results = results;
            }
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
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                developer.IdentityUserId = userId;
                _context.Add(developer);

                await _context.SaveChangesAsync();
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
    }
}
