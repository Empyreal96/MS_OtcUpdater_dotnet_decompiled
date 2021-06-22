using System;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000014 RID: 20
	public class MessageArgs : EventArgs
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x0000F401 File Offset: 0x0000D601
		public MessageArgs(string message)
		{
			this.Message = message;
		}

		// Token: 0x040002E5 RID: 741
		public readonly string Message;
	}
}
