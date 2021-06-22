using System;
using System.Collections.Generic;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils
{
	// Token: 0x0200000A RID: 10
	public interface IUpdateEvaluator
	{
		// Token: 0x060000A8 RID: 168
		void AddUpdate(int id, bool isLeaf, string updateXml);

		// Token: 0x060000A9 RID: 169
		void ChangeUpdate(int id, bool isLeaf, string updateXml);

		// Token: 0x060000AA RID: 170
		void RemoveUpdate(int id);

		// Token: 0x060000AB RID: 171
		void PartitionUpdates(List<int> installedNonLeafUpdates, List<int> otherUpdates);

		// Token: 0x060000AC RID: 172
		void Clear();

		// Token: 0x060000AD RID: 173
		void Reset();

		// Token: 0x060000AE RID: 174
		void EvaluateUpdates();

		// Token: 0x060000AF RID: 175
		int[] GetInstallableUpdates();
	}
}
