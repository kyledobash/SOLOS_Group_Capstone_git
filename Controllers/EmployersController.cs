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
            ViewBag.Id = employer.EmpId;
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
        public async Task<IActionResult> EditJob(int? id)        
        {
            if (id == null)
            {
                return NotFound();
            }
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }            
            return View(job);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditJob(Jobs job)
        {
            try
            {
                var JobInDB = _context.Jobs.Single(m => m.JobId == job.JobId);
                JobInDB.Name = job.Name;
                JobInDB.City = job.City;
                JobInDB.State = job.State;
                JobInDB.Requirements = job.Requirements;
                JobInDB.Descriptions = job.Descriptions;                

                _context.SaveChanges();
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
            var employer = _context.Employer.SingleOrDefault(m => m.EmpId == id);
            _context.Employer.Remove(employer);
            _context.SaveChanges();
            var employers = _context.Employer.ToList();
            return View("Index", employers);            
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
        public IActionResult CreateJob(Jobs job, int id)
        {
            try
            {
                job.EmployerId = id;
                
                _context.Jobs.Add(job);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));                
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult DeleteJob(int id)
        {
            var job = _context.Jobs.SingleOrDefault(m => m.JobId == id);
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            var jobs = _context.Jobs.ToList();
            return View("Index", jobs);
        }

        public IActionResult JobDetails(int id)
        {            
            var jobDetails = _context.Jobs.SingleOrDefault(m => m.JobId == id);
            return View(jobDetails);
        }

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
                var EmployerInDB = _context.Employer.Single(m => m.EmpId == employer.EmpId);
                EmployerInDB.FirstName = employer.FirstName;
                EmployerInDB.LastName = employer.LastName;
                EmployerInDB.Email = employer.Email;
                EmployerInDB.City = employer.City;
                EmployerInDB.State = employer.State;
                EmployerInDB.PhoneNumber = employer.PhoneNumber;

                _context.SaveChanges();
                // return RedirectToAction("Index", "Employer");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //helper method to save a new review to a developer
        public void StoreRatingAndReview(Developer developer, int rating, string review)
        {
            if (ValidateRating(rating))
            {
                RatingReview newReview = new RatingReview();
                newReview.DevId = developer.Id;
                newReview.Rating = rating;
                newReview.Review = review;

                _context.RatingReviews.Add(newReview);
            }
        }

        //calculates a developers' average rating
        public int CalculateAverageRating(Developer developer)
        {
            int runningSum = 0;

            foreach (RatingReview CurrentReview in (_context.RatingReviews.Where(r => r.DevId == developer.Id).ToList()))
            {
                runningSum += CurrentReview.Rating;
            }

            int average = runningSum / (_context.RatingReviews.Where(r => r.DevId == developer.Id).ToList()).Count();
            return average;
        }

        //ensures rating is within correct range
        public bool ValidateRating(int rating)
        {
            if (rating < 0 || rating > 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
