using System;
using System.IO;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000010 RID: 16
	public interface IUpdateableDevice : IDevicePropertyCollection, IDisposable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000B9 RID: 185
		// (remove) Token: 0x060000BA RID: 186
		event MessageHandler NormalMessageEvent;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000BB RID: 187
		// (remove) Token: 0x060000BC RID: 188
		event MessageHandler WarningMessageEvent;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000BD RID: 189
		// (remove) Token: 0x060000BE RID: 190
		event MessageHandler ProgressEvent;

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BF RID: 191
		InstalledPackageInfo[] InstalledPackages { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C0 RID: 192
		[DeviceProperty("buildString")]
		string BuildString { get; }

		// Token: 0x060000C1 RID: 193
		void RebootToUefi();

		// Token: 0x060000C2 RID: 194
		void RebootToTarget(uint Target);

		// Token: 0x060000C3 RID: 195
		void StartDeviceUpdateScan(uint throttle);

		// Token: 0x060000C4 RID: 196
		void StartDeviceUpdateOtcScan();

		// Token: 0x060000C5 RID: 197
		void InitiateDuInstall();

		// Token: 0x060000C6 RID: 198
		void ClearDuStagingDirectory();

		// Token: 0x060000C7 RID: 199
		void GetDuDiagnostics(string path);

		// Token: 0x060000C8 RID: 200
		void GetActionList(string path);

		// Token: 0x060000C9 RID: 201
		void GetPackageInfo(string path);

		// Token: 0x060000CA RID: 202
		void SendIuPackage(string path);

		// Token: 0x060000CB RID: 203
		void SendIuPackage(string path, string name);

		// Token: 0x060000CC RID: 204
		void SendIuPackage(Stream stream);

		// Token: 0x060000CD RID: 205
		void SendCompositionDB(string path);

		// Token: 0x060000CE RID: 206
		void SendUpdateAgent(string path);
	}
}
