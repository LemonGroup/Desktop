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
    /// !!! При реализации сохранения сначала удалять удаленные элементы во избежание возможных конфликтов
    /// Изменения передаются в БД отложенно в методе save()
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public abstract class AbstractRepo<T> where T : class
    {
        private SortedList<int, ItemConteiner> items;
        private SortedSet<int> deletedKeys;
        private int lasint; // счетчик id для создаваемых элементов, растет в сторону уменьшения
        //(реальный id появится в БД)
        protected int NextKey {get { return lasint - 1; } }
        protected AbstractRepo()
        {
            this.items = new SortedList<int, ItemConteiner>();
            deletedKeys = new SortedSet<int>();
            lasint = -1;
        }
        protected AbstractRepo(DataLoader loader) : this()
        {
            var data = RestSerializer.DeserializeArr<T>(loader.GetData());
            foreach (var item in data)
                Add(item);
        }
        public IEnumerable<T> Items { get { return items.Values.Select(i=>i.Item); } }
        public T this[int key]
        {
            get
            {
                if (items.ContainsKey(key)) return items[key].Item;
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

        public void Update(int oldKey, T item)
        {
            int key = GetKey(item);
            if (key.Equals(oldKey)) items[key].State = ItemState.Updated; // поле с ключем не изменилось
            else // в потивном случае удаляем элемент со старым ключем и доавляем под новым
            {
                items.Remove(oldKey);
                items.Add(key, new ItemConteiner(item, ItemState.Updated));
            }
        }

        public void Delete(int key)
        {
            items.Remove(key);
            deletedKeys.Add(key);
        }

        public void Load(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                int key = GetKey(item);
                this.items.Add(key, new ItemConteiner(item, ItemState.Default));
            }
        }
        public abstract void Save();
        protected abstract int GetKey(T item);
        public abstract T Create(string par);
        private enum ItemState
        {
            Default, Created, Updated
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
