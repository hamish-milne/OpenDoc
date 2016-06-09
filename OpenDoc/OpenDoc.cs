using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace OpenDoc
{
	public class OpenDoc
	{
		public string Path { get; set; }

	    private readonly ZipFile zipFile;

		public OpenDoc(Stream stream)
		{
			zipFile = ZipFile.Read(stream);
		}

		public OpenDoc(string path) : this(File.OpenRead(path))
		{
			Path = path;
		}


    }
}
