using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Persons
{
    public class Person
    {
        public Person(string name)
        {
            KeyWords = new List<string>();
            Name = name;
        }

        public string Name { get; set; }
        public List<string> KeyWords { get; private set; }
    }
}
