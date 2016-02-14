using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Services.Contracts
{
    public interface ICrowlingService
    {
        int Crow(bool? IsForTest=null);
    }
}