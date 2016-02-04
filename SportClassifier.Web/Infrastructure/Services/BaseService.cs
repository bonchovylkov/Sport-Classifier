using SportClassifier.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Services
{
    public class BaseService
    {
        protected IUowData Data { get; set; }

        public BaseService(IUowData data)
        {
            this.Data = data;
        }
    }
}