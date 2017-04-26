//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ICSharpCode.SharpZipLib.Core;
//using ICSharpCode.SharpZipLib.Zip;
//using Voodoo;

//namespace Voodoo.CodeGeneration.Helpers
//{
//	public static class ZipHelper
//	{
//		public static void ExtractZipFile(string archiveFilenameIn, string outFolder)
//		{
//			var fileStream = File.OpenRead(archiveFilenameIn);
//			ExtractZipFile(fileStream, outFolder);
//		}

//		public static void ExtractZipFile(Stream stream, string outFolder)
//		{
//			ZipFile zipFile = null;
//			try
//			{

//				zipFile = new ZipFile(stream);
//				foreach (ZipEntry zipEntry in zipFile)
//				{
//					if (!zipEntry.IsFile)
//					{
//						continue;
//					}
//					var entryFileName = zipEntry.Name;
//					var buffer = new byte[4096]; // 4K is optimum
//					var zipStream = zipFile.GetInputStream(zipEntry);

//					var fullZipToPath = IoNic.PathCombineLocal(outFolder, entryFileName);
//					var directoryName = Path.GetDirectoryName(fullZipToPath);
//					if (directoryName.Length > 0)
//						Directory.CreateDirectory(directoryName);

//					using (var streamWriter = File.Create(fullZipToPath))
//					{
//						StreamUtils.Copy(zipStream, streamWriter, buffer);
//					}
//				}
//			}
//			finally
//			{
//				if (zipFile != null)
//				{
//					zipFile.IsStreamOwner = true;
//					zipFile.Close();
//				}
//			}
//		}
//	}
//}

