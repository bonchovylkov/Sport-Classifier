using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Models
{
    public class NewsItem : IEntity
    {
        public NewsItem()
        {
            this.Categories = new HashSet<Category>();
            this.Images = new HashSet<string>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string MainPic { get; set; }
        public long DatePublished { get; set; }

        public virtual ICollection<string> Images { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public string Href { get; set; }
        public string Header { get; set; }
        public string Author { get; set; }
        public string Media { get; set; }
        public string Content { get; set; }
        public string CleanContent { get; set; }
        public string Description { get; set;}
        public bool IsPublic { get; set; }
        public bool UsedForClassication { get; set; }

    }
}
