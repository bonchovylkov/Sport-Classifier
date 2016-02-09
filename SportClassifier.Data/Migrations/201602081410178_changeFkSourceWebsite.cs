namespace SportClassifier.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeFkSourceWebsite : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Sources", name: "SourceWebsite_Id", newName: "SourceWebsiteId");
            RenameIndex(table: "dbo.Sources", name: "IX_SourceWebsite_Id", newName: "IX_SourceWebsiteId");
            DropColumn("dbo.Sources", "fkWebsite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sources", "fkWebsite", c => c.Int());
            RenameIndex(table: "dbo.Sources", name: "IX_SourceWebsiteId", newName: "IX_SourceWebsite_Id");
            RenameColumn(table: "dbo.Sources", name: "SourceWebsiteId", newName: "SourceWebsite_Id");
        }
    }
}
