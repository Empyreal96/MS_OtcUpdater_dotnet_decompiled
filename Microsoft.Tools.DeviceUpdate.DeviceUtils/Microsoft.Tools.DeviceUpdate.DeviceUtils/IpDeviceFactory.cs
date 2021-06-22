using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Tools.Connectivity;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x0200000E RID: 14
	public class IpDeviceFactory : Disposable
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		protected IpDeviceFactory()
		{
			this.AccessDenied = false;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000EFD1 File Offset: 0x0000D1D1
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000EFD9 File Offset: 0x0000D1D9
		public bool AccessDenied { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		public int DeviceCount
		{
			get
			{
				object obj = this.mutex;
				int count;
				lock (obj)
				{
					this.Refresh();
					count = this.ipDevices.Count;
				}
				return count;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000F034 File Offset: 0x0000D234
		public IIpDevice[] Devices
		{
			get
			{
				object obj = this.mutex;
				IIpDevice[] result;
				lock (obj)
				{
					this.Refresh();
					result = this.ipDevices.Values.ToArray<IpDevice>();
				}
				return result;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000F088 File Offset: 0x0000D288
		public void Reset()
		{
			object obj = this.mutex;
			lock (obj)
			{
				foreach (IpDevice ipDevice in this.ipDevices.Values)
				{
					((IDisposable)ipDevice).Dispose();
				}
				this.ipDevices.Clear();
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000F114 File Offset: 0x0000D314
		private void Refresh()
		{
			object obj = this.mutex;
			lock (obj)
			{
				DeviceDiscoveryService deviceDiscoveryService = null;
				try
				{
					this.AccessDenied = false;
					deviceDiscoveryService = new DeviceDiscoveryService();
					deviceDiscoveryService.Start();
					List<DiscoveredDeviceInfo> list = deviceDiscoveryService.DevicesDiscovered();
					HashSet<Guid> hashSet = new HashSet<Guid>();
					foreach (DiscoveredDeviceInfo discoveredDeviceInfo in list)
					{
						hashSet.Add(discoveredDeviceInfo.UniqueId);
					}
					foreach (IpDevice ipDevice in this.ipDevices.Values.ToArray<IpDevice>())
					{
						if (!hashSet.Contains(ipDevice.DeviceUniqueId))
						{
							ipDevice.Dispose();
							this.ipDevices.Remove(ipDevice.DeviceUniqueId);
						}
					}
					foreach (DiscoveredDeviceInfo discoveredDeviceInfo2 in list)
					{
						if (discoveredDeviceInfo2.Connection == 1)
						{
							IpDevice ipDevice2 = null;
							try
							{
								ipDevice2 = this.ipDevices[discoveredDeviceInfo2.UniqueId];
								if (!ipDevice2.DeviceCommunicator.IsIpDevice())
								{
									throw new DeviceException("IpDevice connection lost");
								}
							}
							catch
							{
								this.ipDevices.Remove(discoveredDeviceInfo2.UniqueId);
								if (ipDevice2 != null)
								{
									ipDevice2.Dispose();
									ipDevice2 = null;
								}
								ipDevice2 = null;
							}
							if (ipDevice2 == null)
							{
								IpDeviceCommunicator ipDeviceCommunicator = null;
								try
								{
									ipDeviceCommunicator = new IpDeviceCommunicator(discoveredDeviceInfo2.UniqueId);
									ipDeviceCommunicator.Connect();
									if (ipDeviceCommunicator.IsIpDevice())
									{
										ipDevice2 = new IpDevice(discoveredDeviceInfo2, ipDeviceCommunicator);
										this.ipDevices[discoveredDeviceInfo2.UniqueId] = ipDevice2;
										ipDeviceCommunicator = null;
									}
								}
								catch (AccessDeniedException)
								{
									this.AccessDenied = true;
								}
								catch
								{
								}
								finally
								{
									if (ipDeviceCommunicator != null)
									{
										ipDeviceCommunicator.Dispose();
										ipDeviceCommunicator = null;
									}
								}
							}
						}
					}
				}
				catch
				{
					this.Reset();
				}
				if (deviceDiscoveryService != null)
				{
					try
					{
						deviceDiscoveryService.Stop();
					}
					catch
					{
					}
					deviceDiscoveryService = null;
				}
			}
		}

		// Token: 0x040002E1 RID: 737
		private object mutex = new object();

		// Token: 0x040002E2 RID: 738
		private Dictionary<Guid, IpDevice> ipDevices = new Dictionary<Guid, IpDevice>();

		// Token: 0x040002E3 RID: 739
		public static IpDeviceFactory Instance = new IpDeviceFactory();
	}
}
