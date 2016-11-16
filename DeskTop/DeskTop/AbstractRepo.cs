using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public IEnumerable<T> Items { get { return items.Values.Select(i=>i.Item); } }
        public T this[TKey key]
        {
            get
            {
                if (items.ContainsKey(key)) return items[key].Item;
                return null;
            }
            set { Add(value); }
        }

        private SortedList<TKey, ItemConteiner> items;
        private SortedSet<TKey> deletedKeys;

        public void Add(T item)
        {
            TKey key = GetKey(item);
            if (!items.ContainsKey(key)) items.Add(key, new ItemConteiner(item, ItemState.Created));
            else
            {
                items.Remove(key);
                items.Add(key, new ItemConteiner(item, ItemState.Updated));
            }           
        }

        public void Update(TKey oldKey, T item)
        {
            TKey key = GetKey(item);
            if (key.Equals(oldKey)) items[key].State = ItemState.Updated; // поле с ключем не изменилось
            else // в потивном случае удаляем элемент со старым ключем и доавляем под новым
            {
                items.Remove(oldKey);
                items.Add(key, new ItemConteiner(item, ItemState.Updated));
            }
        }

        public void Delete(TKey key)
        {
            items.Remove(key);
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
