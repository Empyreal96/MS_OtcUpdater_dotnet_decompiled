using System;
using System.IO;
using FFUComponents;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000018 RID: 24
	public class UefiDevice : Disposable, IUefiDevice, IDevicePropertyCollection, IDisposable
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000F68D File Offset: 0x0000D88D
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000F695 File Offset: 0x0000D895
		public IFFUDevice FFUDevice { get; set; }

		// Token: 0x06000108 RID: 264 RVA: 0x0000F69E File Offset: 0x0000D89E
		public UefiDevice(IFFUDevice ffuDevice)
		{
			this.FFUDevice = ffuDevice;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000F6AD File Offset: 0x0000D8AD
		public Guid DeviceUniqueId
		{
			get
			{
				return this.FFUDevice.DeviceUniqueID;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000F6BC File Offset: 0x0000D8BC
		public string UniqueID
		{
			get
			{
				return this.DeviceUniqueId.ToString().Replace("-", "");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		public string UefiName
		{
			get
			{
				return this.FFUDevice.DeviceFriendlyName;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000F6F9 File Offset: 0x0000D8F9
		public void UpdateFFUDevice(IFFUDevice device)
		{
			if (device == null)
			{
				throw new Exception("Cannot update FFUDevice with a NULL value");
			}
			this.FFUDevice = device;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000F710 File Offset: 0x0000D910
		public void FlashFFU(string path, bool optimize, EventHandler<ProgressEventArgs> progressEventHandler)
		{
			this.FFUDevice.ProgressEvent += progressEventHandler;
			this.FFUDevice.EndTransfer();
			this.FFUDevice.FlashFFUFile(path, optimize);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000F737 File Offset: 0x0000D937
		public void SkipFFU()
		{
			this.FFUDevice.SkipTransfer();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000F745 File Offset: 0x0000D945
		public void WriteWim(string wimPath, EventHandler<ProgressEventArgs> progressEventHandler)
		{
			this.FFUDevice.ProgressEvent += progressEventHandler;
			this.FFUDevice.EndTransfer();
			this.FFUDevice.WriteWim(wimPath);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000F76C File Offset: 0x0000D96C
		public string GetServicingLogs(string logPath)
		{
			this.FFUDevice.EndTransfer();
			return this.FFUDevice.GetServicingLogs(logPath);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000F786 File Offset: 0x0000D986
		public string GetFlashingLogs(string logPath)
		{
			this.FFUDevice.EndTransfer();
			return this.FFUDevice.GetFlashingLogs(logPath);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		public void ReadPartition(string partition, out byte[] data)
		{
			uint blockSize = 0U;
			ulong num = 0UL;
			if (!this.FFUDevice.GetDiskInfo(ref blockSize, ref num))
			{
				throw new DeviceException("Unable to retrieve disk size details.  Please ensure the device supports this FFU operation.");
			}
			GptDevice gptDevice = null;
			if (!GptDevice.CreateInstance(this.FFUDevice, blockSize, out gptDevice))
			{
				throw new DeviceException("Unable to parse GPT on device.  The disk may have been corrupted.");
			}
			if (!gptDevice.ReadPartition(partition, out data))
			{
				throw new DeviceException(string.Format("Error reading partition {0}.", partition));
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000F808 File Offset: 0x0000DA08
		public void WritePartition(string partition, byte[] data)
		{
			uint blockSize = 0U;
			ulong num = 0UL;
			if (!this.FFUDevice.GetDiskInfo(ref blockSize, ref num))
			{
				throw new DeviceException("Unable to retrieve disk size details.  Please ensure that the device supports this FFU operation");
			}
			GptDevice gptDevice = null;
			if (!GptDevice.CreateInstance(this.FFUDevice, blockSize, out gptDevice))
			{
				throw new DeviceException("Unable to parse GPT on device.  The disk may have been corrupted.");
			}
			if (!gptDevice.WritePartition(partition, data))
			{
				throw new DeviceException(string.Format(" Error writing partition {0}.", partition));
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000F86E File Offset: 0x0000DA6E
		public void ClearPlatformIDOverride()
		{
			if (!this.FFUDevice.ClearIdOverride())
			{
				throw new DeviceException("Unable to clear platform ID.  Please ensure that the device supports this FFU operation.");
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000F888 File Offset: 0x0000DA88
		public void EnterMassStorage()
		{
			if (!this.FFUDevice.EnterMassStorage())
			{
				throw new DeviceException("Unable to enter mass storage mode.  Please ensure that the device supports this FFU operation.");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000F8A2 File Offset: 0x0000DAA2
		public string FFUDeviceType
		{
			get
			{
				return this.FFUDevice.DeviceType;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		public string FFUServicingLogsAvailable
		{
			get
			{
				string result = "false";
				try
				{
					File.Delete(this.FFUDevice.GetServicingLogs(Path.GetTempPath()));
					result = "true";
				}
				catch
				{
				}
				return result;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		public string FFUFlashingLogsAvailable
		{
			get
			{
				string result = "false";
				try
				{
					File.Delete(this.FFUDevice.GetFlashingLogs(Path.GetTempPath()));
					result = "true";
				}
				catch
				{
				}
				return result;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public virtual string GetProperty(string name)
		{
			return PropertyDeviceCollection.GetPropertyString(this, name);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000F938 File Offset: 0x0000DB38
		protected override void DisposeManaged()
		{
			if (this.FFUDevice != null)
			{
				this.FFUDevice.Dispose();
				this.FFUDevice = null;
			}
		}
	}
}
