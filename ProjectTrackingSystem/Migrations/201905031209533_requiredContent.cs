namespace ProjectTrackingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "Content", c => c.String());
        }
    }
}
