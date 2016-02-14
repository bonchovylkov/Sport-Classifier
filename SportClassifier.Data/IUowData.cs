using SportClassifier.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Data
{
    public interface IUowData
    {
        DbContext Context { get; }
        IRepository<User> Users { get; }

        IRepository<Category> Categories { get; }
        IRepository<Source> Sources { get; }
        IRepository<SourceWebsite> SourceWebsites { get; }
        IRepository<NewsItem> NewsItems { get; }

        IRepository<KeyType> KeyTypes { get; }
        IRepository<KeyValue> KeyValues { get; }
        IRepository<Setting> Settings { get;  }

        IRepository<FailedUrl> FailedUrls { get;  }
            IRepository<WordSource> WordSources { get;  }
        
        

        int SaveChanges();
    }
}
