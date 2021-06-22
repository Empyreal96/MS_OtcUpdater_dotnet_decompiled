using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Xml;
using Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService;
using Microsoft.Tools.DeviceUpdate.EvaluationUtils.SimpleAuthWebService;
using Microsoft.WindowsPhone.ImageUpdate.Tools;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x0200000E RID: 14
	public class UpdateScanner
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060000D3 RID: 211 RVA: 0x00005174 File Offset: 0x00003374
		// (remove) Token: 0x060000D4 RID: 212 RVA: 0x000051AC File Offset: 0x000033AC
		public event UpdateScanner.MessageCallback ProgressEvent;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060000D5 RID: 213 RVA: 0x000051E4 File Offset: 0x000033E4
		// (remove) Token: 0x060000D6 RID: 214 RVA: 0x0000521C File Offset: 0x0000341C
		public event UpdateScanner.MessageCallback NormalMessageEvent;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060000D7 RID: 215 RVA: 0x00005254 File Offset: 0x00003454
		// (remove) Token: 0x060000D8 RID: 216 RVA: 0x0000528C File Offset: 0x0000348C
		public event UpdateScanner.MessageCallback WarningMessageEvent;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000D9 RID: 217 RVA: 0x000052C4 File Offset: 0x000034C4
		// (remove) Token: 0x060000DA RID: 218 RVA: 0x000052FC File Offset: 0x000034FC
		public event UpdateScanner.MessageCallback ErrorMessageEvent;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060000DB RID: 219 RVA: 0x00005334 File Offset: 0x00003534
		// (remove) Token: 0x060000DC RID: 220 RVA: 0x0000536C File Offset: 0x0000356C
		public event UpdateScanner.MessageCallback LogMessageEvent;

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000053A1 File Offset: 0x000035A1
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000053A9 File Offset: 0x000035A9
		public string ClientServerUrl { get; set; }

		// Token: 0x060000DF RID: 223 RVA: 0x000053B4 File Offset: 0x000035B4
		public UpdateScanner()
		{
			this.clientGuid = Guid.NewGuid();
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(UpdateScanner.PinPublicKey);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005438 File Offset: 0x00003638
		public static bool PinPublicKey(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return certificate != null && chain != null && chain.ChainElements != null && chain.ChainElements.Count >= 3 && chain.ChainElements[1].Certificate.GetPublicKeyString() == "3082020A02820201008B15EABD7BB76731A4870E148A920978C1D568B6F29A59E07FC81A6746DE319A02AAC8E74749E9A93A874C85FE3C2E1B470445AB5611BB60B8523689DBAB5FC828AEF31B39A0BF06A264D9ACE62343CD699590E5AF886F98DBFB22C2072C48F14C85035A6ACD4061E1221A22E39B2B3DECE38B9B2BCC55658114BACCE18AFED0BC3A27173A4741FBB830F10432D1D5396E7A46CB322AE173768B353A15D88E94826678BE917C0BA301AE80E309842C692BD98C24D4F23F350777E19FE354435FEC8AFD24584FA35460D2005F75442BD2EF7FBB3EB65EFB77FBE19E66A5401F313CD0E33204B8A7EBA512DF701A6AF0A35210B858C3887DA4B331F97F6153AE2DCC823AF64A8B4364F2CC93DDE0D5142E4806661040F623E79F3950B877211229407BFC77F430D35EBA37B367203B8A1DFF997839A69B85CFAD5A7230512A598D281735327A941DB052057F4A06E04D7DEA96D52B0CA32D895AB55C2AABC4B6A6D35F699F104300FDA79C98C3BA4BBBE8D8233C73B38116363DB1BC619AC012DD30B490B7DF48ADD8176A0332A7605B9ECF1A6D7865548FD3528EE56E84F2CAB8DF191DDAB4FA299EC81EA3D6D7C4303EC1DB7DFCB97906E0BA679A745DDCAF247B12522DD5571C00DC3D9AA03E926D435A82F7B586FE9E54B430C29D60D14F4FDD37571D1C84DE5E8FD0905C706EE33AB74649B3EF01796095443BD819D022650203010001";
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005488 File Offset: 0x00003688
		public DownloadInfo[] Scan(IDeviceInfoProvider deviceInfoProvider, IUpdateEvaluator updateEvaluator, IDownloader downloader)
		{
			int num = 0;
			DownloadInfo[] updates;
			for (;;)
			{
				num++;
				try
				{
					this.deviceInfoProvider = deviceInfoProvider;
					this.updateEvaluator = updateEvaluator;
					this.downloader = downloader;
					from x in this.downloadInfoMap.Values
					select x.Installable = false;
					this.NormalMessageEvent("Checking for updates...");
					BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
					basicHttpBinding.MaxReceivedMessageSize = 1048576L;
					if (this.ClientServerUrl.StartsWith("https://"))
					{
						basicHttpBinding.Security.Mode = BasicHttpSecurityMode.Transport;
					}
					string text = this.ClientServerUrl;
					if (!text.EndsWith("/"))
					{
						text += "/";
					}
					text += this.clientServerPath;
					EndpointAddress remoteAddress = new EndpointAddress(text);
					this.clientWebService = new ClientSoapClient(basicHttpBinding, remoteAddress);
					if (Evaluator.GetDebugLoggingEnabled())
					{
						this.LogMessageEvent(string.Format("CertificateValidationMode: {0}", this.clientWebService.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode));
						this.LogMessageEvent(string.Format("TrustedStoreLocation: {0}", this.clientWebService.ClientCredentials.ServiceCertificate.Authentication.TrustedStoreLocation));
						this.LogMessageEvent(string.Format("RevocationMode: {0}", this.clientWebService.ClientCredentials.ServiceCertificate.Authentication.RevocationMode));
					}
					if (this.cookie.EncryptedData == null)
					{
						this.LoadConfig();
						this.LoadAuthorizationCookie();
						this.LoadCookie();
						if (this.config.IsRegistrationRequired)
						{
							this.RegisterDevice();
						}
					}
					updateEvaluator.Reset();
					updateEvaluator.EvaluateUpdates();
					while (this.SyncUpdates())
					{
					}
					int[] installableUpdates = updateEvaluator.GetInstallableUpdates();
					updates = this.GetUpdates(installableUpdates);
				}
				catch (FaultException ex)
				{
					if (num == 3)
					{
						throw new Exception("Maximum SOAP faults reached.");
					}
					this.WarningMessageEvent("SOAP fault received from server.  Will clear update cache and retry.");
					if (Evaluator.GetDebugLoggingEnabled())
					{
						this.LogMessageEvent(string.Format("FaultException.Action: {0}", ex.Action));
						this.LogMessageEvent(string.Format("FaultException.InnerException: {0}", ex.InnerException));
						this.LogMessageEvent(string.Format("FaultException.Reason: {0}", ex.Reason));
						this.LogMessageEvent(string.Format("FaultException.Source: {0}", ex.Source));
						this.LogMessageEvent(string.Format("FaultException.Message: {0}", ex.Message));
						this.LogMessageEvent(string.Format("FaultException.StackTrace: {0}", ex.StackTrace));
					}
					updateEvaluator.Clear();
					this.cookie.EncryptedData = null;
					continue;
				}
				break;
			}
			return updates;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000576C File Offset: 0x0000396C
		private void LoadConfig()
		{
			this.config = this.clientWebService.GetConfig("1.0");
			string value = (from x in this.config.Properties
			where x.Name == "ProtocolVersion"
			select x).Single<ConfigurationProperty>().Value;
			this.maxExtendedUpdatesPerRequest = int.Parse((from x in this.config.Properties
			where x.Name == "MaxExtendedUpdatesPerRequest"
			select x).Single<ConfigurationProperty>().Value);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005810 File Offset: 0x00003A10
		private void LoadAuthorizationCookie()
		{
			foreach (AuthPlugInInfo authInfo2 in this.config.AuthInfo)
			{
				if (this.GetAnonymousAuthorizationCookie(authInfo2))
				{
					return;
				}
				if (this.GetSimpleTargetingAuthorizationCookie(authInfo2))
				{
					return;
				}
			}
			throw new Exception("Failed to load authorization cookie");
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000585C File Offset: 0x00003A5C
		private bool GetAnonymousAuthorizationCookie(AuthPlugInInfo authInfo)
		{
			if (authInfo.PlugInID != "Anonymous")
			{
				return false;
			}
			this.authCookies[0] = new Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService.AuthorizationCookie();
			this.authCookies[0].PlugInId = "Anonymous";
			this.authCookies[0].CookieData = this.clientGuid.ToByteArray();
			return true;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000058B8 File Offset: 0x00003AB8
		private bool GetSimpleTargetingAuthorizationCookie(AuthPlugInInfo authInfo)
		{
			string b = "SimpleTargeting";
			if (authInfo.PlugInID != b)
			{
				return false;
			}
			BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
			basicHttpBinding.MaxReceivedMessageSize = 1048576L;
			if (this.ClientServerUrl.StartsWith("https://"))
			{
				basicHttpBinding.Security.Mode = BasicHttpSecurityMode.Transport;
			}
			string text = this.ClientServerUrl;
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			text += authInfo.ServiceUrl;
			EndpointAddress remoteAddress = new EndpointAddress(text);
			Microsoft.Tools.DeviceUpdate.EvaluationUtils.SimpleAuthWebService.AuthorizationCookie authorizationCookie = new SimpleAuthSoapClient(basicHttpBinding, remoteAddress).GetAuthorizationCookie(this.clientGuid.ToString(), "", "DeviceUpdateAgent");
			this.authCookies[0] = new Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService.AuthorizationCookie();
			this.authCookies[0].PlugInId = authorizationCookie.PlugInId;
			this.authCookies[0].CookieData = authorizationCookie.CookieData;
			this.authCookies[0].ExtensionData = authorizationCookie.ExtensionData;
			return true;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000059B2 File Offset: 0x00003BB2
		private void LoadCookie()
		{
			this.cookie = this.clientWebService.GetCookie(this.authCookies, this.cookie, DateTime.UtcNow, DateTime.UtcNow, "1.0");
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000059E0 File Offset: 0x00003BE0
		private void RegisterDevice()
		{
			ComputerInfo computerInfo = new ComputerInfo();
			DeviceInfoFromCab deviceInfoFromCab = this.deviceInfoProvider as DeviceInfoFromCab;
			computerInfo.OSMajorVersion = int.Parse(deviceInfoFromCab.MajorVersion);
			computerInfo.OSMinorVersion = int.Parse(deviceInfoFromCab.MinorVersion);
			computerInfo.DnsName = "DeviceUpdateAgent";
			computerInfo.ComputerModel = deviceInfoFromCab.PhoneManufacturerModelName;
			computerInfo.MobileOperator = deviceInfoFromCab.PhoneMobileOperatorName;
			computerInfo.OSBuildNumber = int.Parse(deviceInfoFromCab.BuildNumber);
			computerInfo.ProcessorArchitecture = deviceInfoFromCab.CpuIdString;
			computerInfo.ClientVersionMajorNumber = 1;
			computerInfo.ClientVersionMinorNumber = 0;
			computerInfo.ClientVersionBuildNumber = 0;
			computerInfo.ClientVersionQfeNumber = 0;
			computerInfo.ComputerManufacturer = deviceInfoFromCab.PhoneManufacturer;
			computerInfo.FirmwareVersion = deviceInfoFromCab.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Platform\\DeviceTargetingInfo", "PhoneFirmwareRevision");
			computerInfo.OSLocale = deviceInfoFromCab.Language;
			this.clientWebService.RegisterComputer(this.cookie, computerInfo);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005AC0 File Offset: 0x00003CC0
		private bool SyncUpdates()
		{
			bool flag = false;
			SyncUpdateParameters syncUpdateParameters = new SyncUpdateParameters();
			syncUpdateParameters.InstalledNonLeafUpdateIDs = new ArrayOfInt();
			syncUpdateParameters.OtherCachedUpdateIDs = new ArrayOfInt();
			this.updateEvaluator.PartitionUpdates(syncUpdateParameters.InstalledNonLeafUpdateIDs, syncUpdateParameters.OtherCachedUpdateIDs);
			syncUpdateParameters.FilterCategoryIds = new CategoryIdentifier[1];
			syncUpdateParameters.FilterCategoryIds[0] = new CategoryIdentifier();
			syncUpdateParameters.FilterCategoryIds[0].Id = Guid.Parse("{" + this.deviceInfoProvider.Category + "}");
			SyncInfo syncInfo = this.clientWebService.SyncUpdates(this.cookie, syncUpdateParameters);
			this.cookie = syncInfo.NewCookie;
			flag |= syncInfo.Truncated;
			if (syncInfo.OutOfScopeRevisionIDs != null)
			{
				flag |= (syncInfo.OutOfScopeRevisionIDs.Count > 0);
				foreach (int id in syncInfo.OutOfScopeRevisionIDs)
				{
					this.updateEvaluator.RemoveUpdate(id);
				}
			}
			if (syncInfo.ChangedUpdates != null)
			{
				foreach (UpdateInfo updateInfo in syncInfo.ChangedUpdates)
				{
					this.updateEvaluator.ChangeUpdate(updateInfo.ID, updateInfo.IsLeaf, updateInfo.Xml);
					flag |= !updateInfo.IsLeaf;
				}
			}
			if (syncInfo.NewUpdates != null)
			{
				foreach (UpdateInfo updateInfo2 in syncInfo.NewUpdates)
				{
					string updateXml = this.ExpandCabbedMetadata(updateInfo2.Xml);
					this.updateEvaluator.AddUpdate(updateInfo2.ID, updateInfo2.IsLeaf, updateXml);
					flag |= !updateInfo2.IsLeaf;
				}
			}
			this.updateEvaluator.EvaluateUpdates();
			return flag;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005C9C File Offset: 0x00003E9C
		private string ExpandCabbedMetadata(string xml)
		{
			string xml2 = string.Format("<Update xmlns=\"{0}\">{1}</Update>", UpdateXmlNamespaceManager.Instance.LookupNamespace("msus-pub"), xml);
			this.doc.LoadXml(xml2);
			XmlNode xmlNode = this.doc.SelectSingleNode("/msus-pub:Update/msus-pub:Ref", UpdateXmlNamespaceManager.Instance);
			if (xmlNode == null)
			{
				return xml;
			}
			string value = xmlNode.Attributes["CabDigest"].Value;
			string b = value;
			HashAlgorithm hashAlgorithm = this.sha1;
			XmlNode xmlNode2 = xmlNode.SelectSingleNode("./msus-pub:AdditionalDigest", UpdateXmlNamespaceManager.Instance);
			if (xmlNode2 != null && xmlNode2.Attributes["Algorithm"].Value == "SHA256")
			{
				b = xmlNode2.InnerText;
				hashAlgorithm = this.sha256;
			}
			ArrayOfBase64Binary arrayOfBase64Binary = new ArrayOfBase64Binary();
			arrayOfBase64Binary.Add(Convert.FromBase64String(value));
			GetFileLocationsResults fileLocations = this.clientWebService.GetFileLocations(this.cookie, arrayOfBase64Binary);
			this.cookie = fileLocations.NewCookie;
			string url = fileLocations.FileLocations[0].Url;
			string text = null;
			string text2 = null;
			string result;
			try
			{
				text = this.downloader.DownloadToTempPath(url);
				using (FileStream fileStream = File.OpenRead(text))
				{
					if (Convert.ToBase64String(hashAlgorithm.ComputeHash(fileStream)) != b)
					{
						throw new Exception("invalid hash for cabbed metadata file");
					}
				}
				text2 = this.ExtractFirstToTempPath(text);
				result = File.ReadAllText(text2);
			}
			finally
			{
				if (!string.IsNullOrEmpty(text) && File.Exists(text))
				{
					File.Delete(text);
				}
				if (!string.IsNullOrEmpty(text2) && File.Exists(text2))
				{
					File.Delete(text2);
				}
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005E4C File Offset: 0x0000404C
		private string ExtractFirstToTempPath(string cabPath)
		{
			string text = null;
			string result;
			try
			{
				string text2 = CabApiWrapper.GetFileList(cabPath)[0];
				string tempPath = Path.GetTempPath();
				text = Path.Combine(tempPath, text2);
				CabApiWrapper.ExtractOne(cabPath, tempPath, text2);
				result = text;
			}
			catch
			{
				if (!string.IsNullOrEmpty(text) && File.Exists(text))
				{
					File.Delete(text);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005EA8 File Offset: 0x000040A8
		private DownloadInfo[] GetUpdates(int[] ids)
		{
			if (ids.Length == 0)
			{
				return null;
			}
			ArrayOfString arrayOfString = new ArrayOfString();
			arrayOfString.AddRange(this.deviceInfoProvider.Locales);
			for (int i = 0; i < ids.Length; i += this.maxExtendedUpdatesPerRequest)
			{
				ArrayOfInt arrayOfInt = new ArrayOfInt();
				for (int j = 0; j < Math.Min(this.maxExtendedUpdatesPerRequest, ids.Length - i); j++)
				{
					arrayOfInt.Add(ids[i + j]);
				}
				foreach (UpdateData updateData in this.clientWebService.GetExtendedUpdateInfo(this.cookie, arrayOfInt, new XmlUpdateFragmentType[]
				{
					XmlUpdateFragmentType.Extended
				}, arrayOfString).Updates)
				{
					string xml = string.Format("<Update xmlns=\"{0}\">{1}</Update>", UpdateXmlNamespaceManager.Instance.LookupNamespace("msus-pub"), updateData.Xml);
					this.doc.LoadXml(xml);
					XmlNodeList xmlNodeList = this.doc.SelectNodes("/msus-pub:Update/msus-pub:Files/msus-pub:File", UpdateXmlNamespaceManager.Instance);
					if (xmlNodeList != null)
					{
						foreach (object obj in xmlNodeList)
						{
							XmlNode xmlNode = (XmlNode)obj;
							string value = xmlNode.Attributes["FileName"].Value;
							int num = int.Parse(xmlNode.Attributes["Size"].Value);
							string value2 = xmlNode.Attributes["DigestAlgorithm"].Value;
							string value3 = xmlNode.Attributes["Digest"].Value;
							string sha256Hash = null;
							if (value2 != "SHA1")
							{
								throw new Exception(string.Format("Unexpected hash algorithm on package: {0}", value2));
							}
							XmlNodeList xmlNodeList2 = xmlNode.SelectNodes("./msus-pub:AdditionalDigest", UpdateXmlNamespaceManager.Instance);
							if (xmlNodeList2 != null)
							{
								foreach (object obj2 in xmlNodeList2)
								{
									XmlNode xmlNode2 = (XmlNode)obj2;
									if (xmlNode2.Attributes["Algorithm"].Value == "SHA256")
									{
										sha256Hash = xmlNode2.InnerText;
										break;
									}
								}
							}
							DownloadInfo downloadInfo;
							if (this.downloadInfoMap.ContainsKey(value3))
							{
								downloadInfo = this.downloadInfoMap[value3];
							}
							else
							{
								downloadInfo = new DownloadInfo();
								this.downloadInfoMap[value3] = downloadInfo;
							}
							downloadInfo.Name = string.Concat(new object[]
							{
								Path.GetFileNameWithoutExtension(value),
								".",
								updateData.ID,
								Path.GetExtension(value)
							});
							downloadInfo.Size = (long)num;
							downloadInfo.SHA1Hash = value3;
							downloadInfo.SHA256Hash = sha256Hash;
							downloadInfo.Installable = true;
						}
					}
				}
			}
			ArrayOfBase64Binary arrayOfBase64Binary = new ArrayOfBase64Binary();
			arrayOfBase64Binary.AddRange(from x in this.downloadInfoMap.Values
			where x.Installable
			select Convert.FromBase64String(x.SHA1Hash));
			if (arrayOfBase64Binary.Count > 0)
			{
				GetFileLocationsResults fileLocations = this.clientWebService.GetFileLocations(this.cookie, arrayOfBase64Binary);
				this.cookie = fileLocations.NewCookie;
				foreach (FileLocation fileLocation in fileLocations.FileLocations)
				{
					this.downloadInfoMap[Convert.ToBase64String(fileLocation.FileDigest)].Url = fileLocation.Url;
				}
			}
			return (from x in this.downloadInfoMap.Values
			where x.Installable
			select x).ToArray<DownloadInfo>();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000062E4 File Offset: 0x000044E4
		public string[] GetUpdateTitles(Evaluator evaluator)
		{
			int[] explicitlyDeployableUpdates = evaluator.GetExplicitlyDeployableUpdates();
			List<string> list = new List<string>();
			if (explicitlyDeployableUpdates.Length == 0)
			{
				return list.ToArray();
			}
			ArrayOfString arrayOfString = new ArrayOfString();
			arrayOfString.Add("en-en");
			for (int i = 0; i < explicitlyDeployableUpdates.Length; i += this.maxExtendedUpdatesPerRequest)
			{
				ArrayOfInt arrayOfInt = new ArrayOfInt();
				for (int j = 0; j < Math.Min(this.maxExtendedUpdatesPerRequest, explicitlyDeployableUpdates.Length - i); j++)
				{
					arrayOfInt.Add(explicitlyDeployableUpdates[i + j]);
				}
				foreach (UpdateData updateData in this.clientWebService.GetExtendedUpdateInfo(this.cookie, arrayOfInt, new XmlUpdateFragmentType[]
				{
					XmlUpdateFragmentType.LocalizedProperties
				}, arrayOfString).Updates)
				{
					string xml = string.Format("<Update xmlns=\"{0}\">{1}</Update>", UpdateXmlNamespaceManager.Instance.LookupNamespace("msus-pub"), updateData.Xml);
					this.doc.LoadXml(xml);
					XmlNode xmlNode = this.doc.SelectSingleNode("/msus-pub:Update/msus-pub:LocalizedProperties/msus-pub:Title", UpdateXmlNamespaceManager.Instance);
					list.Add(xmlNode.InnerText);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0400005D RID: 93
		private Guid clientGuid;

		// Token: 0x0400005E RID: 94
		private const string metadataXmlFile = "metadata.xml";

		// Token: 0x0400005F RID: 95
		private string clientServerPath = "ClientWebService/client.asmx";

		// Token: 0x04000060 RID: 96
		private const string clientVersion = "1.0";

		// Token: 0x04000061 RID: 97
		private int maxExtendedUpdatesPerRequest = 1;

		// Token: 0x04000062 RID: 98
		private ClientSoapClient clientWebService;

		// Token: 0x04000063 RID: 99
		private Config config;

		// Token: 0x04000064 RID: 100
		private Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService.AuthorizationCookie[] authCookies = new Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService.AuthorizationCookie[1];

		// Token: 0x04000065 RID: 101
		private Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService.Cookie cookie = new Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService.Cookie();

		// Token: 0x04000066 RID: 102
		private IDeviceInfoProvider deviceInfoProvider;

		// Token: 0x04000067 RID: 103
		private IUpdateEvaluator updateEvaluator;

		// Token: 0x04000068 RID: 104
		private IDownloader downloader;

		// Token: 0x04000069 RID: 105
		private XmlDocument doc = new XmlDocument();

		// Token: 0x0400006A RID: 106
		private Dictionary<string, DownloadInfo> downloadInfoMap = new Dictionary<string, DownloadInfo>();

		// Token: 0x0400006B RID: 107
		private SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

		// Token: 0x0400006C RID: 108
		private SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();

		// Token: 0x0400006D RID: 109
		private const string signingCertPublicKey = "3082020A02820201008B15EABD7BB76731A4870E148A920978C1D568B6F29A59E07FC81A6746DE319A02AAC8E74749E9A93A874C85FE3C2E1B470445AB5611BB60B8523689DBAB5FC828AEF31B39A0BF06A264D9ACE62343CD699590E5AF886F98DBFB22C2072C48F14C85035A6ACD4061E1221A22E39B2B3DECE38B9B2BCC55658114BACCE18AFED0BC3A27173A4741FBB830F10432D1D5396E7A46CB322AE173768B353A15D88E94826678BE917C0BA301AE80E309842C692BD98C24D4F23F350777E19FE354435FEC8AFD24584FA35460D2005F75442BD2EF7FBB3EB65EFB77FBE19E66A5401F313CD0E33204B8A7EBA512DF701A6AF0A35210B858C3887DA4B331F97F6153AE2DCC823AF64A8B4364F2CC93DDE0D5142E4806661040F623E79F3950B877211229407BFC77F430D35EBA37B367203B8A1DFF997839A69B85CFAD5A7230512A598D281735327A941DB052057F4A06E04D7DEA96D52B0CA32D895AB55C2AABC4B6A6D35F699F104300FDA79C98C3BA4BBBE8D8233C73B38116363DB1BC619AC012DD30B490B7DF48ADD8176A0332A7605B9ECF1A6D7865548FD3528EE56E84F2CAB8DF191DDAB4FA299EC81EA3D6D7C4303EC1DB7DFCB97906E0BA679A745DDCAF247B12522DD5571C00DC3D9AA03E926D435A82F7B586FE9E54B430C29D60D14F4FDD37571D1C84DE5E8FD0905C706EE33AB74649B3EF01796095443BD819D022650203010001";

		// Token: 0x0200006A RID: 106
		// (Invoke) Token: 0x06000332 RID: 818
		public delegate void MessageCallback(string message);
	}
}
