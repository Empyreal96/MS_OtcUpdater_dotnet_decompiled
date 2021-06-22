using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Tools.DeviceUpdate.DeviceUtils;

namespace OtcUpdater
{
	// Token: 0x02000007 RID: 7
	internal class ConsoleView : IView
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00004044 File Offset: 0x00002244
		public ConsoleView()
		{
			this.oldForegroundColor = Console.ForegroundColor;
			this.oldBackgroundColor = Console.BackgroundColor;
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Gray;
			this.lastWidth = Console.WindowWidth;
			this.lastHeight = Console.WindowHeight;
			string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "logs");
			Directory.CreateDirectory(text);
			string path = Path.Combine(text, DateTime.Now.ToString("yyyyMMdd-HHmmssffff") + ".log");
			this.logWriter = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read));
			this.logWriter.AutoFlush = true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000410C File Offset: 0x0000230C
		public void Restore()
		{
			Console.BackgroundColor = this.oldBackgroundColor;
			Console.ForegroundColor = this.oldForegroundColor;
			Console.CursorLeft = 0;
			Console.CursorTop = 4 + 2 * this.deviceIndex;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004139 File Offset: 0x00002339
		public void ClearInputBuffer()
		{
			while (Console.KeyAvailable)
			{
				Console.ReadKey(true);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000414C File Offset: 0x0000234C
		public char PollForKey()
		{
			if (Console.KeyAvailable)
			{
				return Console.ReadKey(true).KeyChar;
			}
			return '\0';
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004170 File Offset: 0x00002370
		public char WaitForKey()
		{
			this.ClearInputBuffer();
			return Console.ReadKey(true).KeyChar;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004191 File Offset: 0x00002391
		public void ShowHeader(string header)
		{
			this.ShowHeader(header, false);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000419C File Offset: 0x0000239C
		private void ShowHeader(string header, bool refreshing)
		{
			object obj = this.mutex;
			lock (obj)
			{
				this.header = header;
				if (!refreshing)
				{
					this.Refresh();
				}
				Console.CursorTop = 0;
				Console.CursorLeft = 0;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(this.header);
				this.logWriter.WriteLine(this.header);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004218 File Offset: 0x00002418
		public void LogMessage(string message)
		{
			this.ShowMessage(ConsoleColor.Gray, message, false, false);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004224 File Offset: 0x00002424
		public void ShowNormalMessage(string message)
		{
			this.ShowMessage(ConsoleColor.Gray, message, false, true);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004230 File Offset: 0x00002430
		public void ShowWarningMessage(string message)
		{
			this.ShowMessage(ConsoleColor.Yellow, message, false, true);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000423D File Offset: 0x0000243D
		public void ShowErrorMessage(string message)
		{
			this.ShowMessage(ConsoleColor.Red, message, false, true);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004224 File Offset: 0x00002424
		public void ShowProgressMessage(string message)
		{
			this.ShowMessage(ConsoleColor.Gray, message, false, true);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000424C File Offset: 0x0000244C
		private void ShowMessage(ConsoleColor color, string message, bool refreshing, bool visible)
		{
			object obj = this.mutex;
			lock (obj)
			{
				this.status.color = color;
				this.status.message = message;
				if (visible)
				{
					if (!refreshing)
					{
						this.Refresh();
					}
					Console.CursorTop = 2;
					Console.CursorLeft = 0;
					Console.ForegroundColor = this.status.color;
					Console.Write(this.PadMessage(this.status.message));
				}
				this.logWriter.WriteLine(this.status.message);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000042F4 File Offset: 0x000024F4
		public void ShowDeviceHeader(IWpdDevice device, string header)
		{
			this.ShowDeviceHeader((device == null) ? string.Empty : device.SerialNumber, header, false);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004310 File Offset: 0x00002510
		private void ShowDeviceHeader(string serialNumber, string header, bool refreshing)
		{
			object obj = this.mutex;
			lock (obj)
			{
				ConsoleView.DeviceInfo deviceInfo = this.GetDeviceInfo(serialNumber);
				deviceInfo.header = header;
				if (!refreshing)
				{
					this.Refresh();
				}
				Console.CursorLeft = 0;
				Console.CursorTop = 4 + 2 * deviceInfo.row;
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write(this.PadMessage(string.Format("{0}: {1}", serialNumber, deviceInfo.header)));
				this.logWriter.WriteLine(string.Format("{0}: {1}", serialNumber, deviceInfo.header));
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000043B8 File Offset: 0x000025B8
		public void LogDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, ConsoleColor.Gray, message, false, false);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000043C5 File Offset: 0x000025C5
		public void ShowNormalDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, ConsoleColor.Gray, message, false, true);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000043D2 File Offset: 0x000025D2
		public void ShowWarningDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, ConsoleColor.Yellow, message, false, true);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000043E0 File Offset: 0x000025E0
		public void ShowErrorDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, ConsoleColor.Red, message, false, true);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000043C5 File Offset: 0x000025C5
		public void ShowProgressDeviceMessage(IWpdDevice device, string message)
		{
			this.ShowDeviceMessage(device, ConsoleColor.Gray, message, false, true);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000043EE File Offset: 0x000025EE
		private void ShowDeviceMessage(IWpdDevice device, ConsoleColor color, string message, bool refreshing, bool visible)
		{
			this.ShowDeviceMessage((device == null) ? string.Empty : device.SerialNumber, color, message, refreshing, visible);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000440C File Offset: 0x0000260C
		private void ShowDeviceMessage(string serialNumber, ConsoleColor color, string message, bool refreshing, bool visible)
		{
			object obj = this.mutex;
			lock (obj)
			{
				ConsoleView.DeviceInfo deviceInfo = this.GetDeviceInfo(serialNumber);
				deviceInfo.status.color = color;
				deviceInfo.status.message = message;
				if (visible)
				{
					if (!refreshing)
					{
						this.Refresh();
					}
					Console.CursorLeft = 0;
					Console.CursorTop = 4 + 2 * deviceInfo.row + 1;
					Console.ForegroundColor = deviceInfo.status.color;
					Console.Write(this.PadMessage(" " + deviceInfo.status.message));
				}
				this.logWriter.WriteLine(string.Format("{0}: {1}", serialNumber, deviceInfo.status.message));
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000044DC File Offset: 0x000026DC
		private ConsoleView.DeviceInfo GetDeviceInfo(string serialNumber)
		{
			ConsoleView.DeviceInfo deviceInfo;
			if (this.deviceInfoMap.ContainsKey(serialNumber))
			{
				deviceInfo = this.deviceInfoMap[serialNumber];
			}
			else
			{
				deviceInfo = new ConsoleView.DeviceInfo();
				ConsoleView.DeviceInfo deviceInfo2 = deviceInfo;
				int num = this.deviceIndex;
				this.deviceIndex = num + 1;
				deviceInfo2.row = num;
				this.deviceInfoMap[serialNumber] = deviceInfo;
			}
			return deviceInfo;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004531 File Offset: 0x00002731
		private string PadMessage(string message)
		{
			return message.Substring(0, Math.Min(message.Length, Console.WindowWidth - 1)).PadRight(Console.WindowWidth - 1);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004558 File Offset: 0x00002758
		private void Refresh()
		{
			if (Console.WindowWidth == this.lastWidth && Console.WindowHeight == this.lastHeight)
			{
				return;
			}
			this.lastWidth = Console.WindowWidth;
			this.lastHeight = Console.WindowHeight;
			Console.Clear();
			this.ShowHeader(this.header, true);
			this.ShowMessage(this.status.color, this.status.message, true, true);
			foreach (string serialNumber in this.deviceInfoMap.Keys)
			{
				ConsoleView.DeviceInfo deviceInfo = this.GetDeviceInfo(serialNumber);
				this.ShowDeviceHeader(serialNumber, deviceInfo.header, true);
				this.ShowDeviceMessage(serialNumber, deviceInfo.status.color, deviceInfo.status.message, true, true);
			}
		}

		// Token: 0x0400001B RID: 27
		private string header;

		// Token: 0x0400001C RID: 28
		private ConsoleView.Status status;

		// Token: 0x0400001D RID: 29
		private Dictionary<string, ConsoleView.DeviceInfo> deviceInfoMap = new Dictionary<string, ConsoleView.DeviceInfo>();

		// Token: 0x0400001E RID: 30
		private object mutex = new object();

		// Token: 0x0400001F RID: 31
		private int deviceIndex;

		// Token: 0x04000020 RID: 32
		private StreamWriter logWriter;

		// Token: 0x04000021 RID: 33
		private ConsoleColor oldForegroundColor;

		// Token: 0x04000022 RID: 34
		private ConsoleColor oldBackgroundColor;

		// Token: 0x04000023 RID: 35
		private int lastWidth;

		// Token: 0x04000024 RID: 36
		private int lastHeight;

		// Token: 0x0200000E RID: 14
		private struct Status
		{
			// Token: 0x04000032 RID: 50
			public ConsoleColor color;

			// Token: 0x04000033 RID: 51
			public string message;
		}

		// Token: 0x0200000F RID: 15
		private class DeviceInfo
		{
			// Token: 0x04000034 RID: 52
			public int row;

			// Token: 0x04000035 RID: 53
			public string header;

			// Token: 0x04000036 RID: 54
			public ConsoleView.Status status;
		}
	}
}
