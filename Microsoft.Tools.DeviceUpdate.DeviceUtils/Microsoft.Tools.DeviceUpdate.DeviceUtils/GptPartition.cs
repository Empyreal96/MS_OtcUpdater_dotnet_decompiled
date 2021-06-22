using System;
using System.Text;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000008 RID: 8
	public class GptPartition
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		private GptPartition(ulong firstLBA, ulong lastLBA, string name)
		{
			this.FirstLBA = firstLBA;
			this.LastLBA = lastLBA;
			this.Name = name;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000D8F1 File Offset: 0x0000BAF1
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000D8F9 File Offset: 0x0000BAF9
		public ulong FirstLBA { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000D902 File Offset: 0x0000BB02
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000D90A File Offset: 0x0000BB0A
		public ulong LastLBA { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000D913 File Offset: 0x0000BB13
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000D91B File Offset: 0x0000BB1B
		public string Name { get; private set; }

		// Token: 0x06000020 RID: 32 RVA: 0x0000D924 File Offset: 0x0000BB24
		public static bool ReadFrom(byte[] data, uint partitionCount, uint partitionEntrySize, out GptPartition[] partitions)
		{
			partitions = new GptPartition[partitionCount];
			for (uint num = 0U; num < partitionCount; num += 1U)
			{
				if (!GptPartition.ReadPartition(data, num, partitionEntrySize, out partitions[(int)num]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000D95C File Offset: 0x0000BB5C
		private static bool ReadPartition(byte[] data, uint index, uint partitionEntrySize, out GptPartition partition)
		{
			int num = (int)(index * partitionEntrySize);
			num += 32;
			ulong firstLBA = BitConverter.ToUInt64(data, num);
			num += 8;
			ulong lastLBA = BitConverter.ToUInt64(data, num);
			num += 8;
			num += 8;
			int count = (int)(partitionEntrySize - 56U);
			string name = Encoding.Unicode.GetString(data, num, count).TrimEnd(new char[1]);
			partition = new GptPartition(firstLBA, lastLBA, name);
			return true;
		}
	}
}
