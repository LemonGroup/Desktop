using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskTop.Persons;
using DeskTop.Sites;

namespace DeskTop
{
    public static class Repos
    {
        public static PersonRepo Persons { get; } = new PersonRepo(new[] {new Person("default"),});
        public static SitesRepo Sites { get; } = new SitesRepo(new Site[0]);

    }
}
