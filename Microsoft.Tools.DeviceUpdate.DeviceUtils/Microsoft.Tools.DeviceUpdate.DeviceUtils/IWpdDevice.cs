using System;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000011 RID: 17
	public interface IWpdDevice : IUpdateableDevice, IDevicePropertyCollection, IDisposable
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CF RID: 207
		[DeviceProperty("deviceId")]
		string DeviceId { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D0 RID: 208
		[DeviceProperty("model")]
		string Model { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D1 RID: 209
		[DeviceProperty("friendlyName")]
		string FriendlyName { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D2 RID: 210
		[DeviceProperty("isLocked")]
		bool IsLocked { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D3 RID: 211
		[DeviceProperty("isMtpSessionUnlocked")]
		bool IsMtpSessionUnlocked { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D4 RID: 212
		[DeviceProperty("branch")]
		string Branch { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D5 RID: 213
		[DeviceProperty("windowsPhoneBuildNumber")]
		string WindowsPhoneBuildNumber { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D6 RID: 214
		[DeviceProperty("coreSysBuildNumber")]
		string CoreSysBuildNumber { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D7 RID: 215
		[DeviceProperty("buildTimeStamp")]
		string BuildTimeStamp { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D8 RID: 216
		[DeviceProperty("imageTargetingType")]
		string ImageTargetingType { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D9 RID: 217
		[DeviceProperty("feedbackId")]
		string FeedbackId { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000DA RID: 218
		[DeviceProperty("osVersion")]
		string OsVersion { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000DB RID: 219
		[DeviceProperty("firmwareVersion")]
		string FirmwareVersion { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000DC RID: 220
		[DeviceProperty("moId")]
		string MoId { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000DD RID: 221
		[DeviceProperty("serialNumber")]
		string SerialNumber { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DE RID: 222
		[DeviceProperty("manufacturer")]
		string Manufacturer { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000DF RID: 223
		[DeviceProperty("oem")]
		string Oem { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E0 RID: 224
		[DeviceProperty("oemDeviceName")]
		string OemDeviceName { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E1 RID: 225
		[DeviceProperty("resolution")]
		string Resolution { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E2 RID: 226
		[DeviceProperty("uefiName")]
		string UefiName { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E3 RID: 227
		[DeviceProperty("duEngineState")]
		string DuEngineState { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E4 RID: 228
		[DeviceProperty("duUpdateAgentError")]
		string DuUpdateAgentError { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000E5 RID: 229
		[DeviceProperty("duSendIuPackageOptions")]
		string DuSendIuPackageOptions { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000E6 RID: 230
		[DeviceProperty("duResult")]
		string DuResult { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E7 RID: 231
		[DeviceProperty("uniqueID")]
		string UniqueID { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000E8 RID: 232
		[DeviceProperty("duDeviceUpdateResult")]
		string DuDeviceUpdateResult { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E9 RID: 233
		[DeviceProperty("duShellStartReady")]
		string DuShellStartReady { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000EA RID: 234
		[DeviceProperty("wpSerialNumber")]
		string WPSerialNumber { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000EB RID: 235
		[DeviceProperty("duShellApiReady")]
		string DuShellApiReady { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000EC RID: 236
		[DeviceProperty("duTotalStorage")]
		string TotalStorage { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000ED RID: 237
		[DeviceProperty("duTotalRAM")]
		string TotalRAM { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000EE RID: 238
		[DeviceProperty("batteryLife")]
		string BatteryLife { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000EF RID: 239
		[DeviceProperty("osEdition")]
		string OSEdition { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F0 RID: 240
		[DeviceProperty("duCTAC")]
		string DuCTAC { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000F1 RID: 241
		[DeviceProperty("duProductNames")]
		string DuProductNames { get; }
	}
}
