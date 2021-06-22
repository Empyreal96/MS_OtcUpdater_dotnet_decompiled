using System;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x02000008 RID: 8
	public class InstalledPackageInfo
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004A98 File Offset: 0x00002C98
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public string Partition { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004AA9 File Offset: 0x00002CA9
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004AB1 File Offset: 0x00002CB1
		public string Package { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004ABA File Offset: 0x00002CBA
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00004AC2 File Offset: 0x00002CC2
		public Version Version { get; set; }

		// Token: 0x060000A6 RID: 166 RVA: 0x00003682 File Offset: 0x00001882
		public InstalledPackageInfo()
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004ACC File Offset: 0x00002CCC
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
				throw new Exception(string.Format("Invalid package version: {0} {1}", package, versionString));
			}
		}
	}
}
