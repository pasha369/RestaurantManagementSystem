namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "About", c => c.String());
            AddColumn("dbo.UserInfo", "Facebook", c => c.String());
            AddColumn("dbo.UserInfo", "Skype", c => c.String());
            AlterColumn("dbo.UserInfo", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfo", "Phone", c => c.Int(nullable: false));
            DropColumn("dbo.UserInfo", "Skype");
            DropColumn("dbo.UserInfo", "Facebook");
            DropColumn("dbo.UserInfo", "About");
        }
    }
}
