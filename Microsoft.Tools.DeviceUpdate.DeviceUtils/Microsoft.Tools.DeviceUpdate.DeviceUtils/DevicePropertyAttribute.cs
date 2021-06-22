using System;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000005 RID: 5
	public class DevicePropertyAttribute : Attribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000D28D File Offset: 0x0000B48D
		public DevicePropertyAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000D29C File Offset: 0x0000B49C
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		public string Name { get; private set; }
	}
}
