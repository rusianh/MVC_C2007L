using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice3.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}