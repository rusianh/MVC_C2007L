using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practice8._1.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name = DefaultConnection")
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
    }
}