using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MVCProject.DomainModels.Migrations;


namespace MVCProject.DomainModels
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext() : base("EmployeeDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmployeeDbContext, Configuration>());
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        
    }
}
