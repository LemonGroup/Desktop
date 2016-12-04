using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeskTop.Util
{
    public class Status : INotifyPropertyChanged
    {
        private int max;
        private int current;
        private string curName;
        private SynchronizationContext sync;
        public Status(int max)
        {
            sync = SynchronizationContext.Current;
            this.max = max;
        }
        #region Fields

        

        public int Max
        {
            get { return max; }
            set
            {
                if (max == value) return;
                max = value;
                OnPropertyChanged();
            }
        }

        public int Current
        {
            get { return current; }
            set
            {
                if (current == value) return;
                current = value;
                OnPropertyChanged();
            }
        }

        public string CurName
        {
            get { return curName; }
            set
            {
                if (curName == value) return;
                curName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                //sync.Post(state => PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName)), null);
                
            }
            
        }
#endregion
    }
}
