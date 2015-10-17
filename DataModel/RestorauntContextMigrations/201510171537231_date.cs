namespace DataModel.RestorauntContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservation", "From", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reservation", "DateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reservation", "From");
        }
    }
}
