using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DeskTop.Web;


namespace DeskTop
{
    /// <summary>
    /// Абстрактный репозиторий. Хранит параллельно с элементами состояние (ItemState создан, обновлен, без изменений)
    /// Доступ к элементам по индексатору. 
    /// Изменения передаются в БД отложенно в методе save()
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public abstract class AbstractRepo<T> where T : class
    {
        private SortedList<int, ItemConteiner> items;
        private int lasint; // счетчик id для создаваемых элементов, растет в сторону уменьшения
        //(реальный id появится в БД)
        private CrudSprav<T> crud;
        protected int NextKey {get { return lasint - 1; } }
        protected AbstractRepo()
        {
            this.items = new SortedList<int, ItemConteiner>();
            lasint = -1;
        }
        protected AbstractRepo(DataLoader loader) : this()
        {
            crud = new CrudSprav<T>(loader.serverAdres, loader.path);
            var data = RestSerializer.DeserializeArr<T>(loader.GetData());
            foreach (var item in data)
                Add(item);
        }
        public IEnumerable<T> Items { get { return items.Values.Where(i=>i.State != ItemState.Deleted).Select(i=>i.Item); } }
        public T this[int key]
        {
            get
            {
                if (items.ContainsKey(key) && items[key].State != ItemState.Deleted) return items[key].Item;
                return null;
            }
            set { Add(value); }
        }


        public void Add(T item)
        {
            int key = GetKey(item);
            if (!items.ContainsKey(key)) items.Add(key, new ItemConteiner(item, ItemState.Created));
            else
            {
                items.Remove(key);
                items.Add(key, new ItemConteiner(item, ItemState.Updated));
            }           
        }

        public void Update(T item)
        {
            int key = GetKey(item);
            items[key].State = ItemState.Updated;
        }

        public void Delete(int key)
        {
            items[key].State = ItemState.Deleted;
        }

        public void Load(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                int key = GetKey(item);
                this.items.Add(key, new ItemConteiner(item, ItemState.Default));
            }
        }

        private IEnumerable<T> GetItems(ItemState state)
        {
            return items.Values.Where(i => i.State == state).Select(i => i.Item);
        }
        public void Save()
        {
            foreach (var item in GetItems(ItemState.Deleted))
                crud.Delete(GetKey(item));
            foreach (var item in GetItems(ItemState.Updated))
                crud.Update(item);
            foreach (var item in GetItems(ItemState.Created))
                crud.Create(item);
        }
        protected abstract int GetKey(T item);
        public abstract T Create(string par);
        private enum ItemState
        {
            Default, Created, Updated, Deleted
        }
        private class ItemConteiner
        {
            public T Item;
            public ItemState State;

            public ItemConteiner(T item, ItemState state)
            {
                Item = item;
                State = state;
            }
        }
    }
}
