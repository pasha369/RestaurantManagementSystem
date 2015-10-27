namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Author : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "Author_Id", c => c.Int());
            CreateIndex("dbo.Review", "Author_Id");
            AddForeignKey("dbo.Review", "Author_Id", "dbo.UserInfo", "Id");
            DropColumn("dbo.Review", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Review", "Author", c => c.String(maxLength: 60));
            DropForeignKey("dbo.Review", "Author_Id", "dbo.UserInfo");
            DropIndex("dbo.Review", new[] { "Author_Id" });
            DropColumn("dbo.Review", "Author_Id");
        }
    }
}
