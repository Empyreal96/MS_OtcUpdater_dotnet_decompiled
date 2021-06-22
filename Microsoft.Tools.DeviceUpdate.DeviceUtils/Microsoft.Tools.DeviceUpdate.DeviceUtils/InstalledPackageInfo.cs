using System;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x0200000B RID: 11
	public class InstalledPackageInfo
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000D9B9 File Offset: 0x0000BBB9
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000D9C1 File Offset: 0x0000BBC1
		public string Partition { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000D9CA File Offset: 0x0000BBCA
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000D9D2 File Offset: 0x0000BBD2
		public string Package { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000D9DB File Offset: 0x0000BBDB
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000D9E3 File Offset: 0x0000BBE3
		public Version Version { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00002050 File Offset: 0x00000250
		public InstalledPackageInfo()
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000D9EC File Offset: 0x0000BBEC
		public InstalledPackageInfo(string partition, string package, string versionString)
		{
			this.Partition = partition;
			this.Package = package;
			try
			{
				this.Version = Version.Parse(versionString);
			}
			catch
			{
				throw new DeviceException(string.Format("Invalid package version: {0} {1}", package, versionString));
			}
		}
	}
}
