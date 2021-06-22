using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x02000054 RID: 84
	public class IULogger : IDeploymentLogger
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0000AE40 File Offset: 0x00009040
		public IULogger()
		{
			this.MinLogLevel = LoggingLevel.Debug;
			this.LoggingMessage.Add(LoggingLevel.Debug, "DEBUG");
			this.LoggingMessage.Add(LoggingLevel.Info, "INFO");
			this.LoggingMessage.Add(LoggingLevel.Warning, "WARNING");
			this.LoggingMessage.Add(LoggingLevel.Error, "ERROR");
			this.LoggingFunctions.Add(LoggingLevel.Debug, new LogString(IULogger.LogToConsole));
			this.LoggingFunctions.Add(LoggingLevel.Info, new LogString(IULogger.LogToConsole));
			this.LoggingFunctions.Add(LoggingLevel.Warning, new LogString(IULogger.LogToError));
			this.LoggingFunctions.Add(LoggingLevel.Error, new LogString(IULogger.LogToError));
			this.LoggingColors.Add(LoggingLevel.Debug, ConsoleColor.DarkGray);
			this.LoggingColors.Add(LoggingLevel.Info, ConsoleColor.Gray);
			this.LoggingColors.Add(LoggingLevel.Warning, ConsoleColor.Yellow);
			this.LoggingColors.Add(LoggingLevel.Error, ConsoleColor.Red);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AF55 File Offset: 0x00009155
		public static void LogToConsole(string format, params object[] list)
		{
			if (list.Length != 0)
			{
				Console.WriteLine(string.Format(CultureInfo.CurrentCulture, format, list));
				return;
			}
			Console.WriteLine(format);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AF73 File Offset: 0x00009173
		public static void LogToError(string format, params object[] list)
		{
			if (list.Length != 0)
			{
				Console.Error.WriteLine(string.Format(CultureInfo.CurrentCulture, format, list));
				return;
			}
			Console.Error.WriteLine(format);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000964B File Offset: 0x0000784B
		public static void LogToNull(string format, params object[] list)
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AF9B File Offset: 0x0000919B
		public void SetLoggingLevel(LoggingLevel level)
		{
			this.MinLogLevel = level;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000AFA4 File Offset: 0x000091A4
		public void SetLogFunction(LoggingLevel level, LogString logFunc)
		{
			if (logFunc == null)
			{
				this.LoggingFunctions[level] = new LogString(IULogger.LogToNull);
				return;
			}
			this.LoggingFunctions[level] = logFunc;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000AFCF File Offset: 0x000091CF
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000AFD7 File Offset: 0x000091D7
		public ConsoleColor OverrideColor
		{
			get
			{
				return this._overrideColor;
			}
			set
			{
				this._overrideColor = value;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public void ResetOverrideColor()
		{
			this._overrideColor = ConsoleColor.Black;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000AFE9 File Offset: 0x000091E9
		public bool UseOverrideColor
		{
			get
			{
				return this._overrideColor > ConsoleColor.Black;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public void Log(LoggingLevel level, string format, params object[] list)
		{
			if (level >= this.MinLogLevel)
			{
				ConsoleColor foregroundColor = Console.ForegroundColor;
				Console.ForegroundColor = (this.UseOverrideColor ? this._overrideColor : this.LoggingColors[level]);
				this.LoggingFunctions[level](format, list);
				Console.ForegroundColor = foregroundColor;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B048 File Offset: 0x00009248
		public void LogException(Exception exp)
		{
			this.LogException(exp, LoggingLevel.Error);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B054 File Offset: 0x00009254
		public void LogException(Exception exp, LoggingLevel level)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StackTrace stackTrace = new StackTrace(exp, true);
			if (stackTrace.FrameCount > 0)
			{
				StackTrace stackTrace2 = stackTrace;
				StackFrame frame = stackTrace2.GetFrame(stackTrace2.FrameCount - 1);
				if (frame != null)
				{
					string arg = string.Format("{0}({1},{2}):", frame.GetFileName(), frame.GetFileLineNumber(), frame.GetFileColumnNumber());
					stringBuilder.Append(string.Format("{0}{1}", arg, Environment.NewLine));
				}
			}
			stringBuilder.Append(string.Format("{0}: {1}{2}", this.LoggingMessage[level], "0x" + Marshal.GetHRForException(exp).ToString("X"), Environment.NewLine));
			stringBuilder.Append(string.Format("{0}:{1}", Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().ProcessName), Environment.NewLine));
			stringBuilder.Append(string.Format("EXCEPTION: {0}{1}", exp.ToString(), Environment.NewLine));
			this.Log(level, stringBuilder.ToString(), new object[0]);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B15E File Offset: 0x0000935E
		public void LogError(string format, params object[] list)
		{
			this.Log(LoggingLevel.Error, format, list);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B169 File Offset: 0x00009369
		public void LogWarning(string format, params object[] list)
		{
			this.Log(LoggingLevel.Warning, format, list);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B174 File Offset: 0x00009374
		public void LogInfo(string format, params object[] list)
		{
			this.Log(LoggingLevel.Info, format, list);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B17F File Offset: 0x0000937F
		public void LogDebug(string format, params object[] list)
		{
			this.Log(LoggingLevel.Debug, format, list);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B18A File Offset: 0x0000938A
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000B198 File Offset: 0x00009398
		public LogString ErrorLogger
		{
			get
			{
				return this.LoggingFunctions[LoggingLevel.Error];
			}
			set
			{
				this.SetLogFunction(LoggingLevel.Error, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000B1A2 File Offset: 0x000093A2
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public LogString WarningLogger
		{
			get
			{
				return this.LoggingFunctions[LoggingLevel.Warning];
			}
			set
			{
				this.SetLogFunction(LoggingLevel.Warning, value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B1BA File Offset: 0x000093BA
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000B1C8 File Offset: 0x000093C8
		public LogString InformationLogger
		{
			get
			{
				return this.LoggingFunctions[LoggingLevel.Info];
			}
			set
			{
				this.SetLogFunction(LoggingLevel.Info, value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000B1D2 File Offset: 0x000093D2
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public LogString DebugLogger
		{
			get
			{
				return this.LoggingFunctions[LoggingLevel.Debug];
			}
			set
			{
				this.SetLogFunction(LoggingLevel.Debug, value);
			}
		}

		// Token: 0x04000116 RID: 278
		private LoggingLevel MinLogLevel;

		// Token: 0x04000117 RID: 279
		private Dictionary<LoggingLevel, string> LoggingMessage = new Dictionary<LoggingLevel, string>();

		// Token: 0x04000118 RID: 280
		private Dictionary<LoggingLevel, LogString> LoggingFunctions = new Dictionary<LoggingLevel, LogString>();

		// Token: 0x04000119 RID: 281
		private Dictionary<LoggingLevel, ConsoleColor> LoggingColors = new Dictionary<LoggingLevel, ConsoleColor>();

		// Token: 0x0400011A RID: 282
		private ConsoleColor _overrideColor;
	}
}
