using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Models
{
    public class Category : IEntity
    {
        public Category()
        {
            this.ChildCategories = new HashSet<Category>();
            this.Sources = new HashSet<Source>();
            this.NewsItems = new HashSet<NewsItem>();
            //this.VideoChannels = new HashSet<VideoChannel>();
            //this.VideoItems = new HashSet<VideoItem>();
            //this.LiveTvItems = new HashSet<LiveTvItem>();
            //this.Galleries = new HashSet<Gallery>();
            //this.FacebookPageForCrawlings = new HashSet<FacebookPageForCrawling>();
            //this.Users = new HashSet<User>();
            //this.PredictItems = new HashSet<PredictItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool ShowInMenu { get; set; }
        public short MenuIndex { get; set; }
        public bool ShowOnHomePage { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }

        public int?  BaseCategoryId { get; set; }
         public virtual Category BaseCategory { get; set; }

        public virtual ICollection<Category> ChildCategories { get; set; }
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Source> Sources { get; set; }
        public virtual ICollection<NewsItem> NewsItems { get; set; }

        //public virtual ICollection<VideoChannel> VideoChannels { get; set; }
        //public virtual ICollection<VideoItem> VideoItems { get; set; }
        //public virtual ICollection<LiveTvItem> LiveTvItems { get; set; }
        //public virtual ICollection<Gallery> Galleries { get; set; }
        //public virtual ICollection<FacebookPageForCrawling> FacebookPageForCrawlings { get; set; }
        //public virtual ICollection<User> Users { get; set; }
        //public virtual ICollection<PredictItem> PredictItems { get; set; }
    }
}
