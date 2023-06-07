namespace WebApi_Practice_003.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial00 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FoodProductsModels", "CategoryId", "dbo.ProductCategories");
            DropIndex("dbo.FoodProductsModels", new[] { "CategoryId" });
            AlterColumn("dbo.FoodProductsModels", "CategoryId", c => c.Int());
            CreateIndex("dbo.FoodProductsModels", "CategoryId");
            AddForeignKey("dbo.FoodProductsModels", "CategoryId", "dbo.ProductCategories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodProductsModels", "CategoryId", "dbo.ProductCategories");
            DropIndex("dbo.FoodProductsModels", new[] { "CategoryId" });
            AlterColumn("dbo.FoodProductsModels", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.FoodProductsModels", "CategoryId");
            AddForeignKey("dbo.FoodProductsModels", "CategoryId", "dbo.ProductCategories", "CategoryId", cascadeDelete: true);
        }
    }
}
