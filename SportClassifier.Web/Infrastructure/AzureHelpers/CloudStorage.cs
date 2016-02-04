using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Infrastructure.AzureHelpers
{
    public  class CloudStorage
    {
        public static CloudStorageAccount StorageAccount
        {
            get
            {
                return new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials("lpportalvhds5rnqyvf495z0",
                  "u4siO72heIQq1twQD1XVPy1rJF7bSo2C4b0eCMjI18L8069NYiK9JnKlowPm/jIz6WmHsGwvNeGLtssaBETTKw=="), true);
            }

        }
    }
}