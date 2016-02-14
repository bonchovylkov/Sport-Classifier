using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Models
{
     public class WordSource
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Key, Column(Order = 1)]
        public string Word { get; set; }

        [Key, Column(Order = 2)]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public long Matches { get; set; }

        public long NonMatches { get; set; }
    }
}
