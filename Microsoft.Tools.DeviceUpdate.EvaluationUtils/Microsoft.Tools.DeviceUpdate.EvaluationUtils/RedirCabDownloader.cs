using System;
using System.IO;
using System.Xml;
using Microsoft.WindowsPhone.ImageUpdate.Tools;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x0200000C RID: 12
	public class RedirCabDownloader
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004EE0 File Offset: 0x000030E0
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004EE8 File Offset: 0x000030E8
		public string ClientWebServiceUrl { get; private set; }

		// Token: 0x060000BA RID: 186 RVA: 0x00004EF4 File Offset: 0x000030F4
		public void DownloadRedirCab(string[] urls)
		{
			foreach (string url in urls)
			{
				try
				{
					this.DownloadRedirCab(url);
					return;
				}
				catch
				{
				}
			}
			throw new Exception("Unable to download Redir cab");
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004F3C File Offset: 0x0000313C
		private void DownloadRedirCab(string url)
		{
			string filename = new HttpDownloader().DownloadToTempPath(url);
			string tempPath = Path.GetTempPath();
			string filename2 = Path.Combine(tempPath, "wuredir.xml");
			CabApiWrapper.ExtractOne(filename, tempPath, "wuredir.xml");
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			xmlNamespaceManager.AddNamespace("wuredir", "http://schemas.microsoft.com/msus/2002/12/wuredir");
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename2);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("wuredir:WuRedir/wuredir:Protocol", xmlNamespaceManager);
			this.ClientWebServiceUrl = xmlNode.Attributes["clientServerUrl"].Value;
		}

		// Token: 0x0400004A RID: 74
		private const string WuRedirXml = "wuredir.xml";
	}
}
