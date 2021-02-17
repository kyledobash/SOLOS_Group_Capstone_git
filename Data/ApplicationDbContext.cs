using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOLOS_Group_Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOLOS_Group_Capstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
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
