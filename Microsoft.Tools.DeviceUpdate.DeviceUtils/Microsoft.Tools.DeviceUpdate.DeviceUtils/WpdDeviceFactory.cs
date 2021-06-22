using System;
using System.Collections.Generic;
using System.Linq;
using PortableDeviceApiLib;
using PortableDeviceConstants;
using PortableDeviceTypesLib;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x0200001B RID: 27
	public class WpdDeviceFactory : Disposable
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		protected WpdDeviceFactory()
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00011C5F File Offset: 0x0000FE5F
		protected override void DisposeManaged()
		{
			this.Reset();
			base.DisposeManaged();
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00011C70 File Offset: 0x0000FE70
		public int DeviceCount
		{
			get
			{
				object obj = this.mutex;
				int count;
				lock (obj)
				{
					this.Refresh();
					count = this.wpdDevices.Count;
				}
				return count;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		public IWpdDevice[] Devices
		{
			get
			{
				object obj = this.mutex;
				IWpdDevice[] result;
				lock (obj)
				{
					this.Refresh();
					result = this.wpdDevices.Values.ToArray<IWpdDevice>();
				}
				return result;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00011D14 File Offset: 0x0000FF14
		public void Reset()
		{
			object obj = this.mutex;
			lock (obj)
			{
				foreach (IWpdDevice wpdDevice in this.wpdDevices.Values)
				{
					wpdDevice.Dispose();
				}
				this.wpdDevices.Clear();
				this.portableDevices.Clear();
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00011DA8 File Offset: 0x0000FFA8
		public void Refresh()
		{
			object obj = this.mutex;
			lock (obj)
			{
				DateTime now = DateTime.Now;
				if (now.Subtract(WpdDeviceFactory.refreshIntervalSeconds).CompareTo(this.lastRefresh) >= 0)
				{
					this.lastRefresh = now;
					uint num = 0U;
					this.wpdManager.RefreshDeviceList();
					this.wpdManager.GetDevices(null, ref num);
					string[] array = new string[num];
					this.wpdManager.GetDevices(array, ref num);
					HashSet<string> hashSet = new HashSet<string>(array);
					string[] array2 = array;
					int i = 0;
					while (i < array2.Length)
					{
						string text = array2[i];
						IPortableDevice portableDevice = null;
						if (!this.portableDevices.ContainsKey(text))
						{
							try
							{
								PortableDeviceApiLib.IPortableDeviceValues pClientInfo = (PortableDeviceApiLib.IPortableDeviceValues)((PortableDeviceValues)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("0C15D503-D017-47CE-9016-7B3F978721CC"))));
								portableDevice = (PortableDevice)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("728A21C5-3D9E-48D7-9810-864848F0F404")));
								portableDevice.Open(text, pClientInfo);
								this.portableDevices[text] = portableDevice;
								goto IL_10A;
							}
							catch
							{
								goto IL_10A;
							}
							goto IL_FB;
						}
						goto IL_FB;
						IL_10A:
						if (!this.wpdDevices.ContainsKey(text))
						{
							try
							{
								IPortableDeviceContent portableDeviceContent = null;
								portableDevice.Content(out portableDeviceContent);
								IPortableDeviceProperties portableDeviceProperties = null;
								portableDeviceContent.Properties(out portableDeviceProperties);
								PortableDeviceApiLib.IPortableDeviceValues portableDeviceValues = null;
								portableDeviceProperties.GetValues("DEVICE", null, out portableDeviceValues);
								Guid b;
								portableDeviceValues.GetGuidValue(ref PortableDevicePKeys.WPD_DEVICE_MODEL_UNIQUE_ID, out b);
								if (WpdDevice.WindowsPhone8ModelID == b)
								{
									this.wpdDevices[text] = new WpdDevice(this.wpdManager, portableDevice, text);
								}
							}
							catch
							{
							}
						}
						i++;
						continue;
						IL_FB:
						portableDevice = this.portableDevices[text];
						goto IL_10A;
					}
					foreach (string text2 in this.portableDevices.Keys.ToArray<string>())
					{
						if (!hashSet.Contains(text2))
						{
							if (this.wpdDevices.ContainsKey(text2))
							{
								this.wpdDevices[text2].Dispose();
								this.wpdDevices.Remove(text2);
							}
							this.portableDevices[text2].Close();
							this.portableDevices.Remove(text2);
						}
					}
				}
			}
		}

		// Token: 0x04000350 RID: 848
		private object mutex = new object();

		// Token: 0x04000351 RID: 849
		private IPortableDeviceManager wpdManager = (PortableDeviceManager)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("0AF10CEC-2ECD-4B92-9581-34F6AE0637F3")));

		// Token: 0x04000352 RID: 850
		private Dictionary<string, IPortableDevice> portableDevices = new Dictionary<string, IPortableDevice>();

		// Token: 0x04000353 RID: 851
		private Dictionary<string, IWpdDevice> wpdDevices = new Dictionary<string, IWpdDevice>();

		// Token: 0x04000354 RID: 852
		private static readonly TimeSpan refreshIntervalSeconds = TimeSpan.FromSeconds(2.0);

		// Token: 0x04000355 RID: 853
		private DateTime lastRefresh = DateTime.Now.Subtract(WpdDeviceFactory.refreshIntervalSeconds);

		// Token: 0x04000356 RID: 854
		public static WpdDeviceFactory Instance = new WpdDeviceFactory();
	}
}
