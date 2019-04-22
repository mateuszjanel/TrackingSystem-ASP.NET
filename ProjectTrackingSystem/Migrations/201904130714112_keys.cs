namespace ProjectTrackingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keys : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Worklogs");
            RenameColumn("dbo.Worklogs", "ID", "WorklogId");
            AddPrimaryKey("dbo.Worklogs", "WorklogId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Worklogs", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Worklogs");
            DropColumn("dbo.Worklogs", "WorklogId");
            AddPrimaryKey("dbo.Worklogs", "ID");
        }
    }
}
