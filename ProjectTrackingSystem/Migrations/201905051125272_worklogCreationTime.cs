namespace ProjectTrackingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class worklogCreationTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Worklogs", "CreationTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Worklogs", "CreationTime");
        }
    }
}
