using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Models
{
    public class KeyValue : IEntity
    {
                public int Id { get; set; }
        public string Name { get; set; }
        public string KeyValueIntCode { get; set; }
        public string Value { get; set; }
        public string DefaultValue1 { get; set; }
        public string DefaultValue2 { get; set; }
        public string DefaultValue3 { get; set; }
        public int KeyTypeId { get; set; }
        public virtual KeyType KeyType { get; set; }
    }
}
