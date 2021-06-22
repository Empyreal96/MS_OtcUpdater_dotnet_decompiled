using System;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x0200000A RID: 10
	public interface IIpDevice : IDisposable, IUpdateableDevice, IDevicePropertyCollection
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35
		[DeviceProperty("deviceUniqueId")]
		Guid DeviceUniqueId { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36
		[DeviceProperty("model")]
		string Model { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37
		[DeviceProperty("branch")]
		string Branch { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38
		[DeviceProperty("coreSysBuildNumber")]
		string CoreSysBuildNumber { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39
		[DeviceProperty("coreSysBuildRevision")]
		string CoreSysBuildRevision { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40
		[DeviceProperty("buildTimeStamp")]
		string BuildTimeStamp { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41
		[DeviceProperty("imageTargetingType")]
		string ImageTargetingType { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42
		[DeviceProperty("feedbackId")]
		string FeedbackId { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002B RID: 43
		[DeviceProperty("firmwareVersion")]
		string FirmwareVersion { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44
		[DeviceProperty("serialNumber")]
		string SerialNumber { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002D RID: 45
		[DeviceProperty("manufacturer")]
		string Manufacturer { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002E RID: 46
		[DeviceProperty("oemDeviceName")]
		string OemDeviceName { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002F RID: 47
		[DeviceProperty("uefiName")]
		string UefiName { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000030 RID: 48
		[DeviceProperty("updateState")]
		string UpdateState { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49
		[DeviceProperty("duResult")]
		string DuResult { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000032 RID: 50
		[DeviceProperty("batteryLevel")]
		string BatteryLevel { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000033 RID: 51
		[DeviceProperty("isfused")]
		bool IsFused { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000034 RID: 52
		[DeviceProperty("issecurebootcapable")]
		bool IsSecureBootCapable { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000035 RID: 53
		[DeviceProperty("issecurebootenabled")]
		bool IsSecureBootEnabled { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000036 RID: 54
		[DeviceProperty("isosvolumeencrypted")]
		bool IsOSVolumeEncrypted { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55
		[DeviceProperty("isdatavolumeencrypted")]
		bool IsDataVolumeEncrypted { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56
		[DeviceProperty("securebootbasepolicy")]
		Guid SecureBootBasePolicy { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000039 RID: 57
		[DeviceProperty("securebootbasepolicyversion")]
		int SecureBootBasePolicyVersion { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003A RID: 58
		[DeviceProperty("activesupplementalpolicies")]
		Guid[] ActiveSupplementalPolicies { get; }

		// Token: 0x0600003B RID: 59
		void SetTime(DateTime time);

		// Token: 0x0600003C RID: 60
		void UseUUPCommands(bool useUUP);

		// Token: 0x0600003D RID: 61
		void CleanupUpdateFolder();
	}
}
