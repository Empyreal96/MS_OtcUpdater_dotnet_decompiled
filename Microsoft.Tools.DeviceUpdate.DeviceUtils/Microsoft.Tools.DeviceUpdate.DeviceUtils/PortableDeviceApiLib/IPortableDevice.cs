using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x0200001D RID: 29
	[CompilerGenerated]
	[InterfaceType(1)]
	[Guid("625E2DF8-6392-4CF0-9AD1-3CFA5F17775C")]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDevice
	{
		// Token: 0x0600018A RID: 394
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Open([MarshalAs(UnmanagedType.LPWStr)] [In] string pszPnPDeviceID, [MarshalAs(UnmanagedType.Interface)] [In] IPortableDeviceValues pClientInfo);

		// Token: 0x0600018B RID: 395
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SendCommand([In] uint dwFlags, [MarshalAs(UnmanagedType.Interface)] [In] IPortableDeviceValues pParameters, [MarshalAs(UnmanagedType.Interface)] out IPortableDeviceValues ppResults);

		// Token: 0x0600018C RID: 396
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Content([MarshalAs(UnmanagedType.Interface)] out IPortableDeviceContent ppContent);

		// Token: 0x0600018D RID: 397
		void _VtblGap1_2();

		// Token: 0x0600018E RID: 398
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Close();
	}
}
