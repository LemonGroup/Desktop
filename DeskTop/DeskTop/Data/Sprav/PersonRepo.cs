using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace DeskTop
{
    public class PersonRepo : AbstractRepo<Person, string>
    {
        protected Person Create(string key)
        {
            return new Person(key);
        }

        public override void Save()
        {
            MessageBox.Show("Персоны сохранены (заглушка)");
        }

        protected override string GetKey(Person item)
        {
            return item.Name;
        }

        public PersonRepo() : base()
        {
        }
    }
}
