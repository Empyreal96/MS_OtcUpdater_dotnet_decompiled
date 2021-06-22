using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x0200001E RID: 30
	[CompilerGenerated]
	[Guid("6A96ED84-7C73-4480-9938-BF5AF477D426")]
	[InterfaceType(1)]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDeviceContent
	{
		// Token: 0x0600018F RID: 399
		void _VtblGap1_1();

		// Token: 0x06000190 RID: 400
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Properties([MarshalAs(UnmanagedType.Interface)] out IPortableDeviceProperties ppProperties);
	}
}
