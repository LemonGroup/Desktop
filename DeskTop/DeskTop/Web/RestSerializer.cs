using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DeskTop.Data.Sprav;
using Newtonsoft.Json;

namespace DeskTop.Web
{
    public class RestSerializer
    {

        public static string Serialize(object item)
        {
            var site = item as Site;
            if (site != null) return new JsonSite(site).ToString();
            var person = item as Person;
            if (person != null) return new JsonPerson(person).ToString();
            var keyWord = item as KeyWord;
            if (keyWord != null) return new JsonKeyWord(keyWord).ToString();
            throw new ArrayTypeMismatchException($"Невозможно сериализовать в Json {item.GetType()}");
        }

        public static T Deserialize<T>(string str) where T : class
        {
            if (typeof(T) == typeof(KeyWord))
            {
                var jkeyWord = JsonConvert.DeserializeObject<JsonKeyWord>(str);
                return (T)(object)jkeyWord.ToT();
            }
            if (typeof(T) == typeof(Site))
            {
                var jsite = JsonConvert.DeserializeObject<JsonSite>(str);
                return (T)(object)jsite.ToT();
            }
            if (typeof(T) == typeof(Person))
            {
                var jperson = JsonConvert.DeserializeObject<JsonPerson>(str);
                return (T)(object)jperson.ToT();
            }

            throw new ArrayTypeMismatchException($"Невозможно десериализовать из Json {typeof(T)}");
        }

        public static string KeyWordCreate(KeyWord kw)
        {
            return String.Format("{{\"personId\":{0},\"keyword\":\"{1}\"}}", kw.personId, kw.Word);

        }
        public static T[] DeserializeArr<T>(string str) where T : class
        {
            if (typeof(T) == typeof(KeyWord))
            {
                var jArr = JsonConvert.DeserializeObject<JsonKeyWord[]>(str);
                var arr = jArr.Select(j => (KeyWord) (object) j.ToT()).ToArray();
                return  arr as T[];
            }
            if (typeof(T) == typeof(Site))
            {
                var jArr = JsonConvert.DeserializeObject<JsonSite[]>(str);
                var arr = jArr.Select(j => (Site) (object) j.ToT()).ToArray();
                return arr as T[];
            }
            if (typeof(T) == typeof(Person))
            {
                var jArr = JsonConvert.DeserializeObject<JsonPerson[]>(str);
                var arr = jArr.Select(j => (T)(object)j.ToT()).ToArray();
                return arr as T[];
            }
            throw new ArrayTypeMismatchException($"Невозможно десериализовать из Json массив {typeof(T)}");
        }


        private abstract class JsonClass<T> where T : class
        {
            public abstract T ToT();

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }

        private class JsonPerson : JsonClass<Person>
        {
            public int id;
            public string personName;
            public JsonPerson() { }
            public JsonPerson(Person person)
            {
                id = person.Id;
                personName = person.Name;              
            }

            public override Person ToT()
            {
                return new Person(id, personName);
            }


        }

        private class JsonKeyWord : JsonClass<KeyWord>
        {
            public int id;
            public int personId;
            public string keyword;
            public JsonKeyWord() { }
            public JsonKeyWord(KeyWord kw)
            {
                id = kw.Id;
                personId = kw.personId;
                keyword = kw.Word;
            }
            public override KeyWord ToT()
            {
                return new KeyWord(id, keyword) {personId = personId};
            }
        }

        private class JsonSite : JsonClass<Site>
        {
            public int id;
            public string site;
            public Type RefType;
            public JsonSite() { }
            public JsonSite(int id, string url)
            {
                this.id = id;
                this.site = url;
            }
            public JsonSite(Site site)
            {
                id = site.Id;
                this.site = site.Url;               
            }

            public override Site ToT()
            {
                return new Site(id, site);
            }
        }
    }
}
