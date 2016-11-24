using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DeskTop.Web;


namespace DeskTop
{
    public class PersonRepo : AbstractRepo<Person>
    {
        protected Person Create(string key)
        {
            return new Person(key);
        }

        public override void Save()
        {
            MessageBox.Show("Персоны сохранены (заглушка)");
        }

        protected override int GetKey(Person item)
        {
            return item.Id;
        }

        public PersonRepo() : base() { }

        public PersonRepo(DataLoader loader) : base()
        {
            var data = loader.GetData<JsonPerson>();
            foreach (JsonPerson jsonPerson in data)
            {
                Person person = new Person(jsonPerson.personName);
                person.Id = jsonPerson.id;
                Add(person);
            }
        }
        private class JsonPerson
        {
            public int id;
            public string personName;

        }
    }
}
