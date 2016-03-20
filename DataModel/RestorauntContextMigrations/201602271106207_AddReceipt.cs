namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceipt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Receipt", "Client_Id", "dbo.ClientInfo");
            DropForeignKey("dbo.Order", "Client_Id", "dbo.ClientInfo");
            DropForeignKey("dbo.Order", "Receipt_Id", "dbo.Receipt");
            DropIndex("dbo.Receipt", new[] { "Client_Id" });
            DropIndex("dbo.Order", new[] { "Client_Id" });
            DropIndex("dbo.Order", new[] { "Receipt_Id" });
            DropColumn("dbo.Order", "Client_Id");
            DropColumn("dbo.Order", "Receipt_Id");
            DropTable("dbo.Receipt");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Receipt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentDateTime = c.DateTime(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Order", "Receipt_Id", c => c.Int());
            AddColumn("dbo.Order", "Client_Id", c => c.Int());
            CreateIndex("dbo.Order", "Receipt_Id");
            CreateIndex("dbo.Order", "Client_Id");
            CreateIndex("dbo.Receipt", "Client_Id");
            AddForeignKey("dbo.Order", "Receipt_Id", "dbo.Receipt", "Id");
            AddForeignKey("dbo.Order", "Client_Id", "dbo.ClientInfo", "Id");
            AddForeignKey("dbo.Receipt", "Client_Id", "dbo.ClientInfo", "Id");
        }
    }
}
