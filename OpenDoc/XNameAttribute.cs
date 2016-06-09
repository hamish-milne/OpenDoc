using System;
using System.Linq;
using System.Xml.Linq;

namespace OpenDoc
{
	[AttributeUsage(AttributeTargets.Class)]
	public class XNameAttribute : Attribute
	{
		public string Prefix { get; set; }
		public string LocalName { get; set; }

		public XNameAttribute(string prefix, string localName)
		{
			Prefix = prefix;
			LocalName = localName;
		}

		public static XName Get(Type t, XElement nsProvider)
		{
			var attr = (XNameAttribute) GetCustomAttribute(t, typeof (XNameAttribute));
			if (attr == null)
				return null;
			return XName.Get(attr.LocalName, nsProvider.GetNamespaceOfPrefix(attr.Prefix).NamespaceName);
		}
	}
}
