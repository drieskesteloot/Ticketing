namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cd : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Issues", name: "UserId", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Issues", name: "IX_UserId", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Issues", name: "IX_ApplicationUserId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Issues", name: "ApplicationUserId", newName: "UserId");
        }
    }
}
