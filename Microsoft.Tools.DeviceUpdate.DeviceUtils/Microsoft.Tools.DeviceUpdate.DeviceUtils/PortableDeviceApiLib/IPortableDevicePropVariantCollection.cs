using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x02000022 RID: 34
	[CompilerGenerated]
	[InterfaceType(1)]
	[Guid("89B2E422-4F1B-4316-BCEF-A44AFEA83EB3")]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDevicePropVariantCollection
	{
		// Token: 0x06000197 RID: 407
		void _VtblGap1_2();

		// Token: 0x06000198 RID: 408
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Add([In] ref tag_inner_PROPVARIANT pValue);
	}
}
