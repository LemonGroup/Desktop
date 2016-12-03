using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DeskTop.Data.Sprav;
using Newtonsoft.Json;


namespace DeskTop.Web
{
    /// <summary>
    /// create update delete для справочников интерфейс web
    /// </summary>
    public class CrudSprav<T> : RestBase where T : class
    {
        private readonly Encoding _encoding = new UTF8Encoding();
        public CrudSprav(string server, string path) : base(server, path) { }
        public T Create(T item)
        {
            HttpWebRequest httpReuest = GetRequest(METHOD_POST);
            var stringData = typeof(T) == typeof(KeyWord) ? RestSerializer.KeyWordCreate(item as KeyWord) : RestSerializer.Serialize(item);
            byte[] data = _encoding.GetBytes(stringData);
            httpReuest.ContentLength = data.Length;

            Stream newStream = httpReuest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            var httpResponse = (HttpWebResponse)httpReuest.GetResponse();
            string answer = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                answer = streamReader.ReadToEnd();
            }
            return RestSerializer.Deserialize<T>(answer);
        }

        public void Update(T item)
        {
            HttpWebRequest httpReuest = GetRequest(METHOD_PUT);

            string stringData = RestSerializer.Serialize(item);
            byte[] data = _encoding.GetBytes(stringData);
            httpReuest.ContentLength = data.Length;

            Stream newStream = httpReuest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            var httpResponse = (HttpWebResponse)httpReuest.GetResponse();            
        }
        public void Delete(int id)
        {
            HttpWebRequest httpReuest = GetRequest(METHOD_DEL, id.ToString());
            var httpResponse = (HttpWebResponse)httpReuest.GetResponse();
        }
        
    }

}
