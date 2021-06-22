using System;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x02000053 RID: 83
	public interface IDeploymentLogger
	{
		// Token: 0x06000245 RID: 581
		void Log(LoggingLevel level, string format, params object[] list);

		// Token: 0x06000246 RID: 582
		void LogException(Exception exp);

		// Token: 0x06000247 RID: 583
		void LogException(Exception exp, LoggingLevel level);

		// Token: 0x06000248 RID: 584
		void LogDebug(string format, params object[] list);

		// Token: 0x06000249 RID: 585
		void LogInfo(string format, params object[] list);

		// Token: 0x0600024A RID: 586
		void LogWarning(string format, params object[] list);

		// Token: 0x0600024B RID: 587
		void LogError(string format, params object[] list);
	}
}
