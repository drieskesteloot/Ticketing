namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "TelefoonNummer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "TelefoonNummer", c => c.Int());
        }
    }
}
