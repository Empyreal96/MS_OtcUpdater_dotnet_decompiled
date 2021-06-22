using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PortableDeviceApiLib
{
	// Token: 0x02000020 RID: 32
	[CompilerGenerated]
	[Guid("DADA2357-E0AD-492E-98DB-DD61C53BA353")]
	[InterfaceType(1)]
	[TypeIdentifier]
	[ComImport]
	public interface IPortableDeviceKeyCollection
	{
		// Token: 0x06000193 RID: 403
		void _VtblGap1_2();

		// Token: 0x06000194 RID: 404
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Add([In] ref _tagpropertykey key);
	}
}
