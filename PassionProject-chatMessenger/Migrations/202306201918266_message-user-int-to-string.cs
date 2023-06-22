namespace PassionProject_chatMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messageuserinttostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "user1", c => c.String());
            AlterColumn("dbo.Messages", "user2", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "user2", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "user1", c => c.Int(nullable: false));
        }
    }
}
