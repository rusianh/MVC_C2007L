using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EAP_C2007L_NguyenDucHuy.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=DefaultConnection") 
        {
            Database.Initialize(true);
        }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<Album> Albums { get; set; }

        
    }
}