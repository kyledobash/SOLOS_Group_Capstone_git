using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOLOS_Group_Capstone.Models
{
    public class EmployerDeveloperResume
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("Employers")]
        //public int EmployerId { get; set; }
        public Employer Employers { get; set; }

        //[ForeignKey("Developers")]
        //public int DeveloperId { get; set; }
        public Developer Developers { get; set; }

        public Resume Resume { get; set; }
        
    }
}
