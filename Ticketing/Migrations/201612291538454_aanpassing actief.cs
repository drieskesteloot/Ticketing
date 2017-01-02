namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aanpassingactief : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Actief", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Actief", c => c.Boolean(nullable: false));
        }
    }
}
