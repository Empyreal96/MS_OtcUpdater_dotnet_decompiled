using System;
using System.IO;
using System.Reflection;
using Microsoft.Tools.DeviceUpdate.DeviceUtils;

namespace OtcUpdater
{
	// Token: 0x02000004 RID: 4
	internal class BatchView : IView
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000205C File Offset: 0x0000025C
		public BatchView()
		{
			string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "logs");
			Directory.CreateDirectory(text);
			string path = Path.Combine(text, DateTime.Now.ToString("yyyyMMdd-HHmmssffff") + ".log");
			this.logWriter = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read));
			this.logWriter.AutoFlush = true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002058 File Offset: 0x00000258
		public void Restore()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002058 File Offset: 0x00000258
		public void ClearInputBuffer()
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000020DC File Offset: 0x000002DC
		public char PollForKey()
		{
			return '\0';
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000020DC File Offset: 0x000002DC
		public char WaitForKey()
		{
			return '\0';
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000020DF File Offset: 0x000002DF
		public void LogMessage(string message)
		{
			this.ShowMessage(message, false);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000020E9 File Offset: 0x000002E9
		public void ShowNormalMessage(string message)
		{
			this.ShowMessage(message, true);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000020F4 File Offset: 0x000002F4
		public void ShowHeader(string header)
		{
			object obj = this.mutex;
			lock (obj)
			{
				Console.WriteLine(header);
				this.logWriter.WriteLine(header);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000020E9 File Offset: 0x000002E9
		public void ShowWarningMessage(string message)
		{
			this.ShowMessage(message, true);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002140 File Offset: 0x00000340
		public void ShowErrorMessage(string message)
		{
			object obj = this.mutex;
			lock (obj)
			{
				Console.WriteLine(message);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000020E9 File Offset: 0x000002E9
		public void ShowProgressMessage(string message)
		{
			this.ShowMessage(message, true);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002180 File Offset: 0x00000380
		private void ShowMessage(string message, bool visible)
		{
			object obj = this.mutex;
			lock (obj)
			{
				if (visible)
				{
					Console.WriteLine(message);
				}
				this.logWriter.WriteLine(message);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021D0 File Offset: 0x000003D0
		public void ShowDeviceHeader(IWpdDevice device, string header)
		{
			object obj = this.mutex;
			lock (obj)
			{
				string value = string.Format("{0}: {1}", device.SerialNumber, header);
				Console.WriteLine(value);
				this.logWriter.WriteLine(value);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002230 File Offset: 0x00000430
		public void LogDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, message, false);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000223B File Offset: 0x0000043B
		public void ShowNormalDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, message, true);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000223B File Offset: 0x0000043B
		public void ShowWarningDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, message, true);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000223B File Offset: 0x0000043B
		public void ShowErrorDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, message, true);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000223B File Offset: 0x0000043B
		public void ShowProgressDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, message, true);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002248 File Offset: 0x00000448
		private void ShowDeviceMessage(IWpdDevice device, string message, bool visible)
		{
			object obj = this.mutex;
			lock (obj)
			{
				message = string.Format("{0}: {1}", (device == null) ? string.Empty : device.SerialNumber, message);
				if (visible)
				{
					Console.WriteLine(message);
				}
				this.logWriter.WriteLine(message);
			}
		}

		// Token: 0x04000002 RID: 2
		private object mutex = new object();

		// Token: 0x04000003 RID: 3
		private StreamWriter logWriter;
	}
}
