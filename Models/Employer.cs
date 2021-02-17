using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SOLOS_Group_Capstone.Models
{
    public class Employer
    {
        [Key]
        public int EmpId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }

        public string City { get; set; }
        public string State { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public Employer()
        {
            this.Developers = new HashSet<Developer>();
            this.Jobs = new HashSet<Jobs>();
            this.Resumes = new HashSet<Resume>();
        }
        public virtual ICollection<Developer> Developers { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
        public virtual ICollection<Resume> Resumes { get; set; }
    }
}
