namespace PassionProject_chatMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Messages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
              "dbo.Message",
              c => new
              {
                  Id = c.String(nullable: false, maxLength: 128),
                  Message = c.String(),
                  user1_id = c.String(),
                  user2_id = c.String(),
                  group_id = c.String(),

              })
              .PrimaryKey(t => t.Id);
             }
        
        public override void Down()
        {
        }
    }
}
