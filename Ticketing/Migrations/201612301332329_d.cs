namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "CreationGebruikerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Issues", "SolverGebruikerId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Issues", "CreationGebruikerId");
            CreateIndex("dbo.Issues", "SolverGebruikerId");
            CreateIndex("dbo.AspNetUsers", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Issues", "CreationGebruikerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Issues", "SolverGebruikerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "SolverGebruikerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Issues", "CreationGebruikerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Issues", new[] { "SolverGebruikerId" });
            DropIndex("dbo.Issues", new[] { "CreationGebruikerId" });
            DropColumn("dbo.AspNetUsers", "ApplicationUser_Id");
            DropColumn("dbo.Issues", "SolverGebruikerId");
            DropColumn("dbo.Issues", "CreationGebruikerId");
        }
    }
}
