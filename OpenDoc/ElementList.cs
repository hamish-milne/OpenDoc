using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace OpenDoc
{
	public class ElementList<T> : OpenDocElement, ICollection<T> where T : OpenDocElement
	{
		public ElementList(XElement element) : base(element)
		{
		}

		private readonly Dictionary<XElement, WeakReference> cache = new Dictionary<XElement, WeakReference>();

		private T GetElementObject(XElement element)
		{
			WeakReference reference;
			cache.TryGetValue(element, out reference);
			var ret = (T) reference?.Target;
			if (ret == null)
			{
				ret = (T)Activator.CreateInstance(typeof (T), element);
				cache[element] = new WeakReference(ret);
			}
			return ret;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Element.Elements().Select(GetElementObject).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			if(item == null)
				throw new ArgumentNullException(nameof(item));
		}

		public void Clear()
		{
			cache.Clear();
			foreach(var e in Element.Elements())
				e.Remove();
		}

		public bool Contains(T item)
		{
			if (item == null) return false;
			return item.Element.Parent == Element;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			int idx = arrayIndex;
			foreach (var obj in Element.Elements().Select(GetElementObject))
			{
				if (idx >= array.Length) return;
				array[idx++] = obj;
			}
		}

		public bool Remove(T item)
		{
			if (item == null) return false;
			cache.Remove(item.Element);
			if (item.Element.Parent == Element)
			{
				item.Element.Remove();
				return true;
			}
			return false;
		}

		public int Count => Element.Elements().Count();
		public bool IsReadOnly => false;
	}
}
