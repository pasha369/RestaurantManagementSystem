namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rstPic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurant", "PhotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurant", "PhotoUrl");
        }
    }
}
