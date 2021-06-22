using System;
using Microsoft.Tools.DeviceUpdate.DeviceUtils;

namespace OtcUpdater
{
	// Token: 0x02000003 RID: 3
	internal interface IView
	{
		// Token: 0x06000003 RID: 3
		void Restore();

		// Token: 0x06000004 RID: 4
		void ClearInputBuffer();

		// Token: 0x06000005 RID: 5
		char PollForKey();

		// Token: 0x06000006 RID: 6
		char WaitForKey();

		// Token: 0x06000007 RID: 7
		void LogMessage(string message);

		// Token: 0x06000008 RID: 8
		void LogDeviceMessage(IWpdDevice device, string message);

		// Token: 0x06000009 RID: 9
		void ShowHeader(string header);

		// Token: 0x0600000A RID: 10
		void ShowNormalMessage(string message);

		// Token: 0x0600000B RID: 11
		void ShowWarningMessage(string message);

		// Token: 0x0600000C RID: 12
		void ShowErrorMessage(string message);

		// Token: 0x0600000D RID: 13
		void ShowProgressMessage(string message);

		// Token: 0x0600000E RID: 14
		void ShowDeviceHeader(IWpdDevice device, string header);

		// Token: 0x0600000F RID: 15
		void ShowNormalDeviceMessage(IWpdDevice device, string message);

		// Token: 0x06000010 RID: 16
		void ShowWarningDeviceMessage(IWpdDevice device, string message);

		// Token: 0x06000011 RID: 17
		void ShowErrorDeviceMessage(IWpdDevice device, string message);

		// Token: 0x06000012 RID: 18
		void ShowProgressDeviceMessage(IWpdDevice device, string message);
	}
}
