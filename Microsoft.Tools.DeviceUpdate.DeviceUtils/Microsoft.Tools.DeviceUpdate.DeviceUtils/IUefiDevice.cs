using System;
using FFUComponents;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x0200000F RID: 15
	public interface IUefiDevice : IDevicePropertyCollection, IDisposable
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AA RID: 170
		Guid DeviceUniqueId { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AB RID: 171
		[DeviceProperty("uniqueID")]
		string UniqueID { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AC RID: 172
		[DeviceProperty("uefiName")]
		string UefiName { get; }

		// Token: 0x060000AD RID: 173
		void FlashFFU(string path, bool optimize, EventHandler<ProgressEventArgs> progressEventHandler);

		// Token: 0x060000AE RID: 174
		void WriteWim(string path, EventHandler<ProgressEventArgs> progressEventHandler);

		// Token: 0x060000AF RID: 175
		void SkipFFU();

		// Token: 0x060000B0 RID: 176
		void ReadPartition(string partition, out byte[] data);

		// Token: 0x060000B1 RID: 177
		void WritePartition(string partition, byte[] data);

		// Token: 0x060000B2 RID: 178
		void ClearPlatformIDOverride();

		// Token: 0x060000B3 RID: 179
		string GetServicingLogs(string path);

		// Token: 0x060000B4 RID: 180
		string GetFlashingLogs(string path);

		// Token: 0x060000B5 RID: 181
		void EnterMassStorage();

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B6 RID: 182
		string FFUDeviceType { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B7 RID: 183
		string FFUServicingLogsAvailable { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B8 RID: 184
		string FFUFlashingLogsAvailable { get; }
	}
}
