namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iop : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "ApplicationUser_Id", newName: "VerantwoordelijkeId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_ApplicationUser_Id", newName: "IX_VerantwoordelijkeId");
            DropColumn("dbo.AspNetUsers", "Verantwoordelijke");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Verantwoordelijke", c => c.String());
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_VerantwoordelijkeId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "VerantwoordelijkeId", newName: "ApplicationUser_Id");
        }
    }
}
