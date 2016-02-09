namespace SportClassifier.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSourceCategoryFK : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Sources", name: "Category_Id", newName: "CategoryId");
            RenameIndex(table: "dbo.Sources", name: "IX_Category_Id", newName: "IX_CategoryId");
            CreateTable(
                "dbo.FailedUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Exception = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FailedUrls");
            RenameIndex(table: "dbo.Sources", name: "IX_CategoryId", newName: "IX_Category_Id");
            RenameColumn(table: "dbo.Sources", name: "CategoryId", newName: "Category_Id");
        }
    }
}
