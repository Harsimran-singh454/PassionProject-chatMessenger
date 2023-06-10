namespace PassionProject_chatMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class message1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                {
                    MessageId = c.Int(nullable: false, identity: true),
                    Content = c.String(),
                    UserId = c.Int(nullable: false),
                    GroupId = c.Int(nullable: false),
                    Timestamp = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);


        }

        public override void Down()
        {
            DropForeignKey("dbo.Messages", "GroupId", "dbo.Groups");
            DropIndex("dbo.Messages", new[] { "GroupId" });
            DropTable("dbo.Groups");
            DropTable("dbo.Messages");
        }
    }
}
