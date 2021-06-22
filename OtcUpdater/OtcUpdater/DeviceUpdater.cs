using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using Microsoft.Tools.DeviceUpdate.DeviceUtils;
using Microsoft.Tools.DeviceUpdate.EvaluationUtils;

namespace OtcUpdater
{
	// Token: 0x02000005 RID: 5
	public class DeviceUpdater
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000026 RID: 38 RVA: 0x000022B4 File Offset: 0x000004B4
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x000022EC File Offset: 0x000004EC
		public event DeviceUpdater.MessageCallback ProgressEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000028 RID: 40 RVA: 0x00002324 File Offset: 0x00000524
		// (remove) Token: 0x06000029 RID: 41 RVA: 0x0000235C File Offset: 0x0000055C
		public event DeviceUpdater.MessageCallback NormalMessageEvent;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600002A RID: 42 RVA: 0x00002394 File Offset: 0x00000594
		// (remove) Token: 0x0600002B RID: 43 RVA: 0x000023CC File Offset: 0x000005CC
		public event DeviceUpdater.MessageCallback WarningMessageEvent;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600002C RID: 44 RVA: 0x00002404 File Offset: 0x00000604
		// (remove) Token: 0x0600002D RID: 45 RVA: 0x0000243C File Offset: 0x0000063C
		public event DeviceUpdater.MessageCallback ErrorMessageEvent;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600002E RID: 46 RVA: 0x00002474 File Offset: 0x00000674
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x000024AC File Offset: 0x000006AC
		public event DeviceUpdater.MessageCallback LogMessageEvent;

