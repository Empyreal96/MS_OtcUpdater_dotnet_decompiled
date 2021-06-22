using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using FFUComponents;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000007 RID: 7
	public class GptDevice
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000D2DF File Offset: 0x0000B4DF
		private GptDevice(IFFUDevice device, ulong blockSize, GptPartition[] partitions)
		{
			this.device = device;
			this.blockSize = blockSize;
			this.partitions = partitions;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		public static bool CreateInstance(IFFUDevice device, uint blockSize, out GptDevice gptDevice)
		{
			gptDevice = null;
			byte[] array = new byte[blockSize];
			device.ReadDisk((ulong)blockSize, array, 0, array.Length);
			int num = 0;
			long num2 = (long)BitConverter.ToUInt64(array, num);
			num += 8;
			if (num2 != 6075990659671082565L)
			{
				return false;
			}
			int num3 = (int)BitConverter.ToUInt32(array, num);
			num += 4;
			if (num3 != 65536)
			{
				return false;
			}
			int length = BitConverter.ToInt32(array, num);
			num += 4;
			int crc = BitConverter.ToInt32(array, num);
			BitConverter.GetBytes(0).CopyTo(array, num);
			num += 4;
			if (!GptDevice.CheckCrc32(array, 0, length, crc))
			{
				return false;
			}
			num += 36;
			num += 16;
			ulong num4 = BitConverter.ToUInt64(array, num);
			num += 8;
			uint num5 = BitConverter.ToUInt32(array, num);
			num += 4;
			uint num6 = BitConverter.ToUInt32(array, num);
			num += 4;
			int crc2 = BitConverter.ToInt32(array, num);
			num += 4;
			byte[] array2 = new byte[num5 * num6];
			device.ReadDisk(num4 * (ulong)blockSize, array2, 0, array2.Length);
			if (!GptDevice.CheckCrc32(array2, crc2))
			{
				return false;
			}
			GptPartition[] array3;
			if (!GptPartition.ReadFrom(array2, num5, num6, out array3))
			{
				return false;
			}
			gptDevice = new GptDevice(device, (ulong)blockSize, array3);
			return true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000D408 File Offset: 0x0000B608
		public bool ReadPartition(string name, out byte[] data)
		{
			data = null;
			foreach (GptPartition gptPartition in this.partitions)
			{
				if (gptPartition.Name == name)
				{
					ulong num = (gptPartition.LastLBA - gptPartition.FirstLBA + 1UL) * this.blockSize;
					data = new byte[num];
					this.device.ReadDisk(gptPartition.FirstLBA * this.blockSize, data, 0, data.Length);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000D484 File Offset: 0x0000B684
		public bool WritePartition(string name, byte[] data)
		{
			GptPartition[] array = this.partitions;
			int i = 0;
			while (i < array.Length)
			{
				GptPartition gptPartition = array[i];
				if (gptPartition.Name == name)
				{
					ulong num = (gptPartition.LastLBA - gptPartition.FirstLBA + 1UL) * this.blockSize;
					if ((long)data.Length != (long)num)
					{
						return false;
					}
					this.device.WriteDisk(gptPartition.FirstLBA * this.blockSize, data, 0, data.Length);
					return true;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		private static bool CheckCrc32(byte[] data, int crc)
		{
			return GptDevice.CheckCrc32(data, 0, data.Length, crc);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000D508 File Offset: 0x0000B708
		private static bool CheckCrc32(byte[] data, int offset, int length, int crc)
		{
			int num = GptDevice.RtlComputeCrc32(0, data, offset, length);
			return crc == num;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000D524 File Offset: 0x0000B724
		private static int RtlComputeCrc32(int PartialCrc, byte[] data, int offset, int length)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(length);
			Marshal.Copy(data, offset, intPtr, length);
			int result = NativeMethods.RtlComputeCrc32(PartialCrc, intPtr, length);
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000D550 File Offset: 0x0000B750
		private static bool DoBackup(IFFUDevice device, string outputPath)
		{
			string[] array = new string[]
			{
				"DPP",
				"MODEM_FS1",
				"MODEM_FS2",
				"MODEM_FSG"
			};
			outputPath = Path.Combine(outputPath, device.DeviceUniqueID.ToString("B"));
			if (!Directory.Exists(outputPath))
			{
				Directory.CreateDirectory(outputPath);
			}
			uint num;
			ulong num2;
			if (!device.GetDiskInfo(ref num, ref num2))
			{
				Console.WriteLine("Unable to retrieve disk size details.  Please ensure the device supports FFU disk I/O.");
				return false;
			}
			GptDevice gptDevice;
			if (!GptDevice.CreateInstance(device, num, out gptDevice))
			{
				Console.WriteLine("Unable to parse GPT on device.  The disk may have been corrupted.");
				return false;
			}
			Console.WriteLine("Backing up partitions to {0}", outputPath);
			foreach (string text in array)
			{
				Console.WriteLine(" Backing up partition {0}", text);
				byte[] bytes;
				if (!gptDevice.ReadPartition(text, out bytes))
				{
					Console.WriteLine(" Error reading partition {0}.", text);
					return false;
				}
				File.WriteAllBytes(Path.Combine(outputPath, text), bytes);
			}
			Console.WriteLine("Finished partition backup.");
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000D648 File Offset: 0x0000B848
		private static bool DoRestore(IFFUDevice device, string inputPath)
		{
			inputPath = Path.Combine(inputPath, device.DeviceUniqueID.ToString("B"));
			if (!Directory.Exists(inputPath))
			{
				return false;
			}
			uint num;
			ulong num2;
			if (!device.GetDiskInfo(ref num, ref num2))
			{
				Console.WriteLine("Unable to retrieve disk size details.  Please ensure the device supports FFU disk I/O.");
				return false;
			}
			GptDevice gptDevice;
			if (!GptDevice.CreateInstance(device, num, out gptDevice))
			{
				Console.WriteLine("Unable to parse GPT on device.  The disk may have been corrupted.");
				return false;
			}
			IEnumerable<string> enumerable = from path in Directory.GetFiles(inputPath)
			select Path.GetFileName(path);
			Console.WriteLine("Restoring partitions from {0}", inputPath);
			foreach (string text in enumerable)
			{
				Console.WriteLine(" Restoring partition {0}", text);
				byte[] data = File.ReadAllBytes(Path.Combine(inputPath, text));
				if (!gptDevice.WritePartition(text, data))
				{
					Console.WriteLine(" Error writing partition {0}.", text);
					return false;
				}
			}
			Console.WriteLine("Finished partition restore.");
			return true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000D760 File Offset: 0x0000B960
		private static bool DoVerify(IFFUDevice device, string inputPath)
		{
			inputPath = Path.Combine(inputPath, device.DeviceUniqueID.ToString("B"));
			if (!Directory.Exists(inputPath))
			{
				return false;
			}
			uint num;
			ulong num2;
			if (!device.GetDiskInfo(ref num, ref num2))
			{
				Console.WriteLine("Unable to retrieve disk size details.  Please ensure the device supports FFU disk I/O.");
				return false;
			}
			GptDevice gptDevice;
			if (!GptDevice.CreateInstance(device, num, out gptDevice))
			{
				Console.WriteLine("Unable to parse GPT on device.  The disk may have been corrupted.");
				return false;
			}
			IEnumerable<string> enumerable = from path in Directory.GetFiles(inputPath)
			select Path.GetFileName(path);
			Console.WriteLine("Verifying partitions from {0}", inputPath);
			foreach (string text in enumerable)
			{
				Console.WriteLine(" Verifying partition {0}", text);
				byte[] array = File.ReadAllBytes(Path.Combine(inputPath, text));
				byte[] array2;
				if (!gptDevice.ReadPartition(text, out array2))
				{
					Console.WriteLine(" Error reading partition {0}.", text);
					return false;
				}
				if ((long)array.Length != (long)array2.Length)
				{
					Console.WriteLine(" Size mismatch for partition {0}.", text);
					return false;
				}
				for (long num3 = 0L; num3 < (long)array.Length; num3 += 1L)
				{
					if (checked(array[(int)((IntPtr)num3)] != array2[(int)((IntPtr)num3)]))
					{
						Console.WriteLine(" Byte mismatch for partition {0} at offset {1}.", text, num3);
						return false;
					}
				}
			}
			Console.WriteLine("Partitions verified.");
			return true;
		}

		// Token: 0x0400029E RID: 670
		private IFFUDevice device;

		// Token: 0x0400029F RID: 671
		private ulong blockSize;

		// Token: 0x040002A0 RID: 672
		private GptPartition[] partitions;
	}
}
