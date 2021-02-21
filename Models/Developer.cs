using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SOLOS_Group_Capstone.Models
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public double PhoneNumber { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "url")]
        public string url { get; set; }

        [Display(Name = "Bookmarked Job Listings")]
        public double BookMarkedJobListing { get; set; } // employeer id . job id ( ex employee with id 4 and job listing with id 18 = 4.18

        [Display(Name = "Pending Applications")]
        public int Pending_applications { get; set; }

        [ForeignKey("JobCast")] //double check that this matches with jobcast junction
        public int JobCastId { get; set; } // get first value as key for jobcastId . then be able to access applicationdbcontext through LINQ . notation.

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [Display(Name = "Has Resume")]
        public bool HasResume { get; set; }
        
        [Display(Name = "Ratings and Reviews")]
        public List<Tuple<int, string>> RatingsAndReviews { get; set; }
    }
}
