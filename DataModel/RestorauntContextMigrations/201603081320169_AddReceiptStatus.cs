namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceiptStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Receipt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentDateTime = c.DateTime(nullable: false),
                        ReceiptStatus = c.Int(nullable: false),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserInfo", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            AddColumn("dbo.Order", "Receipt_Id", c => c.Int());
            CreateIndex("dbo.Order", "Receipt_Id");
            AddForeignKey("dbo.Order", "Receipt_Id", "dbo.Receipt", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "Receipt_Id", "dbo.Receipt");
            DropForeignKey("dbo.Receipt", "Client_Id", "dbo.UserInfo");
            DropIndex("dbo.Receipt", new[] { "Client_Id" });
            DropIndex("dbo.Order", new[] { "Receipt_Id" });
            DropColumn("dbo.Order", "Receipt_Id");
            DropTable("dbo.Receipt");
        }
    }
}
