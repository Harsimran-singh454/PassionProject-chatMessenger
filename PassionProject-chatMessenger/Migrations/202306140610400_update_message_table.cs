namespace PassionProject_chatMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_message_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "user1", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "user2", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "user2");
            DropColumn("dbo.Messages", "user1");
        }
    }
}
