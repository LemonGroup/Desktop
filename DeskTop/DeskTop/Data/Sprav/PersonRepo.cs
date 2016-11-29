using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DeskTop.Web;


namespace DeskTop
{
    public class PersonRepo : AbstractRepo<Person>
    {
 
        public override void Save()
        {
            MessageBox.Show("Персоны сохранены (заглушка)");
        }
        
        protected override int GetKey(Person item)
        {
            return item.Id;
        }

        public override Person Create(string par)
        {
            return new Person(NextKey, par);
        }

        public PersonRepo() : base() { }

        public PersonRepo(DataLoader loader) : base(loader) { }
    }
}
