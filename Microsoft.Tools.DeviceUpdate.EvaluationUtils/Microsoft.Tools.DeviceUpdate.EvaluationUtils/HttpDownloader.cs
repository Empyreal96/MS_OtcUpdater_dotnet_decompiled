using System;
using System.IO;
using System.Net;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x02000004 RID: 4
	public class HttpDownloader : IDownloader
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003671 File Offset: 0x00001871
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003679 File Offset: 0x00001879
		public string DownloadPath { get; set; }

		// Token: 0x06000068 RID: 104 RVA: 0x0000368C File Offset: 0x0000188C
		public string DownloadToTempPath(string url)
		{
			string tempFileName = Path.GetTempFileName();
			if (File.Exists(tempFileName))
			{
				File.Delete(tempFileName);
			}
			string result;
			try
			{
				using (WebResponse response = WebRequest.Create(url).GetResponse())
				{
					using (FileStream fileStream = File.Create(tempFileName))
					{
						response.GetResponseStream().CopyTo(fileStream);
					}
				}
				result = tempFileName;
			}
			catch
			{
				if (File.Exists(tempFileName))
				{
					File.Delete(tempFileName);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003724 File Offset: 0x00001924
		public string Download(string url, string relativePath)
		{
			if (string.IsNullOrEmpty(this.DownloadPath))
			{
				throw new Exception("HttpDownloader: DownloadPath must be set before calling Download");
			}
			string text = Path.Combine(this.DownloadPath, relativePath);
			Directory.CreateDirectory(Path.GetDirectoryName(text));
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			string text2 = this.DownloadToTempPath(url);
			string result;
			try
			{
				File.Move(text2, text);
				result = text;
			}
			catch
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				if (File.Exists(text2))
				{
					File.Delete(text2);
				}
				throw;
			}
			return result;
		}
	}
}
