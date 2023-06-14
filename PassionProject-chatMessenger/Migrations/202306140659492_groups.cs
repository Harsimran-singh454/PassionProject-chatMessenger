namespace PassionProject_chatMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GroupName = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
        }
    }
}

