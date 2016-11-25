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

        public PersonRepo(DataLoader loader, DataLoader keyWordLoader) : base()
        {
            var data = loader.GetData<JsonClasses.JsonPerson>();
            var keyWords = keyWordLoader.GetData<JsonClasses.JsonKeyWord>();
            var kwGroups = keyWords.GroupBy(k => k.personId);
            foreach (JsonClasses.JsonPerson jsonPerson in data)
            {
                Person person = new Person(jsonPerson.personName);
                person.Id = jsonPerson.id;
                foreach (var kwGroup in kwGroups)
                {
                    if (kwGroup.Key == person.Id)
                        foreach (JsonClasses.JsonKeyWord keyWord in kwGroup)
                        {
                            person.KeyWords.Add(keyWord.keyword);
                        }
                }
                Add(person);
            }
        }
    }
}
