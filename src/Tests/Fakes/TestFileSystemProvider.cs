using System;
using System.IO;
using System.Reflection;

namespace Fernweh.Tests.Fakes
{
	public class TestFileSystemProvider 
	{
		public string MapPath(string pathFragment)
		{
			var codeBase = Assembly.GetExecutingAssembly().CodeBase;
			var uri = new UriBuilder(codeBase);
			var path = Uri.UnescapeDataString(uri.Path);
			var partialPath= Path.GetDirectoryName(path) + @"\..\";
			return Path.Combine(partialPath, pathFragment);
		}
	}
}
