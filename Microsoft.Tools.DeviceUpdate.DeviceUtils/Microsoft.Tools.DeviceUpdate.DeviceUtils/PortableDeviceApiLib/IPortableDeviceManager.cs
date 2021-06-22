using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x02000021 RID: 33
	[CompilerGenerated]
	[InterfaceType(1)]
	[Guid("A1567595-4C2F-4574-A6FA-ECEF917B9A40")]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDeviceManager
	{
		// Token: 0x06000195 RID: 405
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDevices([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] [In] [Out] string[] pPnPDeviceIDs, [In] [Out] ref uint pcPnPDeviceIDs);

		// Token: 0x06000196 RID: 406
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RefreshDeviceList();
	}
}
