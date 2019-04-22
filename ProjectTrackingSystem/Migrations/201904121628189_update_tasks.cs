namespace ProjectTrackingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "Project_projectID", c => c.Int());
            CreateIndex("dbo.Tasks", "Project_projectID");
            AddForeignKey("dbo.Tasks", "Project_projectID", "dbo.Projects", "projectID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Project_projectID", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "Project_projectID" });
            DropColumn("dbo.Tasks", "Project_projectID");
        }
    }
}
