namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class i : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "CreationGebruikerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Issues", "SolverGebruikerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Issues", "CreationGebruikerId");
            CreateIndex("dbo.Issues", "SolverGebruikerId");
            AddForeignKey("dbo.Issues", "CreationGebruikerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Issues", "SolverGebruikerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "SolverGebruikerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Issues", "CreationGebruikerId", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "SolverGebruikerId" });
            DropIndex("dbo.Issues", new[] { "CreationGebruikerId" });
            DropColumn("dbo.Issues", "SolverGebruikerId");
            DropColumn("dbo.Issues", "CreationGebruikerId");
        }
    }
}
