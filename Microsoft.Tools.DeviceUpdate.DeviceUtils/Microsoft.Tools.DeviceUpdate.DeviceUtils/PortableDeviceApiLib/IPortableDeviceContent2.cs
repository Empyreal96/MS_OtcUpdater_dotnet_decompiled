using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x0200001F RID: 31
	[CompilerGenerated]
	[InterfaceType(1)]
	[Guid("9B4ADD96-F6BF-4034-8708-ECA72BF10554")]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDeviceContent2 : IPortableDeviceContent
	{
		// Token: 0x06000191 RID: 401
		void _VtblGap1_1();

		// Token: 0x06000192 RID: 402
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Properties([MarshalAs(UnmanagedType.Interface)] out IPortableDeviceProperties ppProperties);
	}
}
