using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using Microsoft.WindowsPhone.ImageUpdate.PkgCommon;
using Microsoft.WindowsPhone.ImageUpdate.Tools;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x02000002 RID: 2
	public class DeviceInfoFromCab : IDeviceInfoProvider
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (remove) Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
		public event DeviceInfoFromCab.MessageCallback ProgressEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event DeviceInfoFromCab.MessageCallback NormalMessageEvent;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public event DeviceInfoFromCab.MessageCallback WarningMessageEvent;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000007 RID: 7 RVA: 0x000021A0 File Offset: 0x000003A0
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x000021D8 File Offset: 0x000003D8
		public event DeviceInfoFromCab.MessageCallback ErrorMessageEvent;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000220D File Offset: 0x0000040D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002215 File Offset: 0x00000415
		public InstalledPackageInfo[] InstalledPackageInfoList { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000221E File Offset: 0x0000041E
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002226 File Offset: 0x00000426
		public string[] InstalledPackages { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000222F File Offset: 0x0000042F
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002237 File Offset: 0x00000437
		public string MobileCoreVersion { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002240 File Offset: 0x00000440
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002248 File Offset: 0x00000448
		public string Label { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002251 File Offset: 0x00000451
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002259 File Offset: 0x00000459
		public string MajorVersion { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002262 File Offset: 0x00000462
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000226A File Offset: 0x0000046A
		public string MinorVersion { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002273 File Offset: 0x00000473
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000227B File Offset: 0x0000047B
		public string QfeLevel { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002284 File Offset: 0x00000484
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000228C File Offset: 0x0000048C
		public string BuildNumber { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002298 File Offset: 0x00000498
		public string OsVersion
		{
			get
			{
				int num = int.Parse(this.QfeLevel);
				int num2 = int.Parse(this.MobileCoreVersion);
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					this.MajorVersion,
					this.MinorVersion,
					(num > num2) ? num : num2,
					this.BuildNumber
				});
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022F8 File Offset: 0x000004F8
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002300 File Offset: 0x00000500
		public string Timestamp { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002309 File Offset: 0x00000509
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002311 File Offset: 0x00000511
		public string PhoneManufacturer { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000231A File Offset: 0x0000051A
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002322 File Offset: 0x00000522
		public string PhoneManufacturerModelName { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000232B File Offset: 0x0000052B
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002333 File Offset: 0x00000533
		public string PhoneMobileOperatorName { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000233C File Offset: 0x0000053C
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002344 File Offset: 0x00000544
		public BuildType BuildType { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000234D File Offset: 0x0000054D
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002355 File Offset: 0x00000555
		public string BuildTypeString { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000235E File Offset: 0x0000055E
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002366 File Offset: 0x00000566
		public string Resolution { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000236F File Offset: 0x0000056F
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002377 File Offset: 0x00000577
		public string Device { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002380 File Offset: 0x00000580
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002388 File Offset: 0x00000588
		public string ReleaseType { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002391 File Offset: 0x00000591
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002399 File Offset: 0x00000599
		public bool IsVm { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023A2 File Offset: 0x000005A2
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000023AA File Offset: 0x000005AA
		public CpuId CpuId { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000023B3 File Offset: 0x000005B3
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000023BB File Offset: 0x000005BB
		public string CpuIdString { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000023C4 File Offset: 0x000005C4
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000023CC File Offset: 0x000005CC
		public string SourceBuild { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000023D5 File Offset: 0x000005D5
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000023DD File Offset: 0x000005DD
		public string Category { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000023E6 File Offset: 0x000005E6
		public string Product
		{
			get
			{
				return "MC";
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000023ED File Offset: 0x000005ED
		public string Flavor
		{
			get
			{
				return string.Format("{0}.{1}{2}", this.Product, this.CpuIdString, this.BuildTypeString);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000240B File Offset: 0x0000060B
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002413 File Offset: 0x00000613
		public string Language { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000241C File Offset: 0x0000061C
		public string FFUName
		{
			get
			{
				if (!this.IsVm)
				{
					return "flash.ffu";
				}
				return "flash.vhd";
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002431 File Offset: 0x00000631
		public string ImageType
		{
			get
			{
				if (string.Compare(this.ReleaseType, "Test", true) != 0)
				{
					return "production";
				}
				if (!this.IsVm)
				{
					return "selfhost";
				}
				return "test";
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000245F File Offset: 0x0000065F
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002467 File Offset: 0x00000667
		public string[] Locales { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002470 File Offset: 0x00000670
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002478 File Offset: 0x00000678
		public string[] RedirCabUrls { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002481 File Offset: 0x00000681
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002489 File Offset: 0x00000689
		public string DeviceID { get; set; }

		// Token: 0x06000043 RID: 67 RVA: 0x000024BC File Offset: 0x000006BC
		public void Load(string cabPath)
		{
			this.tempDir = Path.Combine(Path.GetTempPath(), Thread.CurrentThread.ManagedThreadId.ToString());
			Directory.CreateDirectory(this.tempDir);
			this.GetInstalledPackages(cabPath);
			this.GetMobileCoreVersion();
			this.GetDeviceInfoFromRegistry(cabPath);
			this.GetDeviceInfoFromOemInput(cabPath);
			this.IsVm = (string.Compare(this.Device, "VM", true) == 0);
			this.CpuIdString = (this.IsVm ? "x86" : "arm");
			this.CpuId = this.GetCpuId(this.CpuIdString);
			this.SourceBuild = string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				this.Label,
				this.MobileCoreVersion,
				this.QfeLevel,
				this.Timestamp
			});
			this.InstalledPackages = (from x in this.InstalledPackageInfoList
			select x.Package).ToArray<string>();
			SortedSet<string> sortedSet = new SortedSet<string>(StringComparer.InvariantCultureIgnoreCase);
			foreach (InstalledPackageInfo installedPackageInfo in this.InstalledPackageInfoList)
			{
				Dictionary<string, Version> dictionary;
				if (this.installedPackageVersions.ContainsKey(installedPackageInfo.Partition))
				{
					dictionary = this.installedPackageVersions[installedPackageInfo.Partition];
				}
				else
				{
					dictionary = new Dictionary<string, Version>(StringComparer.InvariantCultureIgnoreCase);
					this.installedPackageVersions[installedPackageInfo.Partition] = dictionary;
				}
				dictionary[installedPackageInfo.Package] = installedPackageInfo.Version;
				if (installedPackageInfo.Package.StartsWith("microsoft.mainos.production_lang_"))
				{
					sortedSet.Add(installedPackageInfo.Package.Replace("microsoft.mainos.production_lang_", ""));
				}
				this.Locales = sortedSet.ToArray<string>();
			}
			this.Category = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\DeviceUpdate\\Agent\\Settings", "GuidOfCategoryToScan");
			if (this.IsVm)
			{
				this.Resolution = "allres";
			}
			List<string> list = new List<string>();
			string registryValue = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\DeviceUpdate\\Agent\\Protocol", "MuReDirCabUrl1");
			if (!string.IsNullOrEmpty(registryValue))
			{
				list.Add(registryValue);
			}
			registryValue = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\DeviceUpdate\\Agent\\Protocol", "MuReDirCabUrl2");
			if (!string.IsNullOrEmpty(registryValue))
			{
				list.Add(registryValue);
			}
			registryValue = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\DeviceUpdate\\Agent\\Protocol", "MuReDirCabUrl3");
			if (!string.IsNullOrEmpty(registryValue))
			{
				list.Add(registryValue);
			}
			this.RedirCabUrls = list.ToArray();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002730 File Offset: 0x00000930
		private void GetInstalledPackages(string cabPath)
		{
			string path = Path.Combine(this.tempDir, "InstalledPackages.csv");
			try
			{
				List<InstalledPackageInfo> list = new List<InstalledPackageInfo>();
				CabApiWrapper.ExtractOne(cabPath, this.tempDir, "InstalledPackages.csv");
				using (Stream stream = File.OpenRead(path))
				{
					StreamReader streamReader = new StreamReader(stream);
					bool flag = false;
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						if (!flag)
						{
							flag = true;
						}
						else
						{
							text = text.ToLowerInvariant();
							string[] array = text.Split(",".ToCharArray());
							InstalledPackageInfo item = new InstalledPackageInfo(array[0], array[1], array[2]);
							list.Add(item);
						}
					}
				}
				this.InstalledPackageInfoList = list.ToArray();
			}
			finally
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002808 File Offset: 0x00000A08
		private void GetDeviceInfoFromRegistry(string logPath)
		{
			string text = Path.Combine(this.tempDir, "UsoTroubleshooting.reg");
			try
			{
				try
				{
					CabApiWrapper.ExtractOne(logPath, this.tempDir, "UsoTroubleshooting.reg");
				}
				catch
				{
					text = Path.Combine(this.tempDir, "DuTroubleshooting.reg");
					CabApiWrapper.ExtractOne(logPath, this.tempDir, "DuTroubleshooting.reg");
				}
				this.LoadRegistry(text);
				this.Label = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Versions", "label");
				this.MajorVersion = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Versions", "MajorVersion");
				this.MinorVersion = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Versions", "MinorVersion");
				this.QfeLevel = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Versions", "QFELevel");
				this.BuildNumber = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Versions", "BuildNumber");
				this.Timestamp = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Versions", "timestamp");
				this.PhoneManufacturer = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Platform\\DeviceTargetingInfo", "phoneManufacturer");
				this.PhoneManufacturerModelName = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Platform\\DeviceTargetingInfo", "phoneManufacturerModelName");
				this.PhoneMobileOperatorName = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Platform\\DeviceTargetingInfo", "phoneMobileOperatorName");
			}
			finally
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002970 File Offset: 0x00000B70
		private void LoadRegistry(string regPath)
		{
			using (Stream stream = File.OpenRead(regPath))
			{
				StreamReader streamReader = new StreamReader(stream);
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					if (!text.StartsWith("[") || !text.EndsWith("]"))
					{
						throw new Exception(string.Format("Registry format error: {0}", text));
					}
					text = text.Substring(1);
					text = text.Substring(0, text.Length - 1);
					Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
					this.registryValues[text] = dictionary;
					string text2;
					while ((text2 = streamReader.ReadLine()) != null && text2.Length != 0)
					{
						string text3 = "";
						string text4 = "";
						bool flag = false;
						int i;
						for (i = 0; i < text2.Length; i++)
						{
							if (text2[i] == '=')
							{
								flag = true;
								break;
							}
							if (text2[i] == '"')
							{
								break;
							}
						}
						if (!flag)
						{
							if (i == text2.Length)
							{
								throw new Exception(string.Format("Registry format error: {0}", text2));
							}
							for (i++; i < text2.Length; i++)
							{
								if (text2[i] == '=')
								{
									flag = true;
									break;
								}
								if (text2[i] == '"')
								{
									break;
								}
								text3 += text2[i].ToString();
							}
							if (!flag)
							{
								if (i == text2.Length)
								{
									throw new Exception(string.Format("Registry format error: {0}", text2));
								}
								i++;
								if (text2[i] != '=')
								{
									throw new Exception(string.Format("Registry format error: {0}", text2));
								}
								i++;
								if (text2.Substring(i).StartsWith("DWORD:"))
								{
									text4 = text2.Substring(i).Replace("DWORD:", "");
									text4 = uint.Parse(text4, NumberStyles.HexNumber).ToString();
								}
								else if (text2.Substring(i).StartsWith("QWORD:"))
								{
									text4 = "";
								}
								else if (text2.Substring(i).StartsWith("hex"))
								{
									text4 = "";
								}
								else if (text2.Substring(i).StartsWith("MULTI_SZ"))
								{
									text4 = "";
								}
								else
								{
									while (i < text2.Length && text2[i] != '"')
									{
										i++;
									}
									if (i == text2.Length)
									{
										throw new Exception(string.Format("Registry format error: {0}", text2));
									}
									i++;
									for (;;)
									{
										if (i < text2.Length && text2[i] != '"')
										{
											text4 += text2[i].ToString();
											i++;
										}
										else if (i == text2.Length)
										{
											text2 = streamReader.ReadLine();
											i = 0;
										}
										else if (text2[i] == '"')
										{
											break;
										}
									}
								}
								if (dictionary.ContainsKey(text3))
								{
									this.WarningMessageEvent(this.DeviceID, string.Format("Found multiple entries for registry value while parsing logs: {0}\\{1}", text, text3));
								}
								else
								{
									dictionary.Add(text3, text4);
								}
							}
						}
					}
					if (text2 == null)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public string GetRegistryValue(string key, string value)
		{
			if (this.registryValues.ContainsKey(key) && this.registryValues[key].ContainsKey(value))
			{
				return this.registryValues[key][value];
			}
			return null;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D1C File Offset: 0x00000F1C
		public void SetRegistryValue(string key, string value, string data)
		{
			if (!this.registryValues.ContainsKey(key))
			{
				this.registryValues[key] = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			}
			this.registryValues[key][value] = data;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D58 File Offset: 0x00000F58
		private void GetMobileCoreVersion()
		{
			foreach (InstalledPackageInfo installedPackageInfo in this.InstalledPackageInfoList)
			{
				if (string.Compare(installedPackageInfo.Partition, "MainOS", true) == 0 && (string.Compare(installedPackageInfo.Package, "Microsoft.MobileCore.Prod.MainOS", true) == 0 || string.Compare(installedPackageInfo.Package, "Microsoft.MobileCore.MainOS", true) == 0))
				{
					this.MobileCoreVersion = installedPackageInfo.Version.Build.ToString();
					return;
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DD4 File Offset: 0x00000FD4
		private void GetDeviceInfoFromOemInput(string cabPath)
		{
			string text = Path.Combine(this.tempDir, "OemInput.xml");
			try
			{
				CabApiWrapper.ExtractOne(cabPath, this.tempDir, "OemInput.xml");
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(text);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				xmlNamespaceManager.AddNamespace("iu", "http://schemas.microsoft.com/embedded/2004/10/ImageUpdate");
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/iu:OEMInput/iu:BuildType", xmlNamespaceManager);
				this.BuildTypeString = xmlNode.InnerText;
				this.BuildType = this.GetBuildType(this.BuildTypeString);
				XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/iu:OEMInput/iu:Resolutions/iu:Resolution", xmlNamespaceManager);
				this.Resolution = xmlNode2.InnerText;
				XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/iu:OEMInput/iu:Device", xmlNamespaceManager);
				string text2 = this.PhoneManufacturerModelName.ToLowerInvariant();
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 1835634037U)
				{
					if (num <= 1768523561U)
					{
						if (num != 158757822U)
						{
							if (num != 1768523561U)
							{
								goto IL_230;
							}
							if (!(text2 == "rm-1100"))
							{
								goto IL_230;
							}
							this.Device = "cityman_LTE_AMERICAS";
							goto IL_23D;
						}
						else if (!(text2 == "rm-1105_12716"))
						{
							goto IL_230;
						}
					}
					else if (num != 1818856418U)
					{
						if (num != 1835634037U)
						{
							goto IL_230;
						}
						if (!(text2 == "rm-1104"))
						{
							goto IL_230;
						}
						this.Device = "talkman_LTE_ROW";
						goto IL_23D;
					}
					else if (!(text2 == "rm-1105"))
					{
						goto IL_230;
					}
				}
				else if (num <= 3141548860U)
				{
					if (num != 2127588917U)
					{
						if (num != 3141548860U)
						{
							goto IL_230;
						}
						if (!(text2 == "id324-2"))
						{
							goto IL_230;
						}
					}
					else
					{
						if (!(text2 == "rm-1085"))
						{
							goto IL_230;
						}
						this.Device = "cityman_LTE_ROW";
						goto IL_23D;
					}
				}
				else
				{
					if (num != 3933218936U)
					{
						if (num != 3966685840U)
						{
							if (num != 3983463459U)
							{
								goto IL_230;
							}
							if (!(text2 == "rm-1128"))
							{
								goto IL_230;
							}
						}
						else if (!(text2 == "rm-1129"))
						{
							goto IL_230;
						}
						this.Device = "saimaa_LTE_AMERICAS";
						goto IL_23D;
					}
					if (!(text2 == "rm-1105_16048"))
					{
						goto IL_230;
					}
				}
				this.Device = "talkman_LTE_AMERICAS";
				goto IL_23D;
				IL_230:
				this.Device = xmlNode3.InnerText;
				IL_23D:
				XmlNode xmlNode4 = xmlDocument.SelectSingleNode("/iu:OEMInput/iu:ReleaseType", xmlNamespaceManager);
				this.ReleaseType = xmlNode4.InnerText;
				XmlNode xmlNode5 = xmlDocument.SelectSingleNode("/iu:OEMInput/iu:BootLocale", xmlNamespaceManager);
				this.Language = ((xmlNode5 != null) ? xmlNode5.InnerText : "USA");
			}
			finally
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000308C File Offset: 0x0000128C
		private CpuId GetCpuId(string cpuType)
		{
			if (string.Compare(cpuType, "arm", true) == 0)
			{
				return CpuId.ARM;
			}
			if (string.Compare(cpuType, "x86", true) == 0)
			{
				return CpuId.X86;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000030B3 File Offset: 0x000012B3
		private BuildType GetBuildType(string buildType)
		{
			if (string.Compare(buildType, "fre", true) == 0)
			{
				return BuildType.Retail;
			}
			if (string.Compare(buildType, "chk", true) == 0)
			{
				return BuildType.Debug;
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000030DC File Offset: 0x000012DC
		public bool EvaluateRegSz(string key, string subkey, string value, string data, string comparison)
		{
			string registryValue = this.GetRegistryValue(string.Format("{0}\\{1}", key, subkey), value);
			return registryValue != null && this.Compare(registryValue.ToLower(), data.ToLower(), comparison);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003118 File Offset: 0x00001318
		public bool EvaluateRegDword(string key, string subkey, string value, uint data, string comparison)
		{
			string registryValue = this.GetRegistryValue(string.Format("{0}\\{1}", key, subkey), value);
			if (registryValue == null)
			{
				return false;
			}
			uint num = uint.Parse(registryValue);
			return this.Compare(num, data, comparison);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000315C File Offset: 0x0000135C
		public bool EvaluateRegKeyExists(string key, string subkey)
		{
			string key2 = string.Format("{0}\\{1}", key, subkey);
			return this.registryValues.ContainsKey(key2);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003184 File Offset: 0x00001384
		public bool EvaluateRegValueExists(string key, string subkey, string value)
		{
			string key2 = string.Format("{0}\\{1}", key, subkey);
			return this.registryValues.ContainsKey(key2) && this.registryValues[key2].ContainsKey(value);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000031C0 File Offset: 0x000013C0
		public bool EvaluateCSPQuery(string locUri, string expectedValueString, string comparison)
		{
			IComparable expected = null;
			IComparable comparable = null;
			if (locUri == "./DevDetail/FwV")
			{
				expected = Version.Parse(expectedValueString);
				Version version = null;
				string registryValue = this.GetRegistryValue("HKEY_LOCAL_MACHINE\\System\\Platform\\DeviceTargetingInfo", "PhoneFirmwareRevision");
				if (registryValue != null)
				{
					version = Version.Parse(registryValue);
				}
				comparable = version;
			}
			else if (locUri == "./DevDetail/SwV")
			{
				expected = Version.Parse(expectedValueString);
				comparable = Version.Parse(this.OsVersion);
			}
			else if (locUri == "./DevDetail/DevTyp")
			{
				expected = expectedValueString.ToLower();
				comparable = this.PhoneManufacturerModelName.ToLower();
			}
			else if (locUri == "./DevInfo/Man")
			{
				expected = expectedValueString.ToLower();
				comparable = this.PhoneManufacturer.ToLower();
			}
			else if (locUri == "./DevDetail/Ext/Microsoft/CommercializationOperator")
			{
				expected = expectedValueString.ToLower();
				comparable = this.PhoneMobileOperatorName.ToLower();
			}
			else if (locUri == "./DevDetail/Ext/Microsoft/ProcessorArchitecture")
			{
				expected = int.Parse(expectedValueString);
				CpuId cpuId = this.CpuId;
				if (cpuId != CpuId.X86)
				{
					if (cpuId != CpuId.ARM)
					{
						throw new Exception(string.Format("Unrecognized ProcessArchitecture: {0}", this.CpuId));
					}
					comparable = 5;
				}
				else
				{
					comparable = 0;
				}
			}
			else if (locUri == "./Vendor/MSFT/Update/DeferUpgrade")
			{
				expected = expectedValueString.ToLower();
				comparable = "false";
			}
			else if (locUri == "./DevDetail/Ext/Microsoft/Resolution")
			{
				expected = expectedValueString.ToLower();
				comparable = this.Resolution.ToLower();
			}
			else if (locUri.StartsWith("./Vendor/MSFT/DevicePackagesInfo/"))
			{
				this.GetDevicePackagesInfo(locUri, expectedValueString, out expected, out comparable);
			}
			else
			{
				if (locUri.StartsWith("./Vendor/MSFT/GlobalExperience/Speech/Locales/"))
				{
					return false;
				}
				if (locUri.StartsWith("./Vendor/MSFT/InputLexicons/"))
				{
					return false;
				}
				if (locUri.StartsWith("./Vendor/MSFT/DeviceUpdate/UserInstallableContent/"))
				{
					return false;
				}
				this.WarningMessageEvent(this.DeviceID, string.Format("Unrecognized CSP Uri: {0}", locUri));
				return false;
			}
			return comparable != null && this.Compare(comparable, expected, comparison);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000033B0 File Offset: 0x000015B0
		public bool EvaluateProcessor(string architectureValueString)
		{
			IComparable comparable = int.Parse(architectureValueString);
			CpuId cpuId = this.CpuId;
			IComparable comparable2;
			if (cpuId != CpuId.X86)
			{
				if (cpuId != CpuId.ARM)
				{
					throw new Exception(string.Format("Unrecognized ProcessArchitecture: {0}", this.CpuId));
				}
				comparable2 = 5;
			}
			else
			{
				comparable2 = 0;
			}
			return comparable2 == comparable;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003410 File Offset: 0x00001610
		public bool EvaluateWindowsVersion(string majorVersionValue, string minorVersionValue, string buildNumber, string comparison)
		{
			IComparable osVersion = this.OsVersion;
			IComparable actual = string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				majorVersionValue,
				minorVersionValue,
				"0",
				buildNumber
			});
			return this.Compare(actual, osVersion, comparison);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003458 File Offset: 0x00001658
		private void GetDevicePackagesInfo(string locUri, string expectedValueString, out IComparable expectedValue, out IComparable actualValue)
		{
			expectedValue = Version.Parse(expectedValueString);
			locUri = locUri.Replace("./Vendor/MSFT/DevicePackagesInfo/", "");
			string[] array = locUri.Split("/".ToCharArray());
			string key = array[0];
			string key2 = array[1];
			string a = array[2];
			if (!(a == "PackageVersion"))
			{
				throw new Exception(string.Format("Unrecognized DevicePackagesInfo node: {0}", array[2]));
			}
			if (this.installedPackageVersions.ContainsKey(key) && this.installedPackageVersions[key].ContainsKey(key2))
			{
				actualValue = this.installedPackageVersions[key][key2];
				return;
			}
			actualValue = new Version("0.0.0.0");
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003504 File Offset: 0x00001704
		private bool Compare(IComparable actual, IComparable expected, string comparison)
		{
			if (comparison == "LessThan")
			{
				return actual.CompareTo(expected) < 0;
			}
			if (comparison == "LessThanOrEqualTo")
			{
				return actual.CompareTo(expected) <= 0;
			}
			if (comparison == "EqualTo")
			{
				return actual.CompareTo(expected) == 0;
			}
			if (comparison == "GreaterThanOrEqualTo")
			{
				return actual.CompareTo(expected) >= 0;
			}
			if (comparison == "GreaterThan")
			{
				return actual.CompareTo(expected) > 0;
			}
			if (!(comparison == "EndsWith"))
			{
				throw new Exception(string.Format("Unrecognized applicability rule comparison: {0}", comparison));
			}
			return actual.ToString().EndsWith(expected.ToString());
		}

		// Token: 0x04000005 RID: 5
		private const string InstalledPackagesCSV = "InstalledPackages.csv";

		// Token: 0x04000006 RID: 6
		private const string OemInputXml = "OemInput.xml";

		// Token: 0x04000007 RID: 7
		private const string UsoTroubleshootingReg = "UsoTroubleshooting.reg";

		// Token: 0x04000008 RID: 8
		private const string DuTroubleshootingReg = "DuTroubleshooting.reg";

		// Token: 0x04000009 RID: 9
		private const string VersionsKey = "HKEY_LOCAL_MACHINE\\System\\Versions";

		// Token: 0x0400000A RID: 10
		private const string DeviceTargetingInfoKey = "HKEY_LOCAL_MACHINE\\System\\Platform\\DeviceTargetingInfo";

		// Token: 0x0400000B RID: 11
		private const string SettingsKey = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\DeviceUpdate\\Agent\\Settings";

		// Token: 0x0400000C RID: 12
		private const string ProtocolKey = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\DeviceUpdate\\Agent\\Protocol";

		// Token: 0x0400000D RID: 13
		private const string LanguagePkgPrefix = "microsoft.mainos.production_lang_";

		// Token: 0x0400000E RID: 14
		private Dictionary<string, Dictionary<string, string>> registryValues = new Dictionary<string, Dictionary<string, string>>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x0400000F RID: 15
		private Dictionary<string, Dictionary<string, Version>> installedPackageVersions = new Dictionary<string, Dictionary<string, Version>>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04000010 RID: 16
		private string tempDir;

		// Token: 0x02000065 RID: 101
		// (Invoke) Token: 0x06000316 RID: 790
		public delegate void MessageCallback(string deviceID, string message);
	}
}
