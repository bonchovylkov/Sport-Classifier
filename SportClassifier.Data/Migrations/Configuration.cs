namespace SportClassifier.Data.Migrations
{
    using SportClassifier.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<SportClassifier.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SportClassifier.Data.ApplicationDbContext context)
        {

            //WordSource s = new WordSource();
            //s.CategoryId = 1;
            //s.Word = "football";
            //s.Matches = 10;
            //s.NonMatches = 2;
            //context.WordSources.Add(s);
            //context.SaveChanges();

            //            --insert into [dbo].[Categories]  (name,ParentCategoryId,ShowInMenu,MenuIndex,ShowOnHomePage,Thumbnail,Url)
            //--select name,fkParent,ShowInMenu,MenuIndex,ShowOnHomePage,Thumbnail,Url
            //--from [sportnet].[dbo].[Category]




            //insert into [SportClassifier].[dbo].[Sources] (Name, StreamUrl, LastUpdated,IsActive,CategoryId,SourceWebsiteId)
            //SELECT Name
            //      ,StreamUrl,
            //      LastUpdated,
            //      IsActive,
            //      (select Id from Categories where Id = fkCategory),
            //      fkWebsite


            //  FROM [sportnet].[dbo].[Source]

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
