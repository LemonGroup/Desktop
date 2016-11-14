using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskTop.Persons;

namespace DeskTop
{
    /// <summary>
    /// Абстрактный репозиторий. Хранит параллельно с элементами состояние (ItemState создан, обновлен, без изменений)
    /// Доступ к элементам по индексатору. 
    /// !!! При реализации сохранения сначала удалять удаленные элементы во избежание возможных конфликтов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AbstractRepo<T, TKey> where T : class
    {
        protected AbstractRepo(IEnumerable<T> items)
        {
            this.items = new SortedList<TKey, ItemConteiner>();
            foreach (T item in items)
            {
                TKey key = GetKey(item);
                this.items.Add(key, new ItemConteiner(item,ItemState.Default));
            }
            deletedKeys = new SortedSet<TKey>();
        }
        private SortedList<TKey, ItemConteiner> items;
        private SortedSet<TKey> deletedKeys;
        private IEnumerable<T> Items { get { return items.Values.Select(i=>i.Item); } }
        public T this[TKey key]
        {
            get
            {
                if (items.ContainsKey(key)) return items[key].Item;
                return null;
            }
            set
            {
                Add(key, value);
            }
        }

        public void Add(TKey key, T item)
        {
            if (!items.ContainsKey(key)) items.Add(key, new ItemConteiner(item, ItemState.Created));
            else
            {
                items.Remove(key);
                items.Add(key, new ItemConteiner(item, ItemState.Updated));
            }           
        }


        public void Delete(TKey key)
        {
            deletedKeys.Add(key);
        }
        protected abstract T Create(TKey key);
        public abstract void Save();
        protected abstract TKey GetKey(T item);
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
