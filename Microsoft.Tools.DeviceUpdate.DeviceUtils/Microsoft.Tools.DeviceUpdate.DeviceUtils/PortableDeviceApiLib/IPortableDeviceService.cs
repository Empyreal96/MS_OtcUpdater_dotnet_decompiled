using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x02000024 RID: 36
	[CompilerGenerated]
	[Guid("D3BD3A44-D7B5-40A9-98B7-2FA4D01DEC08")]
	[InterfaceType(1)]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDeviceService
	{
		// Token: 0x0600019B RID: 411
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Open([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPnPServiceID, [MarshalAs(UnmanagedType.Interface)] [In] IPortableDeviceValues pClientInfo);

		// Token: 0x0600019C RID: 412
		void _VtblGap1_1();

		// Token: 0x0600019D RID: 413
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Content([MarshalAs(UnmanagedType.Interface)] out IPortableDeviceContent2 ppContent);

		// Token: 0x0600019E RID: 414
		void _VtblGap2_2();

		// Token: 0x0600019F RID: 415
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Close();

		// Token: 0x060001A0 RID: 416
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetServiceObjectID([MarshalAs(UnmanagedType.LPWStr)] out string ppszServiceObjectID);
	}
}
