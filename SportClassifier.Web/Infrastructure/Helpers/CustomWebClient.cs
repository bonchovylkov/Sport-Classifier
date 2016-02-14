using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Helpers
{
    public class CustomWebClient : WebClient
    {
        Uri _responseUri;

        public Uri ResponseUri
        {
            get { return _responseUri; }
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            try
            {
                WebResponse response = base.GetWebResponse(request);
                _responseUri = response.ResponseUri;
                return response;
            }
            catch (Exception ex)
            {
                return null;

            }

        }
    }
}