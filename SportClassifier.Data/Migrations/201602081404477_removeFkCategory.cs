namespace SportClassifier.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeFkCategory : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sources", "fkCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sources", "fkCategory", c => c.Int(nullable: false));
        }
    }
}
