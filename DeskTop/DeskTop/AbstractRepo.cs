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
        protected AbstractRepo()
        {
            items = new SortedList<TKey, ItemConteiner>();
            deletedKeys = new SortedSet<TKey>();
        }
        private SortedList<TKey, ItemConteiner> items;
        private SortedSet<TKey> deletedKeys;
        private IEnumerable<T> Items { get { return items.Values.Select(i=>i.Item); } }
        protected abstract TKey GetKey(T obj);
        public T this[TKey key]
        {
            get
            {
                if (items.ContainsKey(key)) return items[key].Item;
                return null;
            }
            set
            {
                if (!items.ContainsKey(key)) items.Add(key, new ItemConteiner(value, ItemState.Created));
                else
                {
                    items.Remove(key);
                    items.Add(key, new ItemConteiner(value, ItemState.Updated));
                }
            }
        }

        protected abstract T Create(TKey key);
        public abstract void Save();

        public void Delete(TKey key)
        {
            deletedKeys.Add(key);
        }
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
