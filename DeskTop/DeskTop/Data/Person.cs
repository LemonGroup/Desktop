﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop
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
        public override string ToString() { return Name; }
    }
}
