using System;
using System.IO;
using System.Xml;
using Microsoft.WindowsPhone.ImageUpdate.Tools;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x0200000D RID: 13
	public class SLSDownloader
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004FBF File Offset: 0x000031BF
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004FC7 File Offset: 0x000031C7
		public string ServiceID { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004FD0 File Offset: 0x000031D0
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004FD8 File Offset: 0x000031D8
		public string Arch { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004FE1 File Offset: 0x000031E1
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004FE9 File Offset: 0x000031E9
		public string OSVersion { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004FF2 File Offset: 0x000031F2
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004FFA File Offset: 0x000031FA
		public string ServicePack { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005003 File Offset: 0x00003203
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000500B File Offset: 0x0000320B
		public string ClientHash { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005014 File Offset: 0x00003214
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000501C File Offset: 0x0000321C
		public string Language { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005025 File Offset: 0x00003225
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000502D File Offset: 0x0000322D
		public string ProgramName { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005036 File Offset: 0x00003236
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000503E File Offset: 0x0000323E
		public string ProductType { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005047 File Offset: 0x00003247
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000504F File Offset: 0x0000324F
		public string WUAVersion { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005058 File Offset: 0x00003258
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00005060 File Offset: 0x00003260
		public string ClientWebServiceUrl { get; private set; }

		// Token: 0x060000D1 RID: 209 RVA: 0x0000506C File Offset: 0x0000326C
		public void DownloadEnvironmentCab()
		{
			HttpDownloader httpDownloader = new HttpDownloader();
			string url = string.Format("HTTPS://sls.update.microsoft.com/SLS/{0}/{1}/{2}/{3}?CH={4}&L={5}&P={6}&PT={7}&WUA={8}", new object[]
			{
				this.ServiceID,
				this.Arch,
				this.OSVersion,
				this.ServicePack,
				this.ClientHash,
				this.Language,
				this.ProgramName,
				this.ProductType,
				this.WUAVersion
			});
			string filename = httpDownloader.DownloadToTempPath(url);
			string tempPath = Path.GetTempPath();
			string filename2 = Path.Combine(tempPath, "environment.cab");
			string filename3 = Path.Combine(tempPath, "environment.xml");
			CabApiWrapper.ExtractOne(filename, tempPath, "environment.cab");
			CabApiWrapper.ExtractOne(filename2, tempPath, "environment.xml");
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			xmlNamespaceManager.AddNamespace("wuredir", "http://schemas.microsoft.com/msus/2002/12/wuredir");
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename3);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("ServiceEnvironment/WUClientData/Protocol", xmlNamespaceManager);
			this.ClientWebServiceUrl = xmlNode.Attributes["clientServerUrl"].Value;
		}

		// Token: 0x0400004C RID: 76
		private const string EnvironmentCab = "environment.cab";

		// Token: 0x0400004D RID: 77
		private const string EnvironmentXml = "environment.xml";
	}
}
