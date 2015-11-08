namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menu : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CuisineRestaurants", newName: "RestaurantCuisines");
            DropPrimaryKey("dbo.RestaurantCuisines");
            AddColumn("dbo.Restaurant", "Menu_Id", c => c.Int());
            AddPrimaryKey("dbo.RestaurantCuisines", new[] { "Restaurant_Id", "Cuisine_Id" });
            CreateIndex("dbo.Restaurant", "Menu_Id");
            AddForeignKey("dbo.Restaurant", "Menu_Id", "dbo.Menu", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurant", "Menu_Id", "dbo.Menu");
            DropIndex("dbo.Restaurant", new[] { "Menu_Id" });
            DropPrimaryKey("dbo.RestaurantCuisines");
            DropColumn("dbo.Restaurant", "Menu_Id");
            AddPrimaryKey("dbo.RestaurantCuisines", new[] { "Cuisine_Id", "Restaurant_Id" });
            RenameTable(name: "dbo.RestaurantCuisines", newName: "CuisineRestaurants");
        }
    }
}
