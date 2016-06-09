using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace OpenDoc
{
	public static class AttributeParser<T> where T : struct
	{
		private static readonly Dictionary<T, string> forward = new Dictionary<T, string>();
		private static readonly Dictionary<string, T> reverse = new Dictionary<string, T>();

		static AttributeParser()
		{
			var names = Enum.GetNames(typeof(T));
			var values = (T[])Enum.GetValues(typeof(T));
			for (int j = 0; j < names.Length; j++)
			{
				var name = names[j];
				var sb = new StringBuilder();
				sb.Append(char.ToLower(name[0]));
				for (int i = 1; i < name.Length; i++)
				{
					var c = name[i];
					if (char.IsUpper(c))
						sb.Append('-').Append(char.ToLower(c));
					else
						sb.Append(c);
				}
				var value = values[j];
				var str = sb.ToString();
				forward[value] = str;
				reverse[str] = value;
			}
		}

		public static string GetString(T? value)
		{
			if (value == null) return null;
			string ret;
			forward.TryGetValue(value.Value, out ret);
			return ret;
		}

		public static T? GetValue(string str)
		{
			T ret;
			if (str != null && reverse.TryGetValue(str, out ret))
				return ret;
			return null;
		}
	}

	public class OpenDocElement
	{
		public XElement Element { get; }

		public static T Create<T>(XElement parent) where T : OpenDocElement
		{
			var element = new XElement(XNameAttribute.Get(typeof(T), parent));
			parent.Add(element);
			return (T) Activator.CreateInstance(typeof (T), element);
		}

		public OpenDocElement(XElement element)
		{
			Element = element;
		}

		public XName GetName(string prefix, string localName)
		{
			return XName.Get(localName, Element.GetNamespaceOfPrefix(prefix).NamespaceName);
		}

		public XElement GetOrAddElement(XName name)
		{
			var ret = Element.Element(name);
			if (ret == null)
				Element.Add(ret = new XElement(name));
			return ret;
		}

		protected T? GetAttributeValue<T>(XName name) where T : struct
		{
			return AttributeParser<T>.GetValue(Element.Attribute(name)?.Value);
		}

		protected void SetAttributeValue<T>(XName name, T? value) where T : struct
		{
			Element.SetAttributeValue(name, AttributeParser<T>.GetString(value));
		}

		protected int? GetAttributeInt(XName name)
		{
			int ret;
			if (int.TryParse(Element.Attribute(name)?.Value, out ret))
				return ret;
			return null;
		}

		protected void SetAttributeInt(XName name, int? value)
		{
			Element.SetAttributeValue(name, value.HasValue ? value.ToString() : null);
		}
	}
}
