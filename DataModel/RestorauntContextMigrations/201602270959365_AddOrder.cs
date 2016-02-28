namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrder : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Order", name: "OrderDish_Id", newName: "Dish_Id");
            RenameIndex(table: "dbo.Order", name: "IX_OrderDish_Id", newName: "IX_Dish_Id");
            AddColumn("dbo.Order", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Order", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Order", "Client_Id", c => c.Int());
            AddColumn("dbo.Order", "Restaurant_Id", c => c.Int());
            CreateIndex("dbo.Order", "Client_Id");
            CreateIndex("dbo.Order", "Restaurant_Id");
            AddForeignKey("dbo.Order", "Client_Id", "dbo.ClientInfo", "Id");
            AddForeignKey("dbo.Order", "Restaurant_Id", "dbo.Restaurant", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "Restaurant_Id", "dbo.Restaurant");
            DropForeignKey("dbo.Order", "Client_Id", "dbo.ClientInfo");
            DropIndex("dbo.Order", new[] { "Restaurant_Id" });
            DropIndex("dbo.Order", new[] { "Client_Id" });
            DropColumn("dbo.Order", "Restaurant_Id");
            DropColumn("dbo.Order", "Client_Id");
            DropColumn("dbo.Order", "Created");
            DropColumn("dbo.Order", "Status");
            RenameIndex(table: "dbo.Order", name: "IX_Dish_Id", newName: "IX_OrderDish_Id");
            RenameColumn(table: "dbo.Order", name: "Dish_Id", newName: "OrderDish_Id");
        }
    }
}
