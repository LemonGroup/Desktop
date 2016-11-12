using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskTop.Util
{
    /// <summary>
    /// Позволяет выбирать пользователю элементы коллекции типа T передаваемой при создании класса
    ///  SelectedElements - возвращает выбраные элементы 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ElementSelector<T> : IEnumerable<ElementSelector<T>.Element> where T: class 
    {
        protected List<Element> elements;

        public IEnumerable<T> SelectedElements {
            get { return elements.Where(e => e.Selected).Select(e => e.Value); } }

        public ElementSelector(IEnumerable<T> collection)
        {
            elements = new List<Element>();
            foreach (T element in collection)
                elements.Add(new Element(element));
        }

        public IEnumerator<Element> GetEnumerator()
        {
            return elements.AsEnumerable().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return elements.GetEnumerator();
        }
        public class Element
        {
            public bool Selected { get; set; }
            public T Value { get;set; }

            public Element(T element)
            {
                Selected = false;
                Value = element;
            }
        }
    }
}
