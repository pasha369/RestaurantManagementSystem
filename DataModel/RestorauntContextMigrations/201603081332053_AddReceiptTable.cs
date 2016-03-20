namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceiptTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receipt", "Table_Id", c => c.Int());
            CreateIndex("dbo.Receipt", "Table_Id");
            AddForeignKey("dbo.Receipt", "Table_Id", "dbo.DinnerTable", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Receipt", "Table_Id", "dbo.DinnerTable");
            DropIndex("dbo.Receipt", new[] { "Table_Id" });
            DropColumn("dbo.Receipt", "Table_Id");
        }
    }
}
