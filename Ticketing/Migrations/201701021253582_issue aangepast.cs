namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issueaangepast : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Issues", "Onderwerp", c => c.String(nullable: false));
            AlterColumn("dbo.Issues", "Omschrijving", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Issues", "Omschrijving", c => c.String());
            AlterColumn("dbo.Issues", "Onderwerp", c => c.String());
        }
    }
}
