using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop
{
    public class Person : IEditableObject
    {
        public Person(int id, string name)
        {
            Id = id;
            KeyWords = new List<string>();
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> KeyWords { get; private set; }
        public override string ToString() { return Name; }
        protected string oldName;
        public void BeginEdit()
        {
            oldName = Name;
        }

        public void EndEdit()
        {
            Repos.Persons.Update(this);
        }

        public void CancelEdit()
        {
            Name = oldName;
        }
    }
}
