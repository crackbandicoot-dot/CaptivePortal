using Core.POCOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core
{
    public class CaptivePortalServer : AsyncHttpServerBase
    {
        public CaptivePortalServer(EndPoint endPoint) : base(endPoint)
        {
        }

        protected override async Task<HttpResponse> ProccesRequest(HttpRequest httpRequest)
        {
            throw new NotImplementedException();
        }
    }
}
