using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x02000023 RID: 35
	[CompilerGenerated]
	[InterfaceType(1)]
	[Guid("7F6D695C-03DF-4439-A809-59266BEEE3A6")]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDeviceProperties
	{
		// Token: 0x06000199 RID: 409
		void _VtblGap1_2();

		// Token: 0x0600019A RID: 410
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetValues([MarshalAs(UnmanagedType.LPWStr)] [In] string pszObjectID, [MarshalAs(UnmanagedType.Interface)] [In] IPortableDeviceKeyCollection pKeys, [MarshalAs(UnmanagedType.Interface)] out IPortableDeviceValues ppValues);
	}
}
