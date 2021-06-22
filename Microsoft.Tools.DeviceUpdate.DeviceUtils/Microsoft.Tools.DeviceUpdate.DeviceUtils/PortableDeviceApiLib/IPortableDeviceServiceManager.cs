using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x02000025 RID: 37
	[CompilerGenerated]
	[Guid("A8ABC4E9-A84A-47A9-80B3-C5D9B172A961")]
	[InterfaceType(1)]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDeviceServiceManager
	{
		// Token: 0x060001A1 RID: 417
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDeviceServices([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPnPDeviceID, [In] ref Guid guidServiceCategory, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] [In] [Out] string[] pServices, [In] [Out] ref uint pcServices);
	}
}
