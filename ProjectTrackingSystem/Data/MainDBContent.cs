using Microsoft.AspNet.Identity.EntityFramework;
using ProjectTrackingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectTrackingSystem.Data
{


    public class MainDbContext : IdentityDbContext<ApplicationUser>
    {
        public MainDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainDbContext, Migrations.Configuration>());
            //Database.SetInitializer<MainDbContext>(new CreateDatabaseIfNotExists<MainDbContext>());
        }

        public static MainDbContext Create()
        {
            return new MainDbContext();
        }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Worklog> Worklogs { get; set; }

    }
}