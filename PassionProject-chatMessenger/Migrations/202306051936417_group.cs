namespace PassionProject_chatMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class group : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                {
                    GroupId = c.Int(nullable: false, identity: true),
                    GroupName = c.String(),
                })
                .PrimaryKey(t => t.GroupId);

        }

        public override void Down()
        {
        }
    }
}
