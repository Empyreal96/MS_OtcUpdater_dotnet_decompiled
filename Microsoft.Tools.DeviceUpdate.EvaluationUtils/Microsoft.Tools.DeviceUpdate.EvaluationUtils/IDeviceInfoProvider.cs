using System;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x02000007 RID: 7
	public interface IDeviceInfoProvider
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000096 RID: 150
		string Category { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000097 RID: 151
		string[] RedirCabUrls { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000098 RID: 152
		string[] Locales { get; }

		// Token: 0x06000099 RID: 153
		bool EvaluateRegSz(string key, string subkey, string value, string data, string comparison);

		// Token: 0x0600009A RID: 154
		bool EvaluateRegDword(string key, string subkey, string value, uint data, string comparison);

		// Token: 0x0600009B RID: 155
		bool EvaluateRegKeyExists(string key, string subkey);

		// Token: 0x0600009C RID: 156
		bool EvaluateRegValueExists(string key, string subkey, string value);

		// Token: 0x0600009D RID: 157
		bool EvaluateCSPQuery(string locUri, string expectedValueString, string comparison);

		// Token: 0x0600009E RID: 158
		bool EvaluateProcessor(string architectureValueString);

		// Token: 0x0600009F RID: 159
		bool EvaluateWindowsVersion(string majorVersionValue, string minorVersionValue, string buildNumber, string comparison);
	}
}
