using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SOLOS_Group_Capstone.Models
{
    public class Resume
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Developer Specialty")]
        public string Dev_specialty { get; set; }

        [Display(Name = "Languages")]
        public string Languages { get; set; }

        [Display(Name = "Education and Certificates")]
        public string Education_Certificates { get; set; }

        [Display(Name = "Projects")]
        public string Projects { get; set; }

        [Display(Name = "Web Portfolio")]
        public string Web_Portfolio { get; set; }

        [Display(Name = "Resume Copy")]
        public string Resume_Copy { get; set; }

        [ForeignKey("Developer")]
        public int? DevId { get; set; }
        public Developer? Developer { get; set; }
    }
}
