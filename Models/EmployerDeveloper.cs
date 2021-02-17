using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SOLOS_Group_Capstone.Models
{
    public class EmployerDeveloper
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Employers")]
        public int EmployerId { get; set; }
        public Employer Employers { get; set; }

        [ForeignKey("Developers")]
        public int DeveloperId { get; set; }
        public Developer Developers { get; set; }
        
    }
}
