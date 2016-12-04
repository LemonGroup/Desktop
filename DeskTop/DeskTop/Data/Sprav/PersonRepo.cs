using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DeskTop.Data.Sprav;
using DeskTop.Web;


namespace DeskTop
{
    public class PersonRepo : AbstractRepo<Person>
    {

        protected override int GetKey(Person item)
        {
            return item.Id;
        }

        public override Person Create(string par)
        {
            return new Person(NextKey, par);
        }

        public PersonRepo() : base() { }
        protected override void SaveCreated()
        {
            foreach (var item in GetItems(ItemState.Created))
            {
                crud.Create(item);
                items[GetKey(item)].State = ItemState.Default;
                foreach (KeyWord word in item.KeyWords) // обновляем person id для кл слов созданных персон
                    word.personId = item.Id;
            }
        }

        public void KeywordsToPersons()
        {
            foreach (var person in Items)
            {
                var keywords = Repos.KeyWords.Items.Where(i => i.personId == person.Id);
                person.KeyWords.AddRange(keywords);
            }            
        }
        public PersonRepo(DataLoader loader) : base(loader)
        {
            KeywordsToPersons();
        }
    }
}
