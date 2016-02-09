namespace SportClassifier.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "NewsItem_Id", "dbo.NewsItems");
            DropIndex("dbo.Categories", new[] { "NewsItem_Id" });
            CreateTable(
                "dbo.NewsItemCategories",
                c => new
                    {
                        NewsItem_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NewsItem_Id, t.Category_Id })
                .ForeignKey("dbo.NewsItems", t => t.NewsItem_Id, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.NewsItem_Id)
                .Index(t => t.Category_Id);
            
            DropColumn("dbo.Categories", "NewsItem_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "NewsItem_Id", c => c.Int());
            DropForeignKey("dbo.NewsItemCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.NewsItemCategories", "NewsItem_Id", "dbo.NewsItems");
            DropIndex("dbo.NewsItemCategories", new[] { "Category_Id" });
            DropIndex("dbo.NewsItemCategories", new[] { "NewsItem_Id" });
            DropTable("dbo.NewsItemCategories");
            CreateIndex("dbo.Categories", "NewsItem_Id");
            AddForeignKey("dbo.Categories", "NewsItem_Id", "dbo.NewsItems", "Id");
        }
    }
}
