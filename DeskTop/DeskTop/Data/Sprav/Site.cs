using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop
{
    public class Site : IEditableObject
    {
        public int Id { get; private set; } // получается либо из базы либо 
        public string Url { get; set; }
        private string oldUrl;
        public Site(int id, string url)
        {
            Id = id;
            Url = url;
        }

        public override string ToString() { return Url; }
        public void BeginEdit()
        {
            oldUrl = Url;
        }

        public void EndEdit()
        {
            Repos.Sites.Update(this);
        }

        public void CancelEdit()
        {
            Url = oldUrl;
        }
    }
}
