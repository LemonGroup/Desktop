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
    public class DataLoader : RestBase
    {
        public DataLoader(string server, string path) : base(server, path) { }


        public async Task<string> GetData(string getStr = "")
        {
            var httpWebRequest = GetRequest(METHOD_GET, getStr);

            var httpResponse = (HttpWebResponse) await httpWebRequest.GetResponseAsync();
            string answer = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                answer = streamReader.ReadToEnd();
            }
            return answer;

        }
    }
}
