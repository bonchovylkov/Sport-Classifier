using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Models
{
    public class FailedUrl : IEntity
    {
        public int Id { get; set; }

        public string Url { get; set; }
        public string  Exception { get; set; }

    }
}
