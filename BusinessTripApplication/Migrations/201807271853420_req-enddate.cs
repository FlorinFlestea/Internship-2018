namespace BusinessTripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reqenddate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trips", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trips", "EndDate", c => c.DateTime());
        }
    }
}
