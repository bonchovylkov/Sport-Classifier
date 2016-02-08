using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Models
{
    public class Source : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreamUrl { get; set; }
        public DateTime? LastUpdated { get; set; }


        public bool IsActive { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int? SourceWebsiteId { get; set; }
        public virtual SourceWebsite SourceWebsite { get; set; }
    }
}
