namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIngredients : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Dish_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dish", t => t.Dish_Id)
                .Index(t => t.Dish_Id);
            
            AddColumn("dbo.Dish", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredient", "Dish_Id", "dbo.Dish");
            DropIndex("dbo.Ingredient", new[] { "Dish_Id" });
            DropColumn("dbo.Dish", "Description");
            DropTable("dbo.Ingredient");
        }
    }
}
