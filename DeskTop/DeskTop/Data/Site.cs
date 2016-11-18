using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Sites
{
    public class Site
    {
        public int Id { get; private set; } // получается либо из базы либо 
        public string Url { get; set; }
        public Site(int id, string url)
        {
            Id = id;
            Url = url;
        }

        public override string ToString() { return Url; }
    }
}
