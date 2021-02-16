using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SOLOS_Group_Capstone.Models
{
    public class Resume
    {
        [key]
        public int Id { get; set; }

        [Display(Name = "Developer Specialty")]
        public string Dev_specialty { get; set; }

        [Display(Name = "Languages")]
        public string Languages { get; set; }

        [Display(Name = "Education and Certificates")]
        public string Education_Certificates { get; set; }

        [Display(Name = "Prjoects")]
        public string Projects { get; set; }

        [Display(Name = "Web Portfolio")]
        public string Web_Portfolio { get; set; }

        [Display(Name = "Resume Copy")]
        public string Resume_Copy { get; set; }
    }
}
