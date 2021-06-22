using System;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x02000003 RID: 3
	public class DownloadInfo
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000035C0 File Offset: 0x000017C0
		public DownloadInfo()
		{
			this.Installable = false;
			this.Size = 0L;
			this.SHA1Hash = null;
			this.SHA256Hash = null;
			this.Name = null;
			this.ContentType = null;
			this.Url = null;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000035FA File Offset: 0x000017FA
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003602 File Offset: 0x00001802
		public string SHA1Hash { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000360B File Offset: 0x0000180B
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003613 File Offset: 0x00001813
		public string SHA256Hash { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000361C File Offset: 0x0000181C
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003624 File Offset: 0x00001824
		public string Name { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000362D File Offset: 0x0000182D
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003635 File Offset: 0x00001835
		public long Size { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000363E File Offset: 0x0000183E
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003646 File Offset: 0x00001846
		public bool Installable { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000364F File Offset: 0x0000184F
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003657 File Offset: 0x00001857
		public string ContentType { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003660 File Offset: 0x00001860
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003668 File Offset: 0x00001868
		public string Url { get; set; }
	}
}
