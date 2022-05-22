using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Practice3.Models;

namespace Practice3.DB
{
    public class MyDataInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            context.Brands.AddRange(new List<Brand>()
            {

            });
            
            context.Brands.Add(new Models.Brand()
            {
                BrandId = 1,
                BrandName = "Gucci" 

            });
            context.Brands.Add(new Models.Brand()
            {
                BrandId = 2,
                BrandName = "Louis VuiTuoi"

            });
            context.Brands.Add(new Models.Brand()
            {
                BrandId = 3,
                BrandName = "Cho Dong Xuan"

            });

            context.Products.Add(new Models.Product()
            {
                ProductId = 1,
                BrandId = 1,
                Date = 2012,
                Description = "Suu tap thu dong",
                Name = "Ao 1"

            });
            context.Products.Add(new Models.Product()
            {
                ProductId = 2,
                BrandId = 1,
                Date = 2012,
                Description = "Suu tap thu dong",
                Name = "Ao 2"

            });
            context.Products.Add(new Models.Product()
            {
                ProductId = 3,
                BrandId = 3,
                Date = 2012,
                Description = "Suu tap thu dong",
                Name = "Ao 3"

            });
            base.Seed(context);
            //context.SaveChanges();
        }
    }
}