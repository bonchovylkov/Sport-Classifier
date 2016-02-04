using SportClassifier.Data;
using SportClassifier.Models;
using SportClassifier.Web.Infrastructure.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Services
{
    public class KeyTypeKeyValueService : BaseService, IKeyTypeKeyValueService
    {
        public KeyTypeKeyValueService(IUowData data) : base(data)
        {

        }

        //public KeyValue GetKeyValueByIntCode(string code)
        //{
        //   var kv = this.Data.KeyValues.All().FirstOrDefault(s => s.KeyValueIntCode == code);
        //    return kv;
        //}

    }
}