using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x0200000F RID: 15
	public class UpdateScannerUUP
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000ED RID: 237 RVA: 0x00006420 File Offset: 0x00004620
		// (remove) Token: 0x060000EE RID: 238 RVA: 0x00006458 File Offset: 0x00004658
		public event UpdateScannerUUP.MessageCallback ProgressEvent;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060000EF RID: 239 RVA: 0x00006490 File Offset: 0x00004690
		// (remove) Token: 0x060000F0 RID: 240 RVA: 0x000064C8 File Offset: 0x000046C8
		public event UpdateScannerUUP.MessageCallback NormalMessageEvent;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000F1 RID: 241 RVA: 0x00006500 File Offset: 0x00004700
		// (remove) Token: 0x060000F2 RID: 242 RVA: 0x00006538 File Offset: 0x00004738
		public event UpdateScannerUUP.MessageCallback WarningMessageEvent;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000F3 RID: 243 RVA: 0x00006570 File Offset: 0x00004770
		// (remove) Token: 0x060000F4 RID: 244 RVA: 0x000065A8 File Offset: 0x000047A8
		public event UpdateScannerUUP.MessageCallback ErrorMessageEvent;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060000F5 RID: 245 RVA: 0x000065E0 File Offset: 0x000047E0
		// (remove) Token: 0x060000F6 RID: 246 RVA: 0x00006618 File Offset: 0x00004818
		public event UpdateScannerUUP.MessageCallback LogMessageEvent;

		// Token: 0x060000F8 RID: 248 RVA: 0x0000666B File Offset: 0x0000486B
		private static WebResponse GetUpdatesForDeviceFromService(HttpWebRequest request)
		{
			return request.GetResponse();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006674 File Offset: 0x00004874
		public static string GetWuServiceServiceVersion()
		{
			string result = UpdateScannerUUP.ServiceVersion;
			string environmentVariable = Environment.GetEnvironmentVariable("OTC_UUP_SERVICE_VERSION");
			if (environmentVariable != null)
			{
				result = environmentVariable;
			}
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006698 File Offset: 0x00004898
		public static string GetWuServiceRequestUri()
		{
			string arg = UpdateScannerUUP.ServiceUrl;
			string environmentVariable = Environment.GetEnvironmentVariable("OTC_INTERNAL_URL");
			if (environmentVariable != null)
			{
				arg = UpdateScannerUUP.InternalServiceUrl;
			}
			else
			{
				string environmentVariable2 = Environment.GetEnvironmentVariable("OTC_UUP_URL");
				if (environmentVariable2 != null)
				{
					arg = environmentVariable2;
				}
			}
			return string.Format("{0}/updates/search/{1}/bydeviceinfo/", arg, UpdateScannerUUP.GetWuServiceServiceVersion());
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000066E4 File Offset: 0x000048E4
		public DownloadInfo GetDownloadInfoFromHash(DownloadInfo[] downloadInfoList, string hash)
		{
			foreach (DownloadInfo downloadInfo in downloadInfoList)
			{
				if (downloadInfo.SHA256Hash.Equals(hash))
				{
					return downloadInfo;
				}
			}
			return null;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006718 File Offset: 0x00004918
		public int ProcessActionList(DownloadInfo[] downloadInfoList, string actionListPath)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.doc.NameTable);
			xmlNamespaceManager.AddNamespace("ns", "urn:schemas-microsoft-com:os-update-actionlist");
			this.doc.Load(actionListPath);
			XmlNodeList xmlNodeList = this.doc.DocumentElement.SelectSingleNode(".//ns:Downloads", xmlNamespaceManager).SelectNodes(".//ns:Package", xmlNamespaceManager);
			if (xmlNodeList == null)
			{
				throw new Exception(string.Format("Packages not found!", new object[0]));
			}
			int num = 0;
			foreach (object obj in xmlNodeList)
			{
				XmlNodeList xmlNodeList2 = ((XmlNode)obj).SelectNodes(".//ns:Payload", xmlNamespaceManager);
				if (xmlNodeList2 == null)
				{
					throw new Exception(string.Format("Payload not found!", new object[0]));
				}
				foreach (object obj2 in xmlNodeList2)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					string value = xmlNode.Attributes["Hash"].Value;
					DownloadInfo downloadInfoFromHash = this.GetDownloadInfoFromHash(downloadInfoList, value);
					if (downloadInfoFromHash == null)
					{
						throw new Exception(string.Format("{0}: hash not found in download list!", value));
					}
					string value2 = xmlNode.Attributes["SourceName"].Value;
					if (!downloadInfoFromHash.Name.Equals(value2))
					{
						downloadInfoFromHash.Name = value2;
					}
					downloadInfoFromHash.Installable = true;
					num++;
				}
			}
			return num;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000068B8 File Offset: 0x00004AB8
		private string FormatDeviceAttributes(UpdateScannerUUP.CTACData ctacData)
		{
			string text = "";
			if (!string.IsNullOrEmpty(ctacData.BranchReadinessLevel))
			{
				text += string.Format("BranchReadinessLevel={0};", ctacData.BranchReadinessLevel);
			}
			if (!string.IsNullOrEmpty(ctacData.CurrentBranch))
			{
				text += string.Format("CurrentBranch={0};", ctacData.CurrentBranch);
			}
			if (!string.IsNullOrEmpty(ctacData.DeviceFamily))
			{
				text += string.Format("DeviceFamily={0};", ctacData.DeviceFamily);
			}
			if (!string.IsNullOrEmpty(ctacData.FirmwareVersion))
			{
				text += string.Format("FirmwareVersion={0};", ctacData.FirmwareVersion);
			}
			if (!string.IsNullOrEmpty(ctacData.FlightingBranchName))
			{
				text += string.Format("FlightingBranchName={0};", ctacData.FlightingBranchName);
			}
			if (!string.IsNullOrEmpty(ctacData.FlightRing))
			{
				text += string.Format("FlightRing={0};", ctacData.FlightRing);
			}
			if (!string.IsNullOrEmpty(ctacData.InstallationType))
			{
				text += string.Format("InstallationType={0};", ctacData.InstallationType);
			}
			if (!string.IsNullOrEmpty(ctacData.InstallLanguage))
			{
				text += string.Format("InstallLanguage={0};", ctacData.InstallLanguage);
			}
			if (!string.IsNullOrEmpty(ctacData.IsDeviceRetailDemo))
			{
				text += string.Format("IsDeviceRetailDemo={0};", ctacData.IsDeviceRetailDemo);
			}
			if (!string.IsNullOrEmpty(ctacData.IsFlightingEnabled))
			{
				text += string.Format("IsFlightingEnabled={0};", ctacData.IsFlightingEnabled);
			}
			if (!string.IsNullOrEmpty(ctacData.MobileOperatorCommercialized))
			{
				text += string.Format("MobileOperatorCommercialized={0};", ctacData.MobileOperatorCommercialized);
			}
			if (!string.IsNullOrEmpty(ctacData.OEMModel))
			{
				text += string.Format("OEMModel={0};", ctacData.OEMModel);
			}
			if (!string.IsNullOrEmpty(ctacData.OEMName_Uncleaned))
			{
				text += string.Format("OEMName_Uncleaned={0};", ctacData.OEMName_Uncleaned);
			}
			if (!string.IsNullOrEmpty(ctacData.OSArchitecture))
			{
				text += string.Format("OSArchitecture={0};", ctacData.OSArchitecture);
			}
			if (!string.IsNullOrEmpty(ctacData.OSSkuId))
			{
				text += string.Format("OSSkuId={0};", ctacData.OSSkuId);
			}
			if (!string.IsNullOrEmpty(ctacData.OSUILocale))
			{
				text += string.Format("OSUILocale={0};", ctacData.OSUILocale);
			}
			if (!string.IsNullOrEmpty(ctacData.OSVersion))
			{
				text += string.Format("OSVersion={0};", ctacData.OSVersion);
			}
			if (!string.IsNullOrEmpty(ctacData.PhoneTargetingName))
			{
				text += string.Format("PhoneTargetingName={0};", ctacData.PhoneTargetingName);
			}
			if (!string.IsNullOrEmpty(ctacData.ReleaseType))
			{
				text += string.Format("ReleaseType={0};", ctacData.ReleaseType);
			}
			if (!string.IsNullOrEmpty(ctacData.TelemetryLevel))
			{
				text += string.Format("TelemetryLevel={0};", ctacData.TelemetryLevel);
			}
			if (!string.IsNullOrEmpty(ctacData.UpdateManagementGroup))
			{
				text += string.Format("UpdateManagementGroup={0};", ctacData.UpdateManagementGroup);
			}
			return text;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006BC0 File Offset: 0x00004DC0
		private string GetResponseString(Stream responseStream, string envDebugLogging)
		{
			string result = string.Empty;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (envDebugLogging != null)
				{
					this.LogMessageEvent("Copying web response stream to memory stream...");
				}
				byte[] array = new byte[4096];
				int num;
				do
				{
					num = responseStream.Read(array, 0, array.Length);
					memoryStream.Write(array, 0, num);
				}
				while (responseStream.CanRead && num > 0);
				if (envDebugLogging != null)
				{
					this.LogMessageEvent(string.Format("Web response stream length: {0}...", memoryStream.Length));
					this.LogMessageEvent("Encoding response string from memory stream...");
				}
				result = Encoding.ASCII.GetString(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006C7C File Offset: 0x00004E7C
		public DownloadInfo[] Scan(string ctacInfo, string productInfo)
		{
			UpdateScannerUUP.CTACData ctacdata = null;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(ctacInfo)))
			{
				ctacdata = (UpdateScannerUUP.CTACData)new DataContractJsonSerializer(typeof(UpdateScannerUUP.CTACData)).ReadObject(memoryStream);
			}
			string environmentVariable = Environment.GetEnvironmentVariable("OTC_FLIGHT_BRANCH");
			if (environmentVariable != null)
			{
				ctacdata.FlightingBranchName = environmentVariable;
				ctacdata.IsFlightingEnabled = "1";
			}
			string environmentVariable2 = Environment.GetEnvironmentVariable("OTC_FLIGHT_RING");
			if (environmentVariable2 != null)
			{
				ctacdata.FlightRing = environmentVariable2;
				ctacdata.IsFlightingEnabled = "1";
			}
			string text = this.FormatDeviceAttributes(ctacdata);
			if (this.LogMessageEvent != null)
			{
				this.LogMessageEvent(string.Format("ProductInfo: {0}", productInfo));
			}
			if (this.LogMessageEvent != null)
			{
				this.LogMessageEvent(string.Format("DeviceAttributes: {0}", text));
			}
			string text2 = string.Format(UpdateScannerUUP.DataFormat, productInfo, text);
			try
			{
				string environmentVariable3 = Environment.GetEnvironmentVariable("OTC_DEBUG_LOGGING");
				List<UpdateScannerUUP.UpdateMetadataForDevice> list = null;
				if (environmentVariable3 != null)
				{
					this.LogMessageEvent("Waiting for web service mutex...");
				}
				object obj = this.webServiceMutex;
				lock (obj)
				{
					if (environmentVariable3 != null)
					{
						this.LogMessageEvent("Acquired web service mutex.");
					}
					string wuServiceRequestUri = UpdateScannerUUP.GetWuServiceRequestUri();
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(wuServiceRequestUri);
					httpWebRequest.Method = "POST";
					httpWebRequest.ContentType = "application/json";
					httpWebRequest.ContentLength = (long)text2.Length;
					if (environmentVariable3 != null)
					{
						this.LogMessageEvent(string.Format("WebService URL: {0}", wuServiceRequestUri));
					}
					httpWebRequest.ContentLength = (long)text2.Length;
					if (environmentVariable3 != null)
					{
						this.LogMessageEvent(string.Format("WebService Data: {0}", text2));
					}
					using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream(), Encoding.ASCII))
					{
						streamWriter.Write(text2);
					}
					if (environmentVariable3 != null)
					{
						this.LogMessageEvent("Getting updates for device...");
					}
					using (WebResponse updatesForDeviceFromService = UpdateScannerUUP.GetUpdatesForDeviceFromService(httpWebRequest))
					{
						if (environmentVariable3 != null)
						{
							this.LogMessageEvent("Getting stream from web response...");
						}
						using (Stream responseStream = updatesForDeviceFromService.GetResponseStream())
						{
							if (responseStream != null)
							{
								if (environmentVariable3 != null)
								{
									this.LogMessageEvent("Getting stream reader for web response stream...");
								}
								if (environmentVariable3 != null)
								{
									this.LogMessageEvent("Getting string from web response stream...");
								}
								string responseString = this.GetResponseString(responseStream, environmentVariable3);
								if (environmentVariable3 != null)
								{
									this.LogMessageEvent(string.Format("WebService Response: {0}", responseString));
								}
								if (environmentVariable3 != null)
								{
									this.LogMessageEvent("Allocating memory stream for web response...");
								}
								using (MemoryStream memoryStream2 = new MemoryStream(Encoding.Unicode.GetBytes(responseString)))
								{
									XmlObjectSerializer xmlObjectSerializer = new DataContractJsonSerializer(typeof(List<UpdateScannerUUP.UpdateMetadataForDevice>));
									if (environmentVariable3 != null)
									{
										this.LogMessageEvent("Deserializing data from web response...");
									}
									list = (List<UpdateScannerUUP.UpdateMetadataForDevice>)xmlObjectSerializer.ReadObject(memoryStream2);
									goto IL_2D0;
								}
							}
							if (environmentVariable3 != null)
							{
								this.LogMessageEvent("Web stream not found!");
							}
							IL_2D0:;
						}
					}
				}
				int num = 0;
				foreach (UpdateScannerUUP.UpdateMetadataForDevice updateMetadataForDevice in list)
				{
					foreach (UpdateScannerUUP.FileLocation fileLocation in updateMetadataForDevice.FileLocations)
					{
						num++;
					}
				}
				if (num != 0)
				{
					DownloadInfo[] array = new DownloadInfo[num];
					int num2 = 0;
					foreach (UpdateScannerUUP.UpdateMetadataForDevice updateMetadataForDevice2 in list)
					{
						foreach (UpdateScannerUUP.FileLocation fileLocation2 in updateMetadataForDevice2.FileLocations)
						{
							array[num2] = new DownloadInfo();
							array[num2].Installable = false;
							array[num2].ContentType = fileLocation2.ContentType;
							array[num2].Name = fileLocation2.FileName;
							array[num2].SHA256Hash = fileLocation2.Digest;
							array[num2].Size = fileLocation2.Size;
							array[num2].Url = fileLocation2.Url.ToString();
							num2++;
						}
					}
					return array;
				}
			}
			catch (WebException arg)
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.WriteLine("Encountered web exception communicating with {0}, Exception: {1}", UpdateScannerUUP.ServiceUrl, arg);
				Console.ResetColor();
				throw;
			}
			return null;
		}

		// Token: 0x04000074 RID: 116
		public static string InternalServiceUrl = "https://bn2.cws-int.dcat.dsp.mp.microsoft.com:443/UpdateMetadataService";

		// Token: 0x04000075 RID: 117
		public static string ServiceUrl = "https://fe3.delivery.mp.microsoft.com/UpdateMetadataService";

		// Token: 0x04000076 RID: 118
		public static string ServiceVersion = "v1";

		// Token: 0x04000077 RID: 119
		public static string DataFormat = "{{\"Products\": \"{0}\", \"DeviceAttributes\": \"{1}\"}}";

		// Token: 0x04000078 RID: 120
		public static string DeviceFormat = "FlightRing={0};IsFlightingEnabled={1};ReleaseType={2}";

		// Token: 0x04000079 RID: 121
		public static string FlightingBranchFormat = "FlightingBranchName={0}";

		// Token: 0x0400007A RID: 122
		private XmlDocument doc = new XmlDocument();

		// Token: 0x0400007B RID: 123
		private object webServiceMutex = new object();

		// Token: 0x0200006C RID: 108
		public class UpdateMetadataForDevice
		{
			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600033D RID: 829 RVA: 0x00009F3C File Offset: 0x0000813C
			// (set) Token: 0x0600033E RID: 830 RVA: 0x00009F44 File Offset: 0x00008144
			public IEnumerable<Guid> UpdateIds { get; set; }

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600033F RID: 831 RVA: 0x00009F4D File Offset: 0x0000814D
			// (set) Token: 0x06000340 RID: 832 RVA: 0x00009F55 File Offset: 0x00008155
			public IEnumerable<UpdateScannerUUP.FileLocation> FileLocations { get; set; }
		}

		// Token: 0x0200006D RID: 109
		public class FileLocation
		{
			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000342 RID: 834 RVA: 0x00009F5E File Offset: 0x0000815E
			// (set) Token: 0x06000343 RID: 835 RVA: 0x00009F66 File Offset: 0x00008166
			public string FileName { get; set; }

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000344 RID: 836 RVA: 0x00009F6F File Offset: 0x0000816F
			// (set) Token: 0x06000345 RID: 837 RVA: 0x00009F77 File Offset: 0x00008177
			public long Size { get; set; }

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000346 RID: 838 RVA: 0x00009F80 File Offset: 0x00008180
			// (set) Token: 0x06000347 RID: 839 RVA: 0x00009F88 File Offset: 0x00008188
			public string Language { get; set; }

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000348 RID: 840 RVA: 0x00009F91 File Offset: 0x00008191
			// (set) Token: 0x06000349 RID: 841 RVA: 0x00009F99 File Offset: 0x00008199
			public string Digest { get; set; }

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x0600034A RID: 842 RVA: 0x00009FA2 File Offset: 0x000081A2
			// (set) Token: 0x0600034B RID: 843 RVA: 0x00009FAA File Offset: 0x000081AA
			public string ContentType { get; set; }

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x0600034C RID: 844 RVA: 0x00009FB3 File Offset: 0x000081B3
			// (set) Token: 0x0600034D RID: 845 RVA: 0x00009FBB File Offset: 0x000081BB
			public string Url { get; set; }
		}

		// Token: 0x0200006E RID: 110
		public class CTACData
		{
			// Token: 0x170000DC RID: 220
			// (get) Token: 0x0600034F RID: 847 RVA: 0x00009FC4 File Offset: 0x000081C4
			// (set) Token: 0x06000350 RID: 848 RVA: 0x00009FCC File Offset: 0x000081CC
			public string BranchReadinessLevel { get; set; }

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000351 RID: 849 RVA: 0x00009FD5 File Offset: 0x000081D5
			// (set) Token: 0x06000352 RID: 850 RVA: 0x00009FDD File Offset: 0x000081DD
			public string CurrentBranch { get; set; }

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000353 RID: 851 RVA: 0x00009FE6 File Offset: 0x000081E6
			// (set) Token: 0x06000354 RID: 852 RVA: 0x00009FEE File Offset: 0x000081EE
			public string DeviceFamily { get; set; }

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000355 RID: 853 RVA: 0x00009FF7 File Offset: 0x000081F7
			// (set) Token: 0x06000356 RID: 854 RVA: 0x00009FFF File Offset: 0x000081FF
			public string FirmwareVersion { get; set; }

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000357 RID: 855 RVA: 0x0000A008 File Offset: 0x00008208
			// (set) Token: 0x06000358 RID: 856 RVA: 0x0000A010 File Offset: 0x00008210
			public string FlightingBranchName { get; set; }

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0000A019 File Offset: 0x00008219
			// (set) Token: 0x0600035A RID: 858 RVA: 0x0000A021 File Offset: 0x00008221
			public string FlightRing { get; set; }

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x0600035B RID: 859 RVA: 0x0000A02A File Offset: 0x0000822A
			// (set) Token: 0x0600035C RID: 860 RVA: 0x0000A032 File Offset: 0x00008232
			public string InstallationType { get; set; }

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x0600035D RID: 861 RVA: 0x0000A03B File Offset: 0x0000823B
			// (set) Token: 0x0600035E RID: 862 RVA: 0x0000A043 File Offset: 0x00008243
			public string InstallLanguage { get; set; }

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600035F RID: 863 RVA: 0x0000A04C File Offset: 0x0000824C
			// (set) Token: 0x06000360 RID: 864 RVA: 0x0000A054 File Offset: 0x00008254
			public string IsDeviceRetailDemo { get; set; }

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000361 RID: 865 RVA: 0x0000A05D File Offset: 0x0000825D
			// (set) Token: 0x06000362 RID: 866 RVA: 0x0000A065 File Offset: 0x00008265
			public string IsFlightingEnabled { get; set; }

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x06000363 RID: 867 RVA: 0x0000A06E File Offset: 0x0000826E
			// (set) Token: 0x06000364 RID: 868 RVA: 0x0000A076 File Offset: 0x00008276
			public string MobileOperatorCommercialized { get; set; }

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000365 RID: 869 RVA: 0x0000A07F File Offset: 0x0000827F
			// (set) Token: 0x06000366 RID: 870 RVA: 0x0000A087 File Offset: 0x00008287
			public string OEMModel { get; set; }

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000367 RID: 871 RVA: 0x0000A090 File Offset: 0x00008290
			// (set) Token: 0x06000368 RID: 872 RVA: 0x0000A098 File Offset: 0x00008298
			public string OEMName_Uncleaned { get; set; }

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000369 RID: 873 RVA: 0x0000A0A1 File Offset: 0x000082A1
			// (set) Token: 0x0600036A RID: 874 RVA: 0x0000A0A9 File Offset: 0x000082A9
			public string OSArchitecture { get; set; }

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x0600036B RID: 875 RVA: 0x0000A0B2 File Offset: 0x000082B2
			// (set) Token: 0x0600036C RID: 876 RVA: 0x0000A0BA File Offset: 0x000082BA
			public string OSSkuId { get; set; }

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x0600036D RID: 877 RVA: 0x0000A0C3 File Offset: 0x000082C3
			// (set) Token: 0x0600036E RID: 878 RVA: 0x0000A0CB File Offset: 0x000082CB
			public string OSUILocale { get; set; }

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600036F RID: 879 RVA: 0x0000A0D4 File Offset: 0x000082D4
			// (set) Token: 0x06000370 RID: 880 RVA: 0x0000A0DC File Offset: 0x000082DC
			public string OSVersion { get; set; }

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x06000371 RID: 881 RVA: 0x0000A0E5 File Offset: 0x000082E5
			// (set) Token: 0x06000372 RID: 882 RVA: 0x0000A0ED File Offset: 0x000082ED
			public string PhoneTargetingName { get; set; }

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06000373 RID: 883 RVA: 0x0000A0F6 File Offset: 0x000082F6
			// (set) Token: 0x06000374 RID: 884 RVA: 0x0000A0FE File Offset: 0x000082FE
			public string ReleaseType { get; set; }

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000375 RID: 885 RVA: 0x0000A107 File Offset: 0x00008307
			// (set) Token: 0x06000376 RID: 886 RVA: 0x0000A10F File Offset: 0x0000830F
			public string TelemetryLevel { get; set; }

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000377 RID: 887 RVA: 0x0000A118 File Offset: 0x00008318
			// (set) Token: 0x06000378 RID: 888 RVA: 0x0000A120 File Offset: 0x00008320
			public string UpdateManagementGroup { get; set; }
		}

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x0600037B RID: 891
		public delegate void MessageCallback(string message);
	}
}
