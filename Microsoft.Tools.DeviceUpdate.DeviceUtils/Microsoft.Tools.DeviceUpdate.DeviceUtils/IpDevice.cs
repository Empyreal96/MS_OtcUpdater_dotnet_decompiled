using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Deployment.Compression.Cab;
using Microsoft.Tools.Connectivity;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x0200000C RID: 12
	public class IpDevice : Disposable, IIpDevice, IDisposable, IUpdateableDevice, IDevicePropertyCollection
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000046 RID: 70 RVA: 0x0000DA40 File Offset: 0x0000BC40
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x0000DA78 File Offset: 0x0000BC78
		public event MessageHandler NormalMessageEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000048 RID: 72 RVA: 0x0000DAB0 File Offset: 0x0000BCB0
		// (remove) Token: 0x06000049 RID: 73 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		public event MessageHandler WarningMessageEvent;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600004A RID: 74 RVA: 0x0000DB20 File Offset: 0x0000BD20
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x0000DB58 File Offset: 0x0000BD58
		public event MessageHandler ProgressEvent;

		// Token: 0x0600004C RID: 76 RVA: 0x0000DB90 File Offset: 0x0000BD90
		public IpDevice(DiscoveredDeviceInfo deviceInfo, IpDeviceCommunicator deviceCommunicator)
		{
			this.deviceInfo = deviceInfo;
			this.DeviceCommunicator = deviceCommunicator;
			this.isServicingSupported = deviceCommunicator.IsServicingSupported();
			this.DeviceUniqueId = deviceInfo.UniqueId;
			this.Manufacturer = deviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_MANUFACTURER, null);
			this.Branch = deviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_BUILD_BRANCH, null);
			this.CoreSysBuildNumber = deviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_BUILD_NUMBER, null);
			this.BuildTimeStamp = deviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_BUILD_TIMESTAMP, null);
			this.OemDeviceName = deviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_OEM_DEVICE_NAME, null);
			this.UefiName = deviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_UEFI_NAME, null);
			this.FirmwareVersion = deviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_FIRMWARE_VERSION, null);
			this.GetDeviceSecurityStatus();
			try
			{
				this.CoreSysBuildRevision = deviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_BUILD_REVISION, null);
			}
			catch
			{
				this.CoreSysBuildRevision = "?";
			}
			try
			{
				this.SerialNumber = deviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_SERIAL_NUMBER, null);
			}
			catch
			{
				this.SerialNumber = "";
			}
			this.ImageTargetingType = "";
			this.FeedbackId = "";
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public virtual string GetProperty(string name)
		{
			return PropertyDeviceCollection.GetPropertyString(this, name);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000DCF1 File Offset: 0x0000BEF1
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000DCF9 File Offset: 0x0000BEF9
		public Guid DeviceUniqueId { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000DD02 File Offset: 0x0000BF02
		public string Model
		{
			get
			{
				return this.deviceInfo.Name;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000DD0F File Offset: 0x0000BF0F
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000DD17 File Offset: 0x0000BF17
		public IpDeviceCommunicator DeviceCommunicator { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000DD20 File Offset: 0x0000BF20
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000DD28 File Offset: 0x0000BF28
		public virtual string Branch { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000DD31 File Offset: 0x0000BF31
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000DD39 File Offset: 0x0000BF39
		public virtual string CoreSysBuildNumber { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000DD42 File Offset: 0x0000BF42
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000DD4A File Offset: 0x0000BF4A
		public virtual string CoreSysBuildRevision { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000DD53 File Offset: 0x0000BF53
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000DD5B File Offset: 0x0000BF5B
		public virtual string BuildTimeStamp { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000DD64 File Offset: 0x0000BF64
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		public virtual string ImageTargetingType { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000DD75 File Offset: 0x0000BF75
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000DD7D File Offset: 0x0000BF7D
		public virtual string FeedbackId { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000DD86 File Offset: 0x0000BF86
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000DD8E File Offset: 0x0000BF8E
		public virtual string FirmwareVersion { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000DD97 File Offset: 0x0000BF97
		public string BuildString
		{
			get
			{
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					this.CoreSysBuildNumber,
					this.CoreSysBuildRevision,
					this.Branch,
					this.BuildTimeStamp
				});
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000DDD5 File Offset: 0x0000BFD5
		public string SerialNumber { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000DDDE File Offset: 0x0000BFDE
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000DDE6 File Offset: 0x0000BFE6
		public string Manufacturer { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000DDEF File Offset: 0x0000BFEF
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000DDF7 File Offset: 0x0000BFF7
		public string OemDeviceName { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000DE00 File Offset: 0x0000C000
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000DE08 File Offset: 0x0000C008
		public string UefiName { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000DE11 File Offset: 0x0000C011
		public string BatteryLevel
		{
			get
			{
				return this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_COMMAND_GET_BATTERY_LEVEL, null);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000DE24 File Offset: 0x0000C024
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000DE2C File Offset: 0x0000C02C
		public bool IsFused { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000DE35 File Offset: 0x0000C035
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000DE3D File Offset: 0x0000C03D
		public bool IsSecureBootCapable { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000DE46 File Offset: 0x0000C046
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000DE4E File Offset: 0x0000C04E
		public bool IsSecureBootEnabled { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000DE57 File Offset: 0x0000C057
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000DE5F File Offset: 0x0000C05F
		public bool IsOSVolumeEncrypted { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000DE68 File Offset: 0x0000C068
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000DE70 File Offset: 0x0000C070
		public bool IsDataVolumeEncrypted { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000DE79 File Offset: 0x0000C079
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000DE81 File Offset: 0x0000C081
		public Guid SecureBootBasePolicy { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000DE8A File Offset: 0x0000C08A
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000DE92 File Offset: 0x0000C092
		public int SecureBootBasePolicyVersion { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000DE9B File Offset: 0x0000C09B
		public Guid[] ActiveSupplementalPolicies
		{
			get
			{
				return this.activeSupplementalPolicies;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		public string UpdateState
		{
			get
			{
				try
				{
					this.UpdateStatus();
				}
				catch
				{
				}
				return this.updateState;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public string DuResult
		{
			get
			{
				try
				{
					this.UpdateStatus();
				}
				catch
				{
				}
				return this.duResult;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000DF04 File Offset: 0x0000C104
		private void UpdateStatus()
		{
			string text;
			if (IpDeviceCommunicator.IpDeviceApplyUpdateCommand.ParseResponse(this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.APPLY_UPDATE_COMMAND_STATUS, null), out text, out this.updateState, out this.updateProgress))
			{
				this.duResult = text;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000DF3E File Offset: 0x0000C13E
		private void ClearStatus()
		{
			this.updateState = "";
			this.updateProgress = "";
			this.duResult = "";
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000DF64 File Offset: 0x0000C164
		private void GetDeviceSecurityStatus()
		{
			List<Guid> list = new List<Guid>();
			Regex regex = new Regex("(?x)        # Ignore unescaped whitespace\r\n                ^0x(\\d+)                        # Security state bitmap\r\n                (?: ;                           # Start of the optional components\r\n                    ([{][A-Fa-f0-9]{8}[-][A-Fa-f0-9]{4}[-][A-Fa-f0-9]{4}[-][A-Fa-f0-9]{4}[-][A-Fa-f0-9]{12}[}])\\#(\\d+)  # Base publisher GUID and version\r\n                    (?:                         # Supplemental policies\r\n                        , \r\n                        ([{][A-Fa-f0-9]{8}[-][A-Fa-f0-9]{4}[-][A-Fa-f0-9]{4}[-][A-Fa-f0-9]{4}[-][A-Fa-f0-9]{12}[}])\r\n                    )*\r\n                )?$\r\n                ", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			try
			{
				string input = this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_COMMAND_GET_SECURITY_STATE, null);
				Match match = regex.Match(input);
				if (match.Success)
				{
					IpDevice.SecurityBitmapMask securityBitmapMask = (IpDevice.SecurityBitmapMask)uint.Parse(match.Groups[1].Captures[0].Value, NumberStyles.AllowHexSpecifier);
					this.IsFused = securityBitmapMask.HasFlag(IpDevice.SecurityBitmapMask.DeviceFused);
					this.IsSecureBootCapable = securityBitmapMask.HasFlag(IpDevice.SecurityBitmapMask.SecureBootCapable);
					this.IsSecureBootEnabled = securityBitmapMask.HasFlag(IpDevice.SecurityBitmapMask.SecureBootEnabled);
					this.IsOSVolumeEncrypted = securityBitmapMask.HasFlag(IpDevice.SecurityBitmapMask.OSVolumeEncrypted);
					this.IsDataVolumeEncrypted = securityBitmapMask.HasFlag(IpDevice.SecurityBitmapMask.DataVolumeEncrypted);
					if (match.Groups.Count > 3)
					{
						this.SecureBootBasePolicy = new Guid(match.Groups[2].Captures[0].Value);
						this.SecureBootBasePolicyVersion = int.Parse(match.Groups[3].Captures[0].Value);
						if (match.Groups.Count > 4)
						{
							for (int i = 0; i < match.Groups[4].Captures.Count; i++)
							{
								string value = match.Groups[4].Captures[i].Value;
								list.Add(new Guid(value));
							}
						}
					}
				}
			}
			catch
			{
				this.IsFused = false;
				this.IsSecureBootCapable = true;
				this.IsSecureBootEnabled = false;
				this.IsOSVolumeEncrypted = false;
				this.IsDataVolumeEncrypted = false;
			}
			this.activeSupplementalPolicies = list.ToArray();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000E160 File Offset: 0x0000C360
		public void RebootToUefi()
		{
			try
			{
				this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_COMMAND_REBOOT_TO_UEFI, null);
			}
			catch
			{
				this.OnWarningMessageEvent("Error communicating with the device. To flash, please manually boot to FFU mode by power cycling and holding volume up.");
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public void RebootToTarget(uint target)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000E1A7 File Offset: 0x0000C3A7
		public InstalledPackageInfo[] InstalledPackages
		{
			get
			{
				if (this.installedPackages == null)
				{
					this.installedPackages = this.GetInstalledPackages();
				}
				return this.installedPackages;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		private InstalledPackageInfo[] GetInstalledPackages()
		{
			this.OnNormalMessageEvent("Retrieving list of installed packages...");
			string[] array = this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_COMMAND_GET_INSTALLED_PACKAGES, null).Split(new char[]
			{
				';'
			});
			List<InstalledPackageInfo> list = new List<InstalledPackageInfo>();
			foreach (string text in array)
			{
				string[] array3 = text.Split(new char[]
				{
					','
				});
				if (3 != array3.Length)
				{
					throw new DeviceException(string.Format("Package string has invalid format: {0}", text));
				}
				InstalledPackageInfo item = new InstalledPackageInfo(array3[0], array3[1], array3[2]);
				list.Add(item);
			}
			this.OnNormalMessageEvent("Retrieved list of installed packages");
			if (list.Count == 0)
			{
				throw new DeviceException("Device package count is 0");
			}
			return list.ToArray();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public void StartDeviceUpdateScan(uint throttle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000E280 File Offset: 0x0000C480
		public void StartDeviceUpdateOtcScan()
		{
			if (!this.isServicingSupported)
			{
				throw new ServicingNotSupportedException();
			}
			this.ClearStatus();
			this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.APPLY_UPDATE_COMMAND_GET_REQUIRED_PKGS, "C:\\Data\\ProgramData\\Update");
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		public void InitiateDuInstall()
		{
			if (!this.isServicingSupported)
			{
				throw new ServicingNotSupportedException();
			}
			this.ClearStatus();
			try
			{
				if (this.useUUPUpdateCommands)
				{
					this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.APPLY_UPDATE_COMMAND_INSTALL_WITH_AL, "C:\\Data\\ProgramData\\Update");
				}
				else
				{
					this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.APPLY_UPDATE_COMMAND_COMMIT, null);
				}
			}
			catch (Exception ex)
			{
				try
				{
					this.UpdateStatus();
				}
				catch (Exception)
				{
					throw ex;
				}
				if (this.duResult != "")
				{
					throw ex;
				}
			}
			try
			{
				while ((this.updateState == "" || this.updateState == "UpdateStateIdle") && this.duResult == "")
				{
					Thread.Sleep(1000);
					this.UpdateStatus();
				}
				if (this.useUUPUpdateCommands)
				{
					this.OnProgressEvent("Install has started. Cleaning up staging folder to save disk space.");
					this.CleanupUpdateFolder();
				}
				while (this.DeviceCommunicator.IsIpDevice() && this.updateState != "UpdateStateIdle" && this.updateState != "UpdateStateUpdateComplete" && this.duResult == "")
				{
					if (this.updateProgress == "100")
					{
						this.OnProgressEvent("Completing remaining tasks before rebooting. This will take several minutes.");
					}
					else if (this.updateProgress != "")
					{
						this.OnProgressEvent(string.Format("Install progress: {0}", this.updateProgress));
					}
					Thread.Sleep(1000);
					this.UpdateStatus();
				}
			}
			catch (Exception)
			{
			}
			this.useUUPUpdateCommands = false;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000E47C File Offset: 0x0000C67C
		public void ClearDuStagingDirectory()
		{
			if (!this.isServicingSupported)
			{
				throw new ServicingNotSupportedException();
			}
			this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.APPLY_UPDATE_COMMAND_CLEAR, null);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000E49E File Offset: 0x0000C69E
		public void GetActionList(string path)
		{
			if (!this.isServicingSupported)
			{
				throw new ServicingNotSupportedException();
			}
			this.DeviceCommunicator.GetFile("C:\\Data\\ProgramData\\Update\\actionlist.xml", path);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		public void GetDuDiagnostics(string path)
		{
			this.OnNormalMessageEvent("Collecting log files...");
			CabInfo cabInfo = new CabInfo(path);
			List<string> list = new List<string>();
			string text = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			DirectoryInfo directoryInfo = Directory.CreateDirectory(text);
			try
			{
				try
				{
					this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.APPLY_UPDATE_COMMAND_COLLECT_LOGS, null);
				}
				catch
				{
					this.OnWarningMessageEvent("Device does not support log collection");
					return;
				}
				string text2 = Path.Combine(Path.GetTempPath(), Path.GetFileName("C:\\Data\\ProgramData\\USOShared\\UsoLogs.dudiag"));
				this.DeviceCommunicator.GetFile("C:\\Data\\ProgramData\\USOShared\\UsoLogs.dudiag", text2);
				try
				{
					new CabInfo(text2).Unpack(text);
				}
				finally
				{
					File.Delete(text2);
				}
				try
				{
					string localFilePath = Path.Combine(text, Path.GetFileName("C:\\Data\\ProgramData\\Update\\ApplyUpdate.log"));
					this.DeviceCommunicator.GetFile("C:\\Data\\ProgramData\\Update\\ApplyUpdate.log", localFilePath);
				}
				catch (DeviceException ex)
				{
					if (!(ex.InnerException is FileNotFoundException))
					{
						throw;
					}
				}
				using (StreamWriter streamWriter = new StreamWriter(Path.Combine(text, "DeviceProperties.csv")))
				{
					SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>();
					PropertyDeviceCollection.GetProperties(this, ref sortedDictionary);
					foreach (KeyValuePair<string, string> keyValuePair in sortedDictionary)
					{
						streamWriter.WriteLine(string.Format("{0},{1}", keyValuePair.Key, keyValuePair.Value));
					}
				}
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					list.Add(fileInfo.FullName);
				}
				if (list.Count > 0)
				{
					this.OnNormalMessageEvent(string.Format("Copying log files to {0}", path));
					cabInfo.PackFiles(null, list, null);
				}
				else
				{
					this.OnWarningMessageEvent("No log files found");
				}
			}
			finally
			{
				directoryInfo.Delete(true);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public void GetPackageInfo(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000E710 File Offset: 0x0000C910
		public void SendIuPackage(string path)
		{
			if (!this.isServicingSupported)
			{
				throw new ServicingNotSupportedException();
			}
			string text = string.Format("{0}\\{1}", "C:\\Data\\ProgramData\\Update", Path.GetFileName(path));
			path = this.NetworkSharePathToUNC(path);
			this.DeviceCommunicator.PutFile(text, path);
			if (!this.useUUPUpdateCommands)
			{
				try
				{
					this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.APPLY_UPDATE_COMMAND_STAGE, text);
				}
				finally
				{
					this.DeviceCommunicator.DeleteFile(text);
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000E790 File Offset: 0x0000C990
		public void SendIuPackage(string path, string name)
		{
			if (!this.isServicingSupported)
			{
				throw new ServicingNotSupportedException();
			}
			string text = string.Format("{0}\\{1}", "C:\\Data\\ProgramData\\Update", name);
			path = this.NetworkSharePathToUNC(path);
			this.DeviceCommunicator.PutFile(text, path);
			if (!this.useUUPUpdateCommands)
			{
				try
				{
					this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.APPLY_UPDATE_COMMAND_STAGE, text);
				}
				finally
				{
					this.DeviceCommunicator.DeleteFile(text);
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000E80C File Offset: 0x0000CA0C
		public void SendCompositionDB(string path)
		{
			if (!this.isServicingSupported)
			{
				throw new ServicingNotSupportedException();
			}
			string remoteFilePath = string.Format("{0}\\{1}", "C:\\Data\\ProgramData\\Update", Path.GetFileName(path));
			path = this.NetworkSharePathToUNC(path);
			this.DeviceCommunicator.PutFile(remoteFilePath, path);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000E853 File Offset: 0x0000CA53
		public void SendUpdateAgent(string path)
		{
			if (!this.isServicingSupported)
			{
				throw new ServicingNotSupportedException();
			}
			path = this.NetworkSharePathToUNC(path);
			this.DeviceCommunicator.PutFile("C:\\Data\\ProgramData\\Update\\UpdateAgent.cab", path);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000E880 File Offset: 0x0000CA80
		public void SendIuPackage(Stream stream)
		{
			string path = null;
			try
			{
				path = Path.GetTempFileName();
				using (FileStream fileStream = File.OpenWrite(path))
				{
					stream.CopyTo(fileStream);
				}
				this.SendIuPackage(path);
			}
			finally
			{
				File.Delete(path);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000E8DC File Offset: 0x0000CADC
		public void SetTime(DateTime time)
		{
			string args = string.Format("{0} {1} {2} {3} {4} {5} {6}", new object[]
			{
				time.Year,
				time.Month,
				time.Day,
				time.Hour,
				time.Minute,
				time.Second,
				time.Millisecond
			});
			try
			{
				this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_COMMAND_SET_TIME, args);
			}
			catch (DeviceException ex)
			{
				if (!(ex.InnerException is ArgumentException))
				{
					throw;
				}
				args = string.Format("{0} {1} {2} {3} {4} {5} {6} {7}", new object[]
				{
					time.Year,
					time.Month,
					time.DayOfWeek,
					time.Day,
					time.Hour,
					time.Minute,
					time.Second,
					time.Millisecond
				});
				this.DeviceCommunicator.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_COMMAND_SET_TIME, args);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000EA3C File Offset: 0x0000CC3C
		public void UseUUPCommands(bool useUUP)
		{
			this.useUUPUpdateCommands = useUUP;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000EA45 File Offset: 0x0000CC45
		public void CleanupUpdateFolder()
		{
			this.DeviceCommunicator.CleanupUpdateFolder();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000EA54 File Offset: 0x0000CC54
		protected override void DisposeManaged()
		{
			try
			{
				this.DeviceCommunicator.Dispose();
			}
			catch
			{
			}
			base.DisposeManaged();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000EA88 File Offset: 0x0000CC88
		protected void OnProgressEvent(string message)
		{
			if (this.ProgressEvent != null)
			{
				this.ProgressEvent(this, new MessageArgs(message));
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		protected void OnNormalMessageEvent(string message)
		{
			if (this.NormalMessageEvent != null)
			{
				this.NormalMessageEvent(this, new MessageArgs(message));
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		protected void OnWarningMessageEvent(string message)
		{
			if (this.WarningMessageEvent != null)
			{
				this.WarningMessageEvent(this, new MessageArgs(message));
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000EADC File Offset: 0x0000CCDC
		private string NetworkSharePathToUNC(string path)
		{
			if (new Regex("^\\\\\\\\[\\w]").IsMatch(path))
			{
				path = path.Insert(2, "?\\UNC\\");
			}
			return path;
		}

		// Token: 0x040002A7 RID: 679
		private DiscoveredDeviceInfo deviceInfo;

		// Token: 0x040002A8 RID: 680
		private string updateState = "";

		// Token: 0x040002A9 RID: 681
		private string updateProgress = "";

		// Token: 0x040002AA RID: 682
		private string duResult = "";

		// Token: 0x040002AB RID: 683
		private InstalledPackageInfo[] installedPackages;

		// Token: 0x040002AC RID: 684
		private Guid[] activeSupplementalPolicies;

		// Token: 0x040002AD RID: 685
		private bool isServicingSupported;

		// Token: 0x040002AE RID: 686
		private bool useUUPUpdateCommands;

		// Token: 0x02000034 RID: 52
		[Flags]
		private enum SecurityBitmapMask
		{
			// Token: 0x04000368 RID: 872
			None = 0,
			// Token: 0x04000369 RID: 873
			DeviceFused = 1,
			// Token: 0x0400036A RID: 874
			SecureBootCapable = 2,
			// Token: 0x0400036B RID: 875
			SecureBootEnabled = 4,
			// Token: 0x0400036C RID: 876
			OSVolumeEncrypted = 256,
			// Token: 0x0400036D RID: 877
			DataVolumeEncrypted = 512
		}
	}
}
