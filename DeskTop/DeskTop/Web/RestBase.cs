using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Web
{
    public abstract class RestBase
    {
        private string serverAdres;
        private string path;

        public const string METHOD_GET = "GET";
        public const string METHOD_POST = "POST";
        public const string METHOD_PUT = "PUT";

        public RestBase(string server, string path)
        {
            serverAdres = server;
            this.path = path;
        }

        public HttpWebRequest GetRequest(string method)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(serverAdres + path);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = method;
            httpWebRequest.Headers.Add("Auth-Token", "this-is-fake-token");
            return httpWebRequest;
        }
    }
}
