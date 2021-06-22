using System;
using System.Collections.Generic;

namespace Microsoft.WindowsPhone.ImageUpdate.PkgCommon
{
	// Token: 0x02000029 RID: 41
	public interface IDiffPkg : IPkgInfo
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D2 RID: 466
		IEnumerable<IDiffEntry> DiffFiles { get; }
	}
}
