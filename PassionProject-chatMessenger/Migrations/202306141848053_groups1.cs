namespace PassionProject_chatMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groups1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Group_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Group_Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups");
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropColumn("dbo.AspNetUsers", "Group_Id");
        }
    }
}
