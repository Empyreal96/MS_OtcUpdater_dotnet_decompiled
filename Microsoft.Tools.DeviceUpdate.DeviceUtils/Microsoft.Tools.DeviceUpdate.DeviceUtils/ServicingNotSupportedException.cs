using System;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000017 RID: 23
	public class ServicingNotSupportedException : DeviceException
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000F680 File Offset: 0x0000D880
		public ServicingNotSupportedException() : base("Servicing is not supported on this build")
		{
		}
	}
}
