using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x02000026 RID: 38
	[CompilerGenerated]
	[Guid("6848F6F2-3155-4F86-B6F5-263EEEAB3143")]
	[InterfaceType(1)]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDeviceValues
	{
		// Token: 0x060001A2 RID: 418
		void _VtblGap1_3();

		// Token: 0x060001A3 RID: 419
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetValue([In] ref _tagpropertykey key, out tag_inner_PROPVARIANT pValue);

		// Token: 0x060001A4 RID: 420
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetStringValue([In] ref _tagpropertykey key, [MarshalAs(UnmanagedType.LPWStr)] [In] string Value);

		// Token: 0x060001A5 RID: 421
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetStringValue([In] ref _tagpropertykey key, [MarshalAs(UnmanagedType.LPWStr)] out string pValue);

		// Token: 0x060001A6 RID: 422
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetUnsignedIntegerValue([In] ref _tagpropertykey key, [In] uint Value);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetUnsignedIntegerValue([In] ref _tagpropertykey key, out uint pValue);

		// Token: 0x060001A8 RID: 424
		void _VtblGap2_9();

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetErrorValue([In] ref _tagpropertykey key, [MarshalAs(UnmanagedType.Error)] out int pValue);

		// Token: 0x060001AA RID: 426
		void _VtblGap3_6();

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetGuidValue([In] ref _tagpropertykey key, [In] ref Guid Value);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetGuidValue([In] ref _tagpropertykey key, out Guid pValue);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetBufferValue([In] ref _tagpropertykey key, [In] IntPtr pValue, [In] uint cbValue);

		// Token: 0x060001AE RID: 430
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetBufferValue([In] ref _tagpropertykey key, out IntPtr ppValue, out uint pcbValue);

		// Token: 0x060001AF RID: 431
		void _VtblGap4_2();

		// Token: 0x060001B0 RID: 432
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetIPortableDevicePropVariantCollectionValue([In] ref _tagpropertykey key, [MarshalAs(UnmanagedType.Interface)] [In] IPortableDevicePropVariantCollection pValue);

		// Token: 0x060001B1 RID: 433
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetIPortableDevicePropVariantCollectionValue([In] ref _tagpropertykey key, [MarshalAs(UnmanagedType.Interface)] out IPortableDevicePropVariantCollection ppValue);

		// Token: 0x060001B2 RID: 434
		void _VtblGap5_7();

		// Token: 0x060001B3 RID: 435
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Clear();
	}
}
