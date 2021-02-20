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
        public DbSet<EmployerDeveloper> EmployerDevelopers { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        //public DbSet<APIJobSearch> ApiJobs { get; set; }

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
                FirstName = "Kyle",
                LastName = "Dobash",
                Email = "kyledobash@yahoo.com",
                PhoneNumber = 6029994298,
                City = "PHX",
                State = "AZ"
            }
            ); ;

            //TestCreate();
        }



        //public void TestCreate()
        //{
        //    Employer newEmployeer1 = new Employer();
        //    Employer newEmployeer2 = new Employer();
        //    Employer newEmployeer3 = new Employer();
        //    Employer newEmployeer4 = new Employer();
        //    Employer newEmployeer5 = new Employer();
        //    string name = "Steve";
        //    string name1 = "Mike";
        //    string name2 = "Bob";
        //    string name3 = "Jill";
        //    string name4 = "Lexus";
        //    newEmployeer1.FirstName = name;
        //    newEmployeer1.FirstName = name1;
        //    newEmployeer1.FirstName = name2;
        //    newEmployeer1.FirstName = name3;
        //    newEmployeer1.FirstName = name4;

        //    Employer.Add(newEmployeer1);
        //}
    }
    
}
