using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using FFUComponents;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x02000019 RID: 25
	public class UefiDeviceFactory : Disposable
	{
		// Token: 0x0600011B RID: 283 RVA: 0x0000F954 File Offset: 0x0000DB54
		protected UefiDeviceFactory()
		{
			FFUManager.Start();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000F99A File Offset: 0x0000DB9A
		protected override void DisposeManaged()
		{
			this.Reset();
			FFUManager.Stop();
			base.DisposeManaged();
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000F9B0 File Offset: 0x0000DBB0
		public int DeviceCount
		{
			get
			{
				object obj = this.mutex;
				int count;
				lock (obj)
				{
					this.Refresh();
					count = this.ffuDevices.Count;
				}
				return count;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000FA00 File Offset: 0x0000DC00
		public IUefiDevice[] Devices
		{
			get
			{
				object obj = this.mutex;
				IUefiDevice[] result;
				lock (obj)
				{
					this.Refresh();
					result = this.ffuDevices.Values.ToArray<UefiDevice>();
				}
				return result;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public void Reset()
		{
			object obj = this.mutex;
			lock (obj)
			{
				foreach (UefiDevice uefiDevice in this.ffuDevices.Values)
				{
					uefiDevice.Dispose();
				}
				this.ffuDevices.Clear();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000FAE0 File Offset: 0x0000DCE0
		public void Refresh()
		{
			object obj = this.mutex;
			lock (obj)
			{
				DateTime now = DateTime.Now;
				if (now.Subtract(UefiDeviceFactory.refreshIntervalSeconds).CompareTo(this.lastRefresh) >= 0)
				{
					this.lastRefresh = now;
					ICollection<IFFUDevice> collection = new List<IFFUDevice>();
					FFUManager.GetFlashableDevices(ref collection);
					HashSet<Guid> hashSet = new HashSet<Guid>();
					foreach (IFFUDevice iffudevice in collection)
					{
						hashSet.Add(iffudevice.DeviceUniqueID);
						if (!this.ffuDevices.ContainsKey(iffudevice.DeviceUniqueID))
						{
							this.ffuDevices[iffudevice.DeviceUniqueID] = new UefiDevice(iffudevice);
						}
					}
					foreach (Guid guid in this.ffuDevices.Keys.ToArray<Guid>())
					{
						if (!hashSet.Contains(guid))
						{
							this.ffuDevices[guid].Dispose();
							this.ffuDevices.Remove(guid);
						}
					}
				}
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000FC44 File Offset: 0x0000DE44
		public uint GetDeviceError()
		{
			object obj = this.mutex;
			uint result;
			lock (obj)
			{
				Guid winUSBClassGuid = FFUManager.WinUSBClassGuid;
				IntPtr intPtr = NativeMethods.SetupDiGetClassDevs(ref winUSBClassGuid, null, IntPtr.Zero, 2);
				if (IntPtr.Zero == intPtr)
				{
					result = 0U;
				}
				else
				{
					NativeMethods.DeviceInformationData deviceInformationData = new NativeMethods.DeviceInformationData
					{
						Size = Marshal.SizeOf(typeof(NativeMethods.DeviceInformationData)),
						ClassGuid = winUSBClassGuid,
						DevInst = 0U,
						Reserved = IntPtr.Zero
					};
					try
					{
						uint num = 0U;
						while (NativeMethods.SetupDiEnumDeviceInfo(intPtr, num++, ref deviceInformationData))
						{
							num += 1U;
							uint num2 = 0U;
							uint result2 = 0U;
							if (NativeMethods.CM_Get_DevNode_Status(ref num2, ref result2, deviceInformationData.DevInst, 0U) == 0U)
							{
								return result2;
							}
						}
						result = 0U;
					}
					finally
					{
						NativeMethods.SetupDiDestroyDeviceInfoList(intPtr);
					}
				}
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public bool UpdateDevice(IFFUDevice device)
		{
			object obj = this.mutex;
			bool result;
			lock (obj)
			{
				if (device == null)
				{
					result = false;
				}
				else if (!this.ffuDevices.ContainsKey(device.DeviceUniqueID))
				{
					result = false;
				}
				else
				{
					UefiDevice uefiDevice = this.ffuDevices[device.DeviceUniqueID];
					if (device == uefiDevice.FFUDevice)
					{
						result = false;
					}
					else
					{
						uefiDevice.FFUDevice.Dispose();
						uefiDevice.FFUDevice = device;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x040002E7 RID: 743
		private object mutex = new object();

		// Token: 0x040002E8 RID: 744
		private Dictionary<Guid, UefiDevice> ffuDevices = new Dictionary<Guid, UefiDevice>();

		// Token: 0x040002E9 RID: 745
		private static readonly TimeSpan refreshIntervalSeconds = TimeSpan.FromSeconds(2.0);

		// Token: 0x040002EA RID: 746
		private DateTime lastRefresh = DateTime.Now.Subtract(UefiDeviceFactory.refreshIntervalSeconds);

		// Token: 0x040002EB RID: 747
		public static UefiDeviceFactory Instance = new UefiDeviceFactory();
	}
}
