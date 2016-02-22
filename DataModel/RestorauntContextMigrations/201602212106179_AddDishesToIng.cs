namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDishesToIng : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ingredient", "Dish_Id", "dbo.Dish");
            DropIndex("dbo.Ingredient", new[] { "Dish_Id" });
            CreateTable(
                "dbo.IngredientDishes",
                c => new
                    {
                        Ingredient_Id = c.Int(nullable: false),
                        Dish_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ingredient_Id, t.Dish_Id })
                .ForeignKey("dbo.Ingredient", t => t.Ingredient_Id, cascadeDelete: true)
                .ForeignKey("dbo.Dish", t => t.Dish_Id, cascadeDelete: true)
                .Index(t => t.Ingredient_Id)
                .Index(t => t.Dish_Id);
            
            DropColumn("dbo.Ingredient", "Dish_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredient", "Dish_Id", c => c.Int());
            DropForeignKey("dbo.IngredientDishes", "Dish_Id", "dbo.Dish");
            DropForeignKey("dbo.IngredientDishes", "Ingredient_Id", "dbo.Ingredient");
            DropIndex("dbo.IngredientDishes", new[] { "Dish_Id" });
            DropIndex("dbo.IngredientDishes", new[] { "Ingredient_Id" });
            DropTable("dbo.IngredientDishes");
            CreateIndex("dbo.Ingredient", "Dish_Id");
            AddForeignKey("dbo.Ingredient", "Dish_Id", "dbo.Dish", "Id");
        }
    }
}
