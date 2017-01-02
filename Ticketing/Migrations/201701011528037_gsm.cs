namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gsm : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "GsmNummer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "GsmNummer", c => c.Int());
        }
    }
}
