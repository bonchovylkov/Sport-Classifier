using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Models
{
    public class KeyType : IEntity
    {

        public KeyType()
        {
            this.KeyValues = new HashSet<KeyValue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string KeyTypeIntCode { get; set; }

        public virtual ICollection<KeyValue> KeyValues { get; set; }

    }
}
