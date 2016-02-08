using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Models
{
    public class SourceWebsite : IEntity
    {
        public SourceWebsite()
        {
            this.Sources = new HashSet<Source>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Source> Sources { get; set; }
    }
}
