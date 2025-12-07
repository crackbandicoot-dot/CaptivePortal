using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Request
    {
        public Request(HttpRequest httpRequest, string iP)
        {
            HttpRequest = httpRequest;
            IP = iP;
        }

        public HttpRequest HttpRequest { get; set; }
       public string IP {  get; set; }
    }
}
