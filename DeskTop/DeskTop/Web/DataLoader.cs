using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeskTop.Web
{
    public class DataLoader
    {
        public string serverAdres;
        public string path;

        public DataLoader(string server, string path)
        {
            serverAdres = server;
            this.path = path;
        }

        public T[] GetData<T>()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(serverAdres + path);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Auth-Token", "this-is-fake-token");

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string answer = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                answer = streamReader.ReadToEnd();
            }
            var deserializedArr = JsonConvert.DeserializeObject<T[]>(answer);
            return deserializedArr;

        }
    }
}
