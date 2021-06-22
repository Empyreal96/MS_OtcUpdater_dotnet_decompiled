using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x02000050 RID: 80
	public class RgaBuilder
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000ACD8 File Offset: 0x00008ED8
		public bool HasContent
		{
			get
			{
				return this._rgaValues.Count > 0;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		public void AddRgaValue(string keyName, string valueName, params string[] values)
		{
			KeyValuePair<string, string> key = new KeyValuePair<string, string>(keyName, valueName);
			List<string> list = null;
			if (!this._rgaValues.TryGetValue(key, out list))
			{
				list = new List<string>();
				this._rgaValues.Add(key, list);
			}
			list.AddRange(values);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000AD2C File Offset: 0x00008F2C
		public void Save(string outputFile)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Windows Registry Editor Version 5.00");
			foreach (IGrouping<string, KeyValuePair<KeyValuePair<string, string>, List<string>>> grouping in from x in this._rgaValues
			group x by x.Key.Key)
			{
				stringBuilder.AppendFormat("[{0}]", grouping.Key);
				stringBuilder.AppendLine();
				foreach (KeyValuePair<KeyValuePair<string, string>, List<string>> keyValuePair in grouping)
				{
					RegUtil.RegOutput(stringBuilder, keyValuePair.Key.Value, keyValuePair.Value);
				}
				stringBuilder.AppendLine();
			}
			LongPathFile.WriteAllText(outputFile, stringBuilder.ToString(), Encoding.Unicode);
		}

		// Token: 0x04000110 RID: 272
		private Dictionary<KeyValuePair<string, string>, List<string>> _rgaValues = new Dictionary<KeyValuePair<string, string>, List<string>>(new RgaBuilder.KeyValuePairComparer());

		// Token: 0x0200006F RID: 111
		private class KeyValuePairComparer : IEqualityComparer<KeyValuePair<string, string>>
		{
			// Token: 0x060002C1 RID: 705 RVA: 0x0000BE0C File Offset: 0x0000A00C
			public bool Equals(KeyValuePair<string, string> x, KeyValuePair<string, string> y)
			{
				return x.Key.Equals(y.Key, StringComparison.InvariantCultureIgnoreCase) && x.Value.Equals(y.Value, StringComparison.InvariantCultureIgnoreCase);
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x0000BE3A File Offset: 0x0000A03A
			public int GetHashCode(KeyValuePair<string, string> obj)
			{
				return obj.Key.GetHashCode() ^ obj.Value.GetHashCode();
			}
		}
	}
}
