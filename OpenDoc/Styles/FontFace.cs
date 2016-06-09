using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace OpenDoc.Styles
{

	public class Panose
	{
		
	}

	public enum FontFamilyGeneric
	{
		Roman,
		Swiss,
		Modern,
		Decorative,
		Script,
		System,
	}

	public enum FontPitch
	{
		Fixed,
		Variable,
	}

	public enum FontStyle
	{
		Normal,
		Italic,
		Oblique,
	}

	public enum FontVariant
	{
		Normal,
		SmallCaps,
	}

	public enum FontStretch
	{
		Normal,
		UltraCondensed,
		ExtraCondensed,
		Condensed,
		SemiCondensed,
		SemiExpanded,
		Expanded,
		ExtraExpanded,
		UltraExpanded,
	}

	public enum FontWeight
	{
		Normal,
		Bold,
		N100 = 100,
		N200 = 200,
		N300 = 300,
		N400 = 400,
		N500 = 500,
		N600 = 600,
		N700 = 700,
		N800 = 800,
		N900 = 900,
	}

	[XName("style", "font-face")]
	public class FontFace : OpenDocElement
	{

		private readonly XName nName, nFamily, nFamilyGeneric, nPitch, nPanose;

		public FontFace(XElement element) : base(element)
		{
			nName = GetName("style", "name");
			nFamily = GetName("svg", "font-family");
			nFamilyGeneric = GetName("style", "font-family-generic");
			nPitch = GetName("style", "font-pitch");
			nPanose = GetName("svg", "panose-1");
		}

		public string Name
		{
			get { return Element.Attribute(nName)?.Value; }
			set { Element.SetAttributeValue(nName, value); }
		}

		public string Family
		{
			get { return Element.Attribute(nFamily)?.Value; }
			set { Element.SetAttributeValue(nFamily, value); }
		}

		public FontFamilyGeneric? FamilyGeneric
		{
			get { return GetAttributeValue<FontFamilyGeneric>(nFamilyGeneric); }
			set { SetAttributeValue(nFamilyGeneric, value); }
		}

		public FontPitch? Pitch
		{
			get { return GetAttributeValue<FontPitch>(nPitch); }
			set { SetAttributeValue(nPitch, value); }
		}
	}
}
