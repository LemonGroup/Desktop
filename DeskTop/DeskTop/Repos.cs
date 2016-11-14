using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskTop.Persons;

namespace DeskTop
{
    public static class Repos
    {
        public static PersonRepo Persons { get; } = new PersonRepo(new[] {new Person("default"),});
    }
}
