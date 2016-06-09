using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace OpenDoc.Styles
{
	public class Styles : OpenDocElement
	{
		public ElementList<FontFace> FontFaces { get; }

		public Styles(XElement element) : base(element)
		{
			FontFaces = new ElementList<FontFace>(GetOrAddElement(GetName("office", "font-face-decls")));
		}
	}
}
