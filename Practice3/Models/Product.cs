using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice3.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public int Date { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}