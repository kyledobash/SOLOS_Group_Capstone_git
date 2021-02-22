using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SOLOS_Group_Capstone.Models;



namespace SOLOS_Group_Capstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Employer> Employer { get; set; }
        
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<APIJobSearchSaved> ApiJobs { get; set; }

        public DbSet<RatingReview> RatingReviews { get; set; }

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


            builder.Entity<Employer>()
            .HasData(
            new Employer
            {
                EmpId = 1,
                BuisnessName  = "KB Construction",
                FirstName = "Kyle",
                LastName = "Dobash",
                Email = "kyledobash@yahoo.com",
                PhoneNumber = 6029994298,
                City = "PHX",
                State = "AZ"
            }
            ,
            new Employer
            {
                EmpId = 2,
                BuisnessName = "Wells Fargo",
                FirstName = "Richard",
                LastName = "Rich",
                Email = "RRich@yahoo.com",
                PhoneNumber = 6023784644,
                City = "PHX",
                State = "AZ"
            }
            ,
            new Employer
            {
                EmpId = 3,
                BuisnessName = "Papago Golf Course",
                FirstName = "Shooter",
                LastName = "McGavin",
                Email = "Mcgavin@yahoo.com",
                PhoneNumber = 6022758428,
                City = "PHX",
                State = "AZ"
            }
            ,
            new Employer
            {
                EmpId = 4,
                BuisnessName = "Barnes & Noble",
                FirstName = "Joe",
                LastName = "Goldberg",
                Email = "JGoldberg@yahoo.com",
                PhoneNumber = 6026780088,
                City = "PHX",
                State = "AZ"
            }
            ,
            new Employer
            {
                EmpId = 5,
                BuisnessName = "Costco",
                FirstName = "MIchael",
                LastName = "Scott",
                Email = "Mscott@yahoo.com",
                PhoneNumber = 6029994298,
                City = "PHX",
                State = "AZ"
            }
            );
           
            builder.Entity<Developer>()
           .HasData(
           new Developer
           {
               Id = 1,
               FirstName = "Kelly",
               LastName = "Penson",
               Email = "Penson@yahoo.com",
               PhoneNumber = 6027153648,
               City = "PHX",
               State = "AZ",


           }
            ,

           new Developer
           {
               Id = 2,
               FirstName = "Jim",
               LastName = "Halpert",
               Email = "Jhalpert@yahoo.com",
               PhoneNumber = 6024783264,
               City = "PHX",
               State = "AZ",

           }
           ,
           new Developer
           {
               Id = 3,
               FirstName = "Mark",
               LastName = "Jenson",
               Email = "Mjenson@yahoo.com",
               PhoneNumber = 6025896205,
               City = "PHX",
               State = "AZ",

           }
           ,
           new Developer
           {
               Id = 4,
               FirstName = "Max",
               LastName = "Roberts",
               Email = "Mroberts@yahoo.com",
               PhoneNumber = 6024698264,
               City = "PHX",
               State = "AZ",

           }
           ,
           new Developer
           {
               Id = 5,
               FirstName = "Tim",
               LastName = "Maxwell",
               Email = "Tmaxwell@yahoo.com",
               PhoneNumber = 6022589862,
               City = "PHX",
               State = "AZ",

           }
           );






        }
    }
    
}
