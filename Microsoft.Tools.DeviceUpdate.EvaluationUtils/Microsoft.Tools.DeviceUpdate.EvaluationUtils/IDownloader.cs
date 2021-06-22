using System;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x02000005 RID: 5
	public interface IDownloader
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006A RID: 106
		// (set) Token: 0x0600006B RID: 107
		string DownloadPath { get; set; }

		// Token: 0x0600006C RID: 108
		string Download(string url, string filename);

		// Token: 0x0600006D RID: 109
		string DownloadToTempPath(string url);
	}
}
