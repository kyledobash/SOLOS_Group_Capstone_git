using SOLOS_Group_Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SOLOS_Group_Capstone.Data
{
    public class JobCastDbContext : DbContext
    {
        public JobCastDbContext() : base("JobCastDb-DataAnnotations")
        {
        }

        public DbSet<Employer> Employers { get; set; }
        public DbSet<Developer> Developer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
