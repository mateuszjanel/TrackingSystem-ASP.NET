namespace ProjectTrackingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Comments");
            DropColumn("dbo.Comments", "ID");
            AddColumn("dbo.Comments", "CommentId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Comments", "CommentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Comments");
            DropColumn("dbo.Comments", "CommentId");
            AddPrimaryKey("dbo.Comments", "ID");
        }
    }
}
