using System;
using System.Runtime.InteropServices;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000013 RID: 19
	internal class NativeMethods
	{
		// Token: 0x060000F3 RID: 243
		[DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool SetupDiEnumDeviceInfo(IntPtr DeviceInfoSet, uint MemberIndex, ref NativeMethods.DeviceInformationData DeviceInfoData);

		// Token: 0x060000F4 RID: 244
		[DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr SetupDiGetClassDevs(ref Guid classGuid, string enumerator, IntPtr parent, int flags);

		// Token: 0x060000F5 RID: 245
		[DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

		// Token: 0x060000F6 RID: 246
		[DllImport("Cfgmgr32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern uint CM_Get_DevNode_Status(ref uint status, ref uint problemNumber, uint devInst, uint flags);

		// Token: 0x060000F7 RID: 247
		[DllImport("ntdll.dll")]
		public static extern int RtlComputeCrc32(int PartialCrc, IntPtr Buffer, int Length);

		// Token: 0x02000038 RID: 56
		public enum WinError : uint
		{
			// Token: 0x0400037D RID: 893
			Success,
			// Token: 0x0400037E RID: 894
			FileNotFound = 2U,
			// Token: 0x0400037F RID: 895
			NoMoreFiles = 18U,
			// Token: 0x04000380 RID: 896
			NotReady = 21U,
			// Token: 0x04000381 RID: 897
			GeneralFailure = 31U,
			// Token: 0x04000382 RID: 898
			InvalidParameter = 87U,
			// Token: 0x04000383 RID: 899
			InsufficientBuffer = 122U,
			// Token: 0x04000384 RID: 900
			IoPending = 997U,
			// Token: 0x04000385 RID: 901
			DeviceNotConnected = 1167U,
			// Token: 0x04000386 RID: 902
			TimeZoneIdInvalid = 4294967295U,
			// Token: 0x04000387 RID: 903
			InvalidHandleValue = 4294967295U,
			// Token: 0x04000388 RID: 904
			PathNotFound = 3U,
			// Token: 0x04000389 RID: 905
			AlreadyExists = 183U,
			// Token: 0x0400038A RID: 906
			NoMoreItems = 259U
		}

		// Token: 0x02000039 RID: 57
		public enum SetupApiErr : uint
		{
			// Token: 0x0400038C RID: 908
			InWow64 = 3758096949U
		}

		// Token: 0x0200003A RID: 58
		public enum DiFuction : uint
		{
			// Token: 0x0400038E RID: 910
			PropertyChange = 18U
		}

		// Token: 0x0200003B RID: 59
		public enum DICS : uint
		{
			// Token: 0x04000390 RID: 912
			Enable = 1U,
			// Token: 0x04000391 RID: 913
			Disable,
			// Token: 0x04000392 RID: 914
			PropertyChange,
			// Token: 0x04000393 RID: 915
			Start,
			// Token: 0x04000394 RID: 916
			Stop
		}

		// Token: 0x0200003C RID: 60
		[Flags]
		public enum DICSFlags : uint
		{
			// Token: 0x04000396 RID: 918
			Global = 1U,
			// Token: 0x04000397 RID: 919
			ConfigSpecific = 2U,
			// Token: 0x04000398 RID: 920
			ConfigGeneral = 4U
		}

		// Token: 0x0200003D RID: 61
		public enum DIGCF
		{
			// Token: 0x0400039A RID: 922
			Default = 1,
			// Token: 0x0400039B RID: 923
			Present,
			// Token: 0x0400039C RID: 924
			AllClasses = 4,
			// Token: 0x0400039D RID: 925
			Profile = 8,
			// Token: 0x0400039E RID: 926
			DeviceInterface = 16
		}

		// Token: 0x0200003E RID: 62
		[Flags]
		public enum DIDMFlags : uint
		{
			// Token: 0x040003A0 RID: 928
			HasProblem = 1024U
		}

		// Token: 0x0200003F RID: 63
		public enum CMPROB : uint
		{
			// Token: 0x040003A2 RID: 930
			EntryIsWrongType = 4U
		}

		// Token: 0x02000040 RID: 64
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct ClassInstallHeader
		{
			// Token: 0x040003A3 RID: 931
			public int Size;

			// Token: 0x040003A4 RID: 932
			public NativeMethods.DiFuction InstallFunction;
		}

		// Token: 0x02000041 RID: 65
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PropertyChangeParams
		{
			// Token: 0x040003A5 RID: 933
			public NativeMethods.ClassInstallHeader Header;

			// Token: 0x040003A6 RID: 934
			public uint StateChange;

			// Token: 0x040003A7 RID: 935
			public uint Scope;

			// Token: 0x040003A8 RID: 936
			public uint HwProfile;
		}

		// Token: 0x02000042 RID: 66
		public struct DeviceInformationData
		{
			// Token: 0x040003A9 RID: 937
			public int Size;

			// Token: 0x040003AA RID: 938
			public Guid ClassGuid;

			// Token: 0x040003AB RID: 939
			public uint DevInst;

			// Token: 0x040003AC RID: 940
			public IntPtr Reserved;
		}
	}
}
