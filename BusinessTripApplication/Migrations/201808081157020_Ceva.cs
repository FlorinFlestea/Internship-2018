namespace BusinessTripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ceva : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "ActivationCodeExpireDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "ActivationCodeExpireDate", c => c.DateTime(nullable: false));
        }
    }
}
