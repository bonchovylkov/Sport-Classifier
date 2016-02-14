using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using SportClassifier.Models;

namespace SportClassifier.Data
{
    public class UowData : IUowData
    {
        private readonly DbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UowData()
            : this(new ApplicationDbContext())
        {
        }

        public UowData(DbContext context)
        {
            this.context = context;
        }

        DbContext IUowData.Context
        {
            get { return this.context; }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }


        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }




        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IRepository<Source> Sources
        {
            get { return this.GetRepository<Source>(); }
        }

        public IRepository<SourceWebsite> SourceWebsites
        {
            get { return this.GetRepository<SourceWebsite>(); }
        }

        public IRepository<NewsItem> NewsItems
        {
            get { return this.GetRepository<NewsItem>(); }
        }


        public IRepository<KeyType> KeyTypes
        {
          get { return this.GetRepository<KeyType>(); }
          
        }

        public IRepository<KeyValue> KeyValues
        {
            get { return this.GetRepository<KeyValue>(); }
          
        }

        public IRepository<Setting> Settings
        {
           get { return this.GetRepository<Setting>(); }
            
        }


        public IRepository<FailedUrl> FailedUrls
        {
            get { return this.GetRepository<FailedUrl>(); }
        }


        public IRepository<WordSource> WordSources
        {
             get { return this.GetRepository<WordSource>(); }
        }
    }
}
