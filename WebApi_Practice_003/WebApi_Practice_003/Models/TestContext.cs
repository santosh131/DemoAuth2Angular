using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApi_Practice_003.Models
{
    public class TestContext :DbContext
    {
        public TestContext():base("name=DefaultConnection")
        {

        }

        public DbSet<FoodProductsModel> FoodProducts { get; set; }

        public DbSet<ProductCategory> ProductCategorys { get; set; }
    }
}