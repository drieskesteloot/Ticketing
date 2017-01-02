namespace Ticketing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class r : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "CreationGebruikerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Issues", "SolverGebruikerId", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "CreationGebruikerId" });
            DropIndex("dbo.Issues", new[] { "SolverGebruikerId" });
            DropColumn("dbo.Issues", "CreationGebruikerId");
            DropColumn("dbo.Issues", "SolverGebruikerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "SolverGebruikerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Issues", "CreationGebruikerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Issues", "SolverGebruikerId");
            CreateIndex("dbo.Issues", "CreationGebruikerId");
            AddForeignKey("dbo.Issues", "SolverGebruikerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Issues", "CreationGebruikerId", "dbo.AspNetUsers", "Id");
        }
    }
}
