using Practice3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Practice3.DB
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DemoDB")
        {
                
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}