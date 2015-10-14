namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "PhotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfo", "PhotoUrl");
        }
    }
}