		// Token: 0x06000030 RID: 48 RVA: 0x000024E4 File Offset: 0x000006E4
		public string GetWuServiceRequestUri()
		{
			string result = this.ServiceUrl;
			string environmentVariable = Environment.GetEnvironmentVariable("OTC_MUV6_URL");
			if (environmentVariable != null)
			{
				result = environmentVariable;
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000250C File Offset: 0x0000070C
		public bool GetSkipPushUpdates()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("OTC_SKIP_PUSHUPDATES");
			bool result = false;
			if (environmentVariable != null)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000252C File Offset: 0x0000072C
		public bool GetTrustedHost()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("OTC_TRUSTED_HOST");
			bool result = false;
			if (environmentVariable != null)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000254C File Offset: 0x0000074C
		public bool GetUsePrivatePackages()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("OTC_USE_PRIVATE_PACKAGES");
			bool result = false;
			if (environmentVariable != null)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000256C File Offset: 0x0000076C
		public DeviceUpdater(IWpdDevice device)
		{
			DeviceUpdater <>4__this = this;
			this.device = device;
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			this.logPath = Path.Combine(directoryName, "devicelogs");
			this.updateScanner.ClientServerUrl = this.GetWuServiceRequestUri();
			this.updateScanner.ProgressEvent += delegate(string message)
			{
				<>4__this.ProgressEvent(device, message);
			};
			this.updateScanner.NormalMessageEvent += delegate(string message)
			{
				<>4__this.NormalMessageEvent(device, message);
			};
			this.updateScanner.WarningMessageEvent += delegate(string message)
			{
				<>4__this.WarningMessageEvent(device, message);
			};
			this.updateScanner.ErrorMessageEvent += delegate(string message)
			{
				<>4__this.ErrorMessageEvent(device, message);
			};
			this.updateScanner.LogMessageEvent += delegate(string message)
			{
				<>4__this.LogMessageEvent(device, message);
			};
			this.evaluator.ProgressEvent += delegate(string message)
			{
				<>4__this.ProgressEvent(device, message);
			};
			this.evaluator.NormalMessageEvent += delegate(string message)
			{
				<>4__this.NormalMessageEvent(device, message);
			};
			this.evaluator.WarningMessageEvent += delegate(string message)
			{
				<>4__this.WarningMessageEvent(device, message);
			};
			this.evaluator.ErrorMessageEvent += delegate(string message)
			{
				<>4__this.ErrorMessageEvent(device, message);
			};
			this.evaluator.LogMessageEvent += delegate(string message)
			{
				<>4__this.LogMessageEvent(device, message);
			};
			this.updateScannerUUP.ProgressEvent += delegate(string message)
			{
				<>4__this.ProgressEvent(device, message);
			};
			this.updateScannerUUP.NormalMessageEvent += delegate(string message)
			{
				<>4__this.NormalMessageEvent(device, message);
			};
			this.updateScannerUUP.WarningMessageEvent += delegate(string message)
			{
				<>4__this.WarningMessageEvent(device, message);
			};
			this.updateScannerUUP.ErrorMessageEvent += delegate(string message)
			{
				<>4__this.ErrorMessageEvent(device, message);
			};
			this.updateScannerUUP.LogMessageEvent += delegate(string message)
			{
				<>4__this.LogMessageEvent(device, message);
			};
			if (Evaluator.GetDebugLoggingEnabled() && !this.logged)
			{
				if (Environment.GetEnvironmentVariable("OTC_PACKAGES_PATH") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_PACKAGES_PATH={0}", Environment.GetEnvironmentVariable("OTC_PACKAGES_PATH")));
				}
				if (Environment.GetEnvironmentVariable("OTC_INTERNAL_URL") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_INTERNAL_URL={0}", Environment.GetEnvironmentVariable("OTC_INTERNAL_URL")));
				}
				if (Environment.GetEnvironmentVariable("OTC_FLIGHT_BRANCH") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_FLIGHT_BRANCH={0}", Environment.GetEnvironmentVariable("OTC_FLIGHT_BRANCH")));
				}
				if (Environment.GetEnvironmentVariable("OTC_FLIGHT_RING") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_FLIGHT_RING={0}", Environment.GetEnvironmentVariable("OTC_FLIGHT_RING")));
				}
				if (Environment.GetEnvironmentVariable("OTC_DISABLE_FILENAME_HASH") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_DISABLE_FILENAME_HASH={0}", Environment.GetEnvironmentVariable("OTC_DISABLE_FILENAME_HASH")));
				}
				if (Environment.GetEnvironmentVariable("OTC_USE_PRIVATE_PACKAGES") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_USE_PRIVATE_PACKAGES={0}", Environment.GetEnvironmentVariable("OTC_USE_PRIVATE_PACKAGES")));
				}
				if (Environment.GetEnvironmentVariable("OTC_MUV6_URL") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_MUV6_URL={0}", Environment.GetEnvironmentVariable("OTC_MUV6_URL")));
				}
				if (Environment.GetEnvironmentVariable("OTC_UUP_URL") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_UUP_URL={0}", Environment.GetEnvironmentVariable("OTC_UUP_URL")));
				}
				if (Environment.GetEnvironmentVariable("OTC_UUP_SERVICE_VERSION") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_UUP_SERVICE_VERSION={0}", Environment.GetEnvironmentVariable("OTC_UUP_SERVICE_VERSION")));
				}
				if (Environment.GetEnvironmentVariable("OTC_SKIP_PUSHUPDATES") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_SKIP_PUSHUPDATES={0}", Environment.GetEnvironmentVariable("OTC_SKIP_PUSHUPDATES")));
				}
				if (Environment.GetEnvironmentVariable("OTC_TRUSTED_HOST") != null)
				{
					View.Instance.LogDeviceMessage(device, string.Format("OTC_TRUSTED_HOST={0}", Environment.GetEnvironmentVariable("OTC_TRUSTED_HOST")));
				}
				this.logged = true;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029B0 File Offset: 0x00000BB0
		public void StartUpdate(bool batchFlag)
		{
			object obj = this.mutex;
			lock (obj)
			{
				this.thread = new Thread(delegate()
				{
					this.Update(batchFlag);
				});
				this.thread.Start();
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A20 File Offset: 0x00000C20
		public void StartCollectLogs(bool batchFlag)
		{
			object obj = this.mutex;
			lock (obj)
			{
				this.thread = new Thread(delegate()
				{
					this.CollectLogs(batchFlag);
				});
				this.thread.Start();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A90 File Offset: 0x00000C90
		public void Stop()
		{
			object obj = this.mutex;
			lock (obj)
			{
				this.stop = true;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private bool IsStopping()
		{
			object obj = this.mutex;
			bool result;
			lock (obj)
			{
				result = this.stop;
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B18 File Offset: 0x00000D18
		private void Update(bool batchFlag)
		{
			try
			{
				while (!this.IsDeviceReady())
				{
					if (batchFlag)
					{
						return;
					}
					if (this.IsStopping())
					{
						this.WarningMessageEvent(this.device, "Operation stopped at user request.");
						return;
					}
					Thread.Sleep(3000);
				}
				bool flag = false;
				if ("0X4" == this.device.DuSendIuPackageOptions)
				{
					flag = true;
				}
				DeviceInfoFromCab deviceInfoFromCab = null;
				if (!flag)
				{
					deviceInfoFromCab = this.GetDeviceInfoProvider();
					if (deviceInfoFromCab == null)
					{
						return;
					}
					this.evaluator.DeviceInfoProvider = deviceInfoFromCab;
				}
				if (this.IsStopping())
				{
					this.WarningMessageEvent(this.device, "Operation stopped at user request.");
				}
				else
				{
					HttpDownloader downloader = new HttpDownloader();
					JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
					javaScriptSerializer.MaxJsonLength = int.MaxValue;
					DownloadInfo[] array = null;
					this.NormalMessageEvent(this.device, string.Format("Scanning for updates...", new object[0]));
					if (this.GetUsePrivatePackages())
					{
						new Dictionary<string, DownloadInfo>();
						string text = Path.Combine(PackageDownloader.Instance.DownloadPath, this.UpdateDownloadInformationFile);
						if (!File.Exists(text))
						{
							throw new FileNotFoundException(string.Format("File {0} not found, cannot use Private Package folder.", text), text);
						}
						array = javaScriptSerializer.Deserialize<DownloadInfo[]>(File.ReadAllText(Path.Combine(PackageDownloader.Instance.DownloadPath, this.UpdateDownloadInformationFile)));
					}
					else if (flag)
					{
						this.EnableTrustedHosts(UpdateScannerUUP.GetWuServiceRequestUri());
						array = this.updateScannerUUP.Scan(this.device.DuCTAC, this.device.DuProductNames);
					}
					else
					{
						this.EnableTrustedHosts(this.GetWuServiceRequestUri());
						array = this.updateScanner.Scan(deviceInfoFromCab, this.evaluator, downloader);
					}
					if (array != null)
					{
						int num = array.Length;
						if (!this.GetUsePrivatePackages())
						{
							File.WriteAllText(Path.Combine(PackageDownloader.Instance.DownloadPath, this.UpdateDownloadInformationFile), javaScriptSerializer.Serialize(array));
						}
						if (!flag)
						{
							foreach (string arg in this.updateScanner.GetUpdateTitles(this.evaluator))
							{
								this.NormalMessageEvent(this.device, string.Format("Found update: {0}", arg));
							}
							this.NormalMessageEvent(this.device, string.Format("{0} installable packages found", array.Length));
						}
						else
						{
							int num2 = 0;
							int num3 = 0;
							bool flag2 = false;
							foreach (DownloadInfo downloadInfo in array)
							{
								if (downloadInfo.ContentType.Equals("ServicingStack"))
								{
									num2++;
									num3++;
								}
								else if (downloadInfo.ContentType.Equals("Metadata"))
								{
									num3++;
								}
								if (string.IsNullOrEmpty(downloadInfo.SHA256Hash))
								{
									this.ErrorMessageEvent(this.device, string.Format("Publishing error: {0} -> has null hash", downloadInfo.Name));
									flag2 = true;
								}
							}
							if (flag2)
							{
								this.ErrorMessageEvent(this.device, "Publishing error: Null hashes detected.");
								return;
							}
							if (1 != num2)
							{
								this.ErrorMessageEvent(this.device, string.Format("Publishing error: {0} UpdateAgent packages found.", num2));
								return;
							}
							int num4 = 0;
							foreach (DownloadInfo downloadInfo2 in array)
							{
								if (downloadInfo2.ContentType.Equals("ServicingStack"))
								{
									if (this.IsStopping())
									{
										this.WarningMessageEvent(this.device, "Operation stopped at user request.");
										return;
									}
									num4++;
									this.ProgressEvent(this.device, string.Format("UUP {0}/{1}: Downloading UpdateAgent [{2}]", num4, num3, downloadInfo2.Name));
									PackageDownloader.Instance.DownloadUpdate(downloadInfo2);
								}
								else if (downloadInfo2.ContentType.Equals("Metadata"))
								{
									if (this.IsStopping())
									{
										this.WarningMessageEvent(this.device, "Operation stopped at user request.");
										return;
									}
									num4++;
									this.ProgressEvent(this.device, string.Format("UUP {0}/{1}: Downloading CompDB [{2}]", num4, num3, downloadInfo2.Name));
									PackageDownloader.Instance.DownloadUpdate(downloadInfo2);
								}
							}
							this.NormalMessageEvent(this.device, string.Format("Clearing staging directory...", new object[0]));
							this.device.ClearDuStagingDirectory();
							num4 = 0;
							foreach (DownloadInfo downloadInfo3 in array)
							{
								if (downloadInfo3.ContentType.Equals("ServicingStack"))
								{
									if (this.IsStopping())
									{
										this.WarningMessageEvent(this.device, "Operation stopped at user request.");
										return;
									}
									num4++;
									this.ProgressEvent(this.device, string.Format("UUP {0}/{1}: Sending UpdateAgent [{2}] to device", num4, num3, downloadInfo3.Name));
									this.device.SendUpdateAgent(PackageDownloader.Instance.GetDownloadPath(downloadInfo3));
								}
								else if (downloadInfo3.ContentType.Equals("Metadata"))
								{
									if (this.IsStopping())
									{
										this.WarningMessageEvent(this.device, "Operation stopped at user request.");
										return;
									}
									num4++;
									this.ProgressEvent(this.device, string.Format("UUP {0}/{1}: Sending CompDB [{2}] to device", num4, num3, downloadInfo3.Name));
									this.device.SendCompositionDB(PackageDownloader.Instance.GetDownloadPath(downloadInfo3));
								}
							}
							this.NormalMessageEvent(this.device, string.Format("Starting device scan...", new object[0]));
							this.device.StartDeviceUpdateOtcScan();
							this.NormalMessageEvent(this.device, string.Format("Waiting for device scan to complete...", new object[0]));
							string text2 = "0X1";
							while ("0X1" == text2)
							{
								Thread.Sleep(1000);
								text2 = this.device.DuUpdateAgentError;
							}
							if ("0XC1900401" == text2)
							{
								this.WarningMessageEvent(this.device, "Device is already up to date.");
								return;
							}
							if ("0X0" != text2)
							{
								this.WarningMessageEvent(this.device, string.Format("Device error: {0}", text2));
								return;
							}
							object obj = this.actionListMutex;
							lock (obj)
							{
								string text3 = Path.Combine(PackageDownloader.Instance.DownloadPath, "ActionList.xml");
								this.NormalMessageEvent(this.device, string.Format("Getting action list...", new object[0]));
								this.device.GetActionList(text3);
								num = this.updateScannerUUP.ProcessActionList(array, text3);
							}
							if (num == 0)
							{
								this.WarningMessageEvent(this.device, "No packages to download!");
								return;
							}
						}
						int num5 = 1;
						foreach (DownloadInfo downloadInfo4 in array)
						{
							if (this.IsStopping())
							{
								this.WarningMessageEvent(this.device, "Operation stopped at user request.");
								return;
							}
							if (!flag || downloadInfo4.Installable)
							{
								this.ProgressEvent(this.device, string.Format("{0}/{1}: Downloading {2}", num5++, num, downloadInfo4.Name));
								PackageDownloader.Instance.DownloadUpdate(downloadInfo4);
							}
						}
						try
						{
							if (!flag)
							{
								this.NormalMessageEvent(this.device, string.Format("Clearing staging directory...", new object[0]));
								this.device.ClearDuStagingDirectory();
							}
							num5 = 1;
							foreach (DownloadInfo downloadInfo5 in array)
							{
								if (this.IsStopping())
								{
									this.WarningMessageEvent(this.device, "Operation stopped at user request.");
									return;
								}
								if (!this.GetSkipPushUpdates() && (!flag || downloadInfo5.Installable))
								{
									this.ProgressEvent(this.device, string.Format("{0}/{1}: Sending {2} to device", num5++, num, downloadInfo5.Name));
									if (flag)
									{
										this.device.SendIuPackage(PackageDownloader.Instance.GetDownloadPath(downloadInfo5), downloadInfo5.Name);
									}
									else
									{
										using (FileStream stream = PackageDownloader.Instance.GetStream(downloadInfo5))
										{
											this.device.SendIuPackage(stream);
										}
									}
								}
							}
							if (!this.GetSkipPushUpdates())
							{
								this.NormalMessageEvent(this.device, string.Format("{0} packages sent to the device", num));
								this.NormalMessageEvent(this.device, "Starting update on device...");
								this.device.InitiateDuInstall();
								this.NormalMessageEvent(this.device, "Update successfully started.  Please disconnect the device and monitor the update progress in Settings on your device.");
							}
							else
							{
								this.NormalMessageEvent(this.device, "OTC_SKIP_PUSHUPDATES is set, not updating device.");
							}
							goto IL_9B7;
						}
						catch (MtpException)
						{
							this.ErrorMessageEvent(this.device, "Communications error.  Try disconnecting and reconnecting the device.");
							this.CollectLogs(batchFlag);
							goto IL_9B7;
						}
						catch (Exception ex)
						{
							if ("InitiateDuInstall failed, response code: 8194" == ex.Message)
							{
								this.ErrorMessageEvent(this.device, "Update error: 8194.  Please check if there is an update already in progress.");
							}
							else
							{
								this.ErrorMessageEvent(this.device, string.Format("Update error: {0}", ex.Message));
							}
							this.CollectLogs(batchFlag);
							goto IL_9B7;
						}
					}
					this.NormalMessageEvent(this.device, "No updates are available for this device.  Please disconnect the device.");
					IL_9B7:;
				}
			}
			catch (MtpException)
			{
				this.ErrorMessageEvent(this.device, "Communications error.  Try disconnecting and reconnecting the device.");
			}
			catch (Exception ex2)
			{
				this.ErrorMessageEvent(this.device, ex2.Message);
				if (Evaluator.GetDebugLoggingEnabled())
				{
					this.LogMessageEvent(this.device, this.GetFullMessage(ex2));
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000035C8 File Offset: 0x000017C8
		private bool IsDeviceReady()
		{
			if (!this.device.IsLocked)
			{
				uint num = Convert.ToUInt32(this.device.DuResult, 16);
				if (int.Parse(this.device.BatteryLife) < 40)
				{
					if (this.deviceDetectionState != DeviceUpdater.DeviceDetectionState.LowBattery)
					{
						View.Instance.ShowDeviceHeader(this.device, string.Format("{0}({1}) {2}", this.device.Model, this.device.OemDeviceName, this.device.OsVersion));
						this.deviceDetectionState = DeviceUpdater.DeviceDetectionState.LowBattery;
						this.WarningMessageEvent(this.device, string.Format("Device battery is less than {0}%.  Please ensure the device is charged to at least {0}%.", 40));
					}
				}
				else
				{
					if (num != 2149884176U && num != 2149089883U)
					{
						View.Instance.ShowDeviceHeader(this.device, string.Format("{0}({1}) {2}", this.device.Model, this.device.OemDeviceName, this.device.OsVersion));
						return true;
					}
					if (this.deviceDetectionState != DeviceUpdater.DeviceDetectionState.UpdateInProgress)
					{
						View.Instance.ShowDeviceHeader(this.device, string.Format("{0}({1}) {2}", this.device.Model, this.device.OemDeviceName, this.device.OsVersion));
						this.deviceDetectionState = DeviceUpdater.DeviceDetectionState.UpdateInProgress;
						this.WarningMessageEvent(this.device, "An update is in progress.  Please disconnect the device until the update is complete.");
					}
				}
			}
			else if (this.deviceDetectionState != DeviceUpdater.DeviceDetectionState.DeviceLocked)
			{
				View.Instance.ShowDeviceHeader(this.device, "Device information not available");
				this.deviceDetectionState = DeviceUpdater.DeviceDetectionState.DeviceLocked;
				this.WarningMessageEvent(this.device, "Device is locked.  Please ensure the device is unlocked.");
			}
			return false;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003770 File Offset: 0x00001970
		private DeviceInfoFromCab GetDeviceInfoProvider()
		{
			DeviceInfoFromCab result;
			try
			{
				this.NormalMessageEvent(this.device, "Getting logs from device...");
				string tempFileName = Path.GetTempFileName();
				this.device.GetDuDiagnostics(tempFileName);
				DeviceInfoFromCab deviceInfoFromCab = new DeviceInfoFromCab();
				deviceInfoFromCab.DeviceID = this.device.SerialNumber;
				deviceInfoFromCab.ProgressEvent += delegate(string deviceObject, string message)
				{
					this.ProgressEvent(this.device, message);
				};
				deviceInfoFromCab.NormalMessageEvent += delegate(string deviceObject, string message)
				{
					this.NormalMessageEvent(this.device, message);
				};
				deviceInfoFromCab.WarningMessageEvent += delegate(string deviceObject, string message)
				{
					this.WarningMessageEvent(this.device, message);
				};
				deviceInfoFromCab.ErrorMessageEvent += delegate(string deviceObject, string message)
				{
					this.ErrorMessageEvent(this.device, message);
				};
				deviceInfoFromCab.Load(tempFileName);
				File.Delete(tempFileName);
				if (string.Compare(deviceInfoFromCab.GetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\DeviceUpdate\\Agent\\Engine", "StoredSessionID"), "{51b519d5-b6f5-4333-8df6-e74d7c9aead4}", true) == 0)
				{
					this.WarningMessageEvent(this.device, "An update is in progress.  Please disconnect the device until the update is complete.");
					result = null;
				}
				else
				{
					deviceInfoFromCab.SetRegistryValue("HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\DeviceUpdate\\Agent\\Protocol", "TestTarget", "d369c9b6-2379-466d-9162-afc53361e3c2");
					deviceInfoFromCab.SetRegistryValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\DeviceUpdate\\Agent\\Engine", "StoredSessionId", "{51b519d5-b6f5-4333-8df6-e74d7c9aead4}");
					result = deviceInfoFromCab;
				}
			}
			catch (MtpException)
			{
				this.ErrorMessageEvent(this.device, "Communications error.  Try disconnecting and reconnecting the device.");
				result = null;
			}
			catch (Exception ex)
			{
				this.ErrorMessageEvent(this.device, ex.Message);
				result = null;
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000038CC File Offset: 0x00001ACC
		private void CollectLogs(bool batchFlag)
		{
			if (batchFlag)
			{
				return;
			}
			if (!this.IsDeviceReady())
			{
				this.WarningMessageEvent(this.device, "There's still an operation in progress, logs may not be complete.");
			}
			this.NormalMessageEvent(this.device, "Collecting logs.  Please leave the device connected.");
			try
			{
				Directory.CreateDirectory(this.logPath);
				string text = Path.Combine(this.logPath, this.device.SerialNumber + ".logs.cab");
				this.device.GetDuDiagnostics(text);
				this.NormalMessageEvent(this.device, string.Format("Logs saved to {0}.", text));
			}
			catch (MtpException)
			{
				this.ErrorMessageEvent(this.device, "Communications error.  Try disconnecting and reconnecting the device.");
			}
			catch (Exception)
			{
				this.ErrorMessageEvent(this.device, string.Format("Log collection failed.", new object[0]));
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000039C4 File Offset: 0x00001BC4
		public void Join()
		{
			if (this.thread != null)
			{
				this.thread.Join();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000039DC File Offset: 0x00001BDC
		private void EnableTrustedHosts(string url)
		{
			Uri uri = new Uri(url);
			string[] TrustedHosts = new string[]
			{
				uri.Host
			};
			ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
			{
				if (errors == SslPolicyErrors.None)
				{
					return true;
				}
				if (Evaluator.GetDebugLoggingEnabled())
				{
					this.LogMessageEvent(this.device, string.Format("Certificate Validation Failure:", new object[0]));
					this.LogMessageEvent(this.device, string.Format("  Errors: {0}", errors));
					this.LogMessageEvent(this.device, string.Format("  Certificate: {0}", certificate));
					this.LogMessageEvent(this.device, string.Format("  Chain:", new object[0]));
					foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
					{
						this.LogMessageEvent(this.device, string.Format("    Status: {0}", x509ChainStatus.Status));
						this.LogMessageEvent(this.device, string.Format("    Information: {0}", x509ChainStatus.StatusInformation));
					}
				}
				HttpWebRequest httpWebRequest = sender as HttpWebRequest;
				return httpWebRequest != null && this.GetTrustedHost() && TrustedHosts.Contains(httpWebRequest.RequestUri.Host);
			};
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003A24 File Offset: 0x00001C24
		private string GetAggregateExceptionString(Exception error)
		{
			IList<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			if (error is AggregateException)
			{
				using (IEnumerator<Exception> enumerator = ((AggregateException)error).InnerExceptions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Exception ex = enumerator.Current;
						string item = ex.ToString();
						if (!list.Contains(item))
						{
							stringBuilder.AppendLine(ex.ToString());
							list.Add(item);
						}
					}
					goto IL_76;
				}
			}
			stringBuilder.AppendLine(error.ToString());
			IL_76:
			return stringBuilder.ToString();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003AC0 File Offset: 0x00001CC0
		private string GetFullMessage(Exception error)
		{
			if (error.InnerException != null)
			{
				return string.Format("{0} --> {1}", error.Message, this.GetFullMessage(error.InnerException));
			}
			return error.Message;
		}

		// Token: 0x04000009 RID: 9
		private const int MinimumBatteryLevel = 40;

		// Token: 0x0400000A RID: 10
		private const uint USO_E_SESSION_IN_PROGRESS = 2149884176U;

		// Token: 0x0400000B RID: 11
		private const uint DUA_E_SESSION_IN_PROGRESS = 2149089883U;

		// Token: 0x0400000C RID: 12
		private UpdateScanner updateScanner = new UpdateScanner();

		// Token: 0x0400000D RID: 13
		private UpdateScannerUUP updateScannerUUP = new UpdateScannerUUP();

		// Token: 0x0400000E RID: 14
		private Evaluator evaluator = new Evaluator();

		// Token: 0x0400000F RID: 15
		private DeviceUpdater.DeviceDetectionState deviceDetectionState;

		// Token: 0x04000010 RID: 16
		private IWpdDevice device;

		// Token: 0x04000011 RID: 17
		private string logPath;

		// Token: 0x04000012 RID: 18
		private Process consoleProcess = new Process();

		// Token: 0x04000013 RID: 19
		private object mutex = new object();

		// Token: 0x04000014 RID: 20
		private object actionListMutex = new object();

		// Token: 0x04000015 RID: 21
		private Thread thread;

		// Token: 0x04000016 RID: 22
		private bool stop;

		// Token: 0x04000017 RID: 23
		private string UpdateDownloadInformationFile = "UpdateDownloadInformation.json";

		// Token: 0x04000018 RID: 24
		private string ServiceUrl = "https://fe2.update.microsoft.com/v6/";

		// Token: 0x04000019 RID: 25
		private bool logged;

		// Token: 0x02000008 RID: 8
		// (Invoke) Token: 0x06000064 RID: 100
		public delegate void MessageCallback(IWpdDevice device, string message);

		// Token: 0x02000009 RID: 9
		private enum DeviceDetectionState
		{
			// Token: 0x04000026 RID: 38
			Uninitialized,
			// Token: 0x04000027 RID: 39
			DeviceLocked,
			// Token: 0x04000028 RID: 40
			LowBattery,
			// Token: 0x04000029 RID: 41
			UpdateInProgress
		}
	}
}
