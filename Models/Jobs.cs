using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SOLOS_Group_Capstone.Models
{
    public class Jobs
    {
        [Key]
        public int JobId { get; set; }

        [Display(Name = "Job Name")]
        public string Name { get; set; }

        [Display(Name = "Requirements")]
        public string Requirements { get; set; }

        [Display(Name = "Descriptions")]
        public string Descriptions { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [ForeignKey("Employer")]
        public int? EmployerId { get; set; }
        public Employer? Employer { get; set; }

        [ForeignKey("Developer")]
        public int? DevId { get; set; }
        public Developer? Developer { get; set; }
    }
}
