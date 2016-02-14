using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using SportClassifier.Models;

namespace SportClassifier.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Source> Sources { get; set; }
        public IDbSet<SourceWebsite> SourceWebsites { get; set; }
        public IDbSet<NewsItem> NewsItems { get; set; }

        public IDbSet<KeyType> KeyTypes { get; set; }

        public IDbSet<KeyValue> KeyValues { get; set; }
        public IDbSet<Setting> Settings { get; set; }
        public IDbSet<FailedUrl> FailedUrls { get; set; }
        public IDbSet<WordSource> WordSources { get; set; }
        

    }
}
