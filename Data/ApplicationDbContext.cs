using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOLOS_Group_Capstone.Models;
// using System.Data.Entity;



namespace SOLOS_Group_Capstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<EmployerDeveloper> EmployerDevelopers { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Resume> Resumes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Name = "Developer",
                NormalizedName = "DEVELOPER"
            }
            ,

            new IdentityRole
            {
                Name = "Employer",
                NormalizedName = "EMPLOYER"
            }
            );       
        }
    }         
}
