using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOLOS_Group_Capstone.Data;
using SOLOS_Group_Capstone.Models;

namespace SOLOS_Group_Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobSearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobSearch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIJobSearchSaved>>> GetApiJobs()
        {
            //Retrieve all apiJobCalls from db logic

            List<APIJobSearchSaved> apiJobsAvailible = _context.ApiJobs.ToList();
           // return Ok(apiJobsAvailible);
            return await _context.ApiJobs.ToListAsync();
        }

        // GET: api/JobSearch/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIJobSearchSaved>> GetAPIJobSearchSaved(string id)
        {
            var aPIJobSearchSaved = await _context.ApiJobs.FindAsync(id);

            if (aPIJobSearchSaved == null)
            {
                return NotFound();
            }

            return aPIJobSearchSaved;
        }

        // PUT: api/JobSearch/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAPIJobSearchSaved(string id, APIJobSearchSaved aPIJobSearchSaved)
        {
            if (id != aPIJobSearchSaved.id)
            {
                return BadRequest();
            }

            _context.Entry(aPIJobSearchSaved).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!APIJobSearchSavedExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/JobSearch
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<APIJobSearchSaved>> PostAPIJobSearchSaved(APIJobSearchSaved aPIJobSearchSaved)
        {
            _context.ApiJobs.Add(aPIJobSearchSaved);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (APIJobSearchSavedExists(aPIJobSearchSaved.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAPIJobSearchSaved", new { id = aPIJobSearchSaved.id }, aPIJobSearchSaved);
        }

        // DELETE: api/JobSearch/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIJobSearchSaved>> DeleteAPIJobSearchSaved(string id)
        {
            var aPIJobSearchSaved = await _context.ApiJobs.FindAsync(id);
            if (aPIJobSearchSaved == null)
            {
                return NotFound();
            }

            _context.ApiJobs.Remove(aPIJobSearchSaved);
            await _context.SaveChangesAsync();

            return aPIJobSearchSaved;
        }

        private bool APIJobSearchSavedExists(string id)
        {
            return _context.ApiJobs.Any(e => e.id == id);
        }
    }
}
