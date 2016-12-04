using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DeskTop.Annotations;

namespace DeskTop
{
    public class Site : IEditableObject, INotifyPropertyChanged
    {
        public int Id { get; private set; } // получается либо из базы либо 

        public string Url
        {
            get { return _url; }
            set
            {
                _url = value; 
                OnPropertyChanged(nameof(Url));
            }
        }

        private string oldUrl;
        private string _url;

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
