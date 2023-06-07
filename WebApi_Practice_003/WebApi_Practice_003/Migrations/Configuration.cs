namespace WebApi_Practice_003.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApi_Practice_003.Models.TestContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.ProductCategorys.AddOrUpdate(
                pc=>pc.CategoryId,
                new ProductCategory { CategoryId=1, CategoryName="Fruits" },
                new ProductCategory { CategoryId = 2, CategoryName = "Vegetables" },
                new ProductCategory { CategoryId = 3, CategoryName = "Meats" },
                new ProductCategory { CategoryId = 4, CategoryName = "Drinks" },
                new ProductCategory { CategoryId = 5, CategoryName = "Diary" }
                );
            context.FoodProducts.AddOrUpdate(
                pf => pf.Id,
                new FoodProductsModel { Id = 1, ProductName = "Apple", CategoryId = 1, Price = decimal.Parse("1.00"), Quantity = 5, ImageFileName = "/Images/apple.png" },
                new FoodProductsModel { Id = 2, ProductName = "Mango", CategoryId = 1, Price = decimal.Parse("2.00"), Quantity = 10, ImageFileName = "/Images/mango.png" },
                new FoodProductsModel { Id = 3, ProductName = "Spinach", CategoryId = 2, Price = decimal.Parse("2.00"), Quantity = 3, ImageFileName = "/Images/spinach.png" },
                new FoodProductsModel { Id = 4, ProductName = "Kale", CategoryId = 2, Price = decimal.Parse("2.00"), Quantity = 3, ImageFileName = "/Images/kale.png" },
                new FoodProductsModel { Id = 5, ProductName = "Chicken", CategoryId = 3, Price = decimal.Parse("0.99"), Quantity = 50, ImageFileName = "/Images/chicken.png" },
                new FoodProductsModel { Id = 6, ProductName = "Beef", CategoryId = 3, Price = decimal.Parse("2.99"), Quantity = 50, ImageFileName = "/Images/beef.png" },
                new FoodProductsModel { Id = 7, ProductName = "Coke", CategoryId = 4, Price = decimal.Parse("1.99"), Quantity = 50, ImageFileName = "/Images/coke.png" },
                new FoodProductsModel { Id = 8, ProductName = "Sprite", CategoryId = 4, Price = decimal.Parse("1.99"), Quantity = 50, ImageFileName = "/Images/sprite.png" },
                new FoodProductsModel { Id = 9, ProductName = "Hersheys", CategoryId =5, Price = decimal.Parse("1.99"), Quantity = 50, ImageFileName = "/Images/hersheys.png" },
                new FoodProductsModel { Id = 10, ProductName = "Tobalco", CategoryId = 5, Price = decimal.Parse("1.99"), Quantity = 50, ImageFileName = "/Images/tobalco.png" }
                );
        }
    }
}
