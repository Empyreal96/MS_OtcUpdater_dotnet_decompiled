using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Tools.DeviceUpdate.DeviceUtils;
using Microsoft.Tools.DeviceUpdate.EvaluationUtils;

namespace OtcUpdater
{
	// Token: 0x02000006 RID: 6
	[Guid("53847F5B-A504-4355-9EE5-828176D6992C")]
	internal class OtcUpdater
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00003B3D File Offset: 0x00001D3D
		private static void Main(string[] args)
		{
			new OtcUpdater().Run(args);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003B4A File Offset: 0x00001D4A
		private OtcUpdater()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003B60 File Offset: 0x00001D60
		private void Run(string[] args)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			try
			{
				for (int i = 0; i < args.Length; i++)
				{
					string a = args[i].ToLowerInvariant();
					if (!(a == "/batch"))
					{
						if (!(a == "/log"))
						{
							flag3 = (a == "/help" || true);
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag = true;
					}
				}
				if (flag3 || flag)
				{
					View.Instance = new BatchView();
				}
				else
				{
					View.Instance = new ConsoleView();
				}
				View.Instance.ShowHeader(string.Format("{0} {1}", Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version));
				if (flag3)
				{
					this.ShowHelpMessage();
				}
				else
				{
					string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
					string environmentVariable = Environment.GetEnvironmentVariable("OTC_PACKAGES_PATH");
					if (environmentVariable != null)
					{
						path = environmentVariable;
					}
					PackageDownloader.Instance.DownloadPath = Path.Combine(path, "packages");
					Directory.CreateDirectory(PackageDownloader.Instance.DownloadPath);
					for (;;)
					{
						View.Instance.ClearInputBuffer();
						DeviceUpdater deviceUpdater = null;
						bool flag4 = false;
						while ((deviceUpdater = this.GetDevice()) == null && !flag)
						{
							if (!flag4)
							{
								View.Instance.ShowWarningMessage("Waiting for next device.  Please connect a device via USB.  Press q to quit.");
								flag4 = true;
							}
							if (View.Instance.PollForKey() == 'q')
							{
								using (Dictionary<IWpdDevice, DeviceUpdater>.ValueCollection.Enumerator enumerator = this.deviceUpdaters.Values.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										DeviceUpdater deviceUpdater2 = enumerator.Current;
										deviceUpdater2.Stop();
									}
									break;
								}
							}
							Thread.Sleep(3000);
						}
						if (deviceUpdater == null)
						{
							break;
						}
						if (flag2)
						{
							deviceUpdater.StartCollectLogs(flag);
						}
						else
						{
							deviceUpdater.StartUpdate(flag);
						}
					}
				}
			}
			catch (Exception ex)
			{
				View.Instance.ShowErrorMessage(ex.Message);
				foreach (DeviceUpdater deviceUpdater3 in this.deviceUpdaters.Values)
				{
					deviceUpdater3.Stop();
				}
				if (!flag)
				{
					View.Instance.ShowNormalMessage("Press any key to exit ...");
					View.Instance.WaitForKey();
				}
			}
			finally
			{
				if (this.deviceUpdaters.Count > 0)
				{
					View.Instance.ShowWarningMessage("Waiting for operations to complete.");
					foreach (DeviceUpdater deviceUpdater4 in this.deviceUpdaters.Values)
					{
						deviceUpdater4.Join();
					}
					this.deviceUpdaters.Clear();
				}
				WpdDeviceFactory.Instance.Dispose();
				View.Instance.Restore();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003E7C File Offset: 0x0000207C
		private void ShowHelpMessage()
		{
			View.Instance.ShowNormalMessage("Updates your connected mobile device(s) to the latest supported release of Windows");
			View.Instance.ShowNormalMessage("<no args> Updates devices as they are connected");
			View.Instance.ShowNormalMessage("/batch    Batch mode - updates all currently connected devices and exits");
			View.Instance.ShowNormalMessage("/log      Collects and saves the device logs");
			View.Instance.ShowNormalMessage("/help     Displays this message");
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003ED4 File Offset: 0x000020D4
		private DeviceUpdater GetDevice()
		{
			DeviceUpdater deviceUpdater = null;
			try
			{
				IWpdDevice[] devices = WpdDeviceFactory.Instance.Devices;
				HashSet<IWpdDevice> hashSet = new HashSet<IWpdDevice>();
				if (devices.Length != 0)
				{
					foreach (IWpdDevice wpdDevice in devices)
					{
						hashSet.Add(wpdDevice);
						if (deviceUpdater == null && !this.deviceUpdaters.ContainsKey(wpdDevice))
						{
							deviceUpdater = new DeviceUpdater(wpdDevice);
							deviceUpdater.ProgressEvent += View.Instance.ShowProgressDeviceMessage;
							deviceUpdater.NormalMessageEvent += View.Instance.ShowNormalDeviceMessage;
							deviceUpdater.WarningMessageEvent += View.Instance.ShowWarningDeviceMessage;
							deviceUpdater.ErrorMessageEvent += View.Instance.ShowErrorDeviceMessage;
							deviceUpdater.LogMessageEvent += View.Instance.LogDeviceMessage;
							this.deviceUpdaters.Add(wpdDevice, deviceUpdater);
						}
					}
				}
				foreach (IWpdDevice wpdDevice2 in this.deviceUpdaters.Keys.ToArray<IWpdDevice>())
				{
					if (!hashSet.Contains(wpdDevice2))
					{
						this.deviceUpdaters.Remove(wpdDevice2);
						View.Instance.ShowWarningDeviceMessage(wpdDevice2, "Device was disconnected or is in the process of updating.");
						wpdDevice2.Dispose();
					}
				}
			}
			catch
			{
			}
			return deviceUpdater;
		}

		// Token: 0x0400001A RID: 26
		private Dictionary<IWpdDevice, DeviceUpdater> deviceUpdaters = new Dictionary<IWpdDevice, DeviceUpdater>();
	}
}
