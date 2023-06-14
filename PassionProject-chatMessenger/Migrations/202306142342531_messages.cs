namespace PassionProject_chatMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messages : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Messages", name: "GroupId", newName: "Id");
            RenameIndex(table: "dbo.Messages", name: "IX_GroupId", newName: "IX_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Messages", name: "IX_Id", newName: "IX_GroupId");
            RenameColumn(table: "dbo.Messages", name: "Id", newName: "GroupId");
        }
    }
}
