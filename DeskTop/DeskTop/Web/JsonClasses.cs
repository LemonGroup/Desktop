using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Web
{
    class JsonClasses
    {
        public class JsonPerson
        {
            public int id;
            public string personName;
        }

        public class JsonKeyWord
        {
            public int id;
            public int personId;
            public string keyword;

        }

        public class JsonSite
        {
            public int id;
            public string site;

        }
    }
}
