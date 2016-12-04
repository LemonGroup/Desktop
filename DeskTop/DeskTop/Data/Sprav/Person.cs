using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DeskTop.Annotations;
using DeskTop.Data.Sprav;

namespace DeskTop
{
    public class Person : IEditableObject, INotifyPropertyChanged
    {
        public Person(int id, string name)
        {
            Id = id;
            KeyWords = new List<KeyWord>();
            Name = name;
        }
        public int Id { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public List<KeyWord> KeyWords { get; private set; }
        public override string ToString() { return Name; }
        private string oldName;
        private SortedList<int, KeyWord> oldKeyWords;
        private string _name;

        public void BeginEdit()
        {
            oldName = Name;
            oldKeyWords = new SortedList<int, KeyWord>();
            foreach (KeyWord word in KeyWords)
                oldKeyWords.Add(word.Id, word);
        }

        public void EndEdit()
        {
            Repos.Persons.Update(this);
            foreach (KeyWord word in KeyWords)
            {
                if (oldKeyWords.ContainsKey(word.Id)) 
                 if (oldKeyWords[word.Id].Word != word.Word) Repos.KeyWords.Update(word); // обновляем
                else Repos.KeyWords.Delete(word.Id); // удаляем
            }
        }

        public void CancelEdit()
        {
            Name = oldName;
            KeyWords = oldKeyWords.Values.ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
