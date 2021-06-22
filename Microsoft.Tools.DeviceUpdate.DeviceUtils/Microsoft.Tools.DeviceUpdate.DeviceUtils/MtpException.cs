using System;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class MtpException : DeviceException
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		public MtpException() : base("MTP communication error\n- This may be recoverable by simply reconnecting the device.\n- If it occurred during a multi-step operation, your device may be in an unknown state and need to be reflashed.")
		{
		}
	}
}
