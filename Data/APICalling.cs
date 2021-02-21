using Newtonsoft.Json;
using SOLOS_Group_Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SOLOS_Group_Capstone.Data
{
    public class APICalling
    {
        public async Task APIJobsBuilder(Developer developer, ApplicationDbContext _context)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(developer.url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<APIJobSearchIncoming> jobSearch = JsonConvert.DeserializeObject<List<APIJobSearchIncoming>>(jsonResult);
                APIJobSearchSaved savedJobs = new APIJobSearchSaved();
                int i = 0;
                foreach (var item in jobSearch)
                {
                    savedJobs.id = _context.ApiJobs.Count() + 1 .ToString(); // this does work but it does not operate like you would expect. --- N.E.T. --
                    savedJobs.company = jobSearch[i].company;
                    savedJobs.company_logo = jobSearch[i].company_logo;
                    savedJobs.company_url = jobSearch[i].company_url;
                    savedJobs.created_at = jobSearch[i].created_at;
                    savedJobs.description = jobSearch[i].description;
                    savedJobs.how_to_apply = jobSearch[i].how_to_apply;
                    savedJobs.location = jobSearch[i].location;
                    savedJobs.title = jobSearch[i].title;
                    savedJobs.type = jobSearch[i].type;
                    i++;
                    
                    dataBaseManager(savedJobs, _context); // stores object in apijobs table --- N.E.T. --

                }


            }
        }
        public void dataBaseManager(APIJobSearchSaved jobToSave,ApplicationDbContext _context)
        {
            _context.ApiJobs.Add(jobToSave);
            _context.SaveChanges();
        }
        public Developer getJobSearchUrl(Developer developer, ApplicationDbContext _context)
        {
            developer.url = $"https://jobs.github.com/positions.json?&location={developer.State}";
            _context.Update(developer);
            _context.SaveChanges();

            return developer;
        }
    }
}
