using System;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class DeviceException : Exception
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000D27A File Offset: 0x0000B47A
		public DeviceException(string message) : base(message)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000D283 File Offset: 0x0000B483
		public DeviceException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
