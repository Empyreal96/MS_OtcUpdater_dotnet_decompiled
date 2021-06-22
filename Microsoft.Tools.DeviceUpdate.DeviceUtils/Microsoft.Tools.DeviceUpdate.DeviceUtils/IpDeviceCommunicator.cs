using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Tools.Connectivity;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x0200000D RID: 13
	public class IpDeviceCommunicator : Disposable
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000EAFF File Offset: 0x0000CCFF
		public IpDeviceCommunicator(Guid id)
		{
			this.device = new RemoteDevice(id);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public void Connect()
		{
			try
			{
				this.device.UserName = "UpdateUser";
				this.device.Connect();
			}
			catch (AccessDeniedException)
			{
				this.device.UserName = "SshRecoveryUser";
				this.device.Connect();
			}
			try
			{
				this.device.CreateDirectory("C:\\Data\\ProgramData\\Update");
			}
			catch
			{
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000EB90 File Offset: 0x0000CD90
		protected override void DisposeManaged()
		{
			try
			{
				this.device.Disconnect();
			}
			catch
			{
			}
			base.DisposeManaged();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
		public string ExecuteCommand(IpDeviceCommunicator.IpDeviceCommand command, string args = null)
		{
			return command.Execute(this.device, args);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		public void PutFile(string remoteFilePath, string localFilePath)
		{
			try
			{
				this.device.PutFile(remoteFilePath, localFilePath, true);
			}
			catch (Exception innerException)
			{
				throw new DeviceException(string.Format("Unexpected failure when copying {0} to {1}", localFilePath, remoteFilePath), innerException);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public void GetFile(string remoteFilePath, string localFilePath)
		{
			try
			{
				this.device.GetFile(remoteFilePath, localFilePath, true);
			}
			catch (Exception innerException)
			{
				throw new DeviceException(string.Format("Unexpected failure when copying {0} to {1}", remoteFilePath, localFilePath), innerException);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000EC5C File Offset: 0x0000CE5C
		public void DeleteFile(string remoteFilePath)
		{
			try
			{
				try
				{
					this.device.DeleteFile(remoteFilePath);
				}
				catch (NotImplementedException)
				{
					this.device.RunCommand("cmd.exe", string.Format("/c del {0}", remoteFilePath));
				}
			}
			catch (Exception innerException)
			{
				throw new DeviceException(string.Format("Unexpected failure when deleting {0}", remoteFilePath), innerException);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public void CleanupUpdateFolder()
		{
			foreach (string text in this.device.GetFiles("C:\\Data\\ProgramData\\Update", "*.cab"))
			{
				this.device.DeleteFile(text);
			}
			foreach (string text2 in this.device.GetFiles("C:\\Data\\ProgramData\\Update", "*.xml"))
			{
				this.device.DeleteFile(text2);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		public bool IsIpDevice()
		{
			bool result;
			try
			{
				this.ExecuteCommand(IpDeviceCommunicator.DEVICE_UPDATE_PROPERTY_FIRMWARE_VERSION, null);
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public bool IsServicingSupported()
		{
			bool result;
			try
			{
				this.ExecuteCommand(IpDeviceCommunicator.APPLY_UPDATE_COMMAND_STATUS, null);
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040002C6 RID: 710
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_PROPERTY_FIRMWARE_VERSION = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("firmwareversion", "1");

		// Token: 0x040002C7 RID: 711
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_PROPERTY_MANUFACTURER = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("manufacturer", "2");

		// Token: 0x040002C8 RID: 712
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_PROPERTY_SERIAL_NUMBER = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("serialnumber", "3");

		// Token: 0x040002C9 RID: 713
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_PROPERTY_BUILD_BRANCH = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("buildbranch", "4");

		// Token: 0x040002CA RID: 714
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_PROPERTY_BUILD_NUMBER = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("buildnumber", "5");

		// Token: 0x040002CB RID: 715
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_PROPERTY_BUILD_TIMESTAMP = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("buildtimestamp", "6");

		// Token: 0x040002CC RID: 716
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_PROPERTY_OEM_DEVICE_NAME = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("oemdevicename", "7");

		// Token: 0x040002CD RID: 717
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_PROPERTY_UEFI_NAME = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("uefiname", "8");

		// Token: 0x040002CE RID: 718
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_PROPERTY_BUILD_REVISION = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("buildrevision", null);

		// Token: 0x040002CF RID: 719
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_COMMAND_REBOOT_TO_UEFI = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("reboottouefi", "4097");

		// Token: 0x040002D0 RID: 720
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_COMMAND_SET_TIME = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("settime", "4098");

		// Token: 0x040002D1 RID: 721
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_COMMAND_GET_INSTALLED_PACKAGES = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("getinstalledpackages", null);

		// Token: 0x040002D2 RID: 722
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_COMMAND_GET_BATTERY_LEVEL = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("getbatterylevel", null);

		// Token: 0x040002D3 RID: 723
		public static IpDeviceCommunicator.IpDeviceCommand DEVICE_UPDATE_COMMAND_GET_SECURITY_STATE = new IpDeviceCommunicator.IpDeviceDeviceUpdateUtilCommand("securitystate", null);

		// Token: 0x040002D4 RID: 724
		public static IpDeviceCommunicator.IpDeviceCommand APPLY_UPDATE_COMMAND_STAGE = new IpDeviceCommunicator.IpDeviceApplyUpdateCommand("-stage", true, false, null);

		// Token: 0x040002D5 RID: 725
		public static IpDeviceCommunicator.IpDeviceCommand APPLY_UPDATE_COMMAND_COMMIT = new IpDeviceCommunicator.IpDeviceApplyUpdateCommand("-commit -timeout 1", true, false, null);

		// Token: 0x040002D6 RID: 726
		public static IpDeviceCommunicator.IpDeviceCommand APPLY_UPDATE_COMMAND_CLEAR = new IpDeviceCommunicator.IpDeviceApplyUpdateCommand("-clear", true, false, null);

		// Token: 0x040002D7 RID: 727
		public static IpDeviceCommunicator.IpDeviceCommand APPLY_UPDATE_COMMAND_STATUS = new IpDeviceCommunicator.IpDeviceApplyUpdateCommand("-status", false, true, new int?(5));

		// Token: 0x040002D8 RID: 728
		public static IpDeviceCommunicator.IpDeviceCommand APPLY_UPDATE_COMMAND_COLLECT_LOGS = new IpDeviceCommunicator.IpDeviceApplyUpdateCommand("-collectlogs", true, false, null);

		// Token: 0x040002D9 RID: 729
		public static IpDeviceCommunicator.IpDeviceCommand APPLY_UPDATE_COMMAND_GET_REQUIRED_PKGS = new IpDeviceCommunicator.IpDeviceApplyUpdateCommand("-getrequiredpkgs", true, false, null);

		// Token: 0x040002DA RID: 730
		public static IpDeviceCommunicator.IpDeviceCommand APPLY_UPDATE_COMMAND_INSTALL_WITH_AL = new IpDeviceCommunicator.IpDeviceApplyUpdateCommand("-timeout 1 -installwithAl", true, false, null);

		// Token: 0x040002DB RID: 731
		public const string APPLY_UPDATE_DIRECTORY = "C:\\Data\\ProgramData\\Update";

		// Token: 0x040002DC RID: 732
		public const string APPLY_UPDATE_LOG_FILE = "C:\\Data\\ProgramData\\Update\\ApplyUpdate.log";

		// Token: 0x040002DD RID: 733
		public const string USO_LOG_FILE = "C:\\Data\\ProgramData\\USOShared\\UsoLogs.dudiag";

		// Token: 0x040002DE RID: 734
		public const string ACTION_LIST_FILE = "C:\\Data\\ProgramData\\Update\\actionlist.xml";

		// Token: 0x040002DF RID: 735
		public const string UPDATE_AGENT_FILE = "C:\\Data\\ProgramData\\Update\\UpdateAgent.cab";

		// Token: 0x040002E0 RID: 736
		private RemoteDevice device;

		// Token: 0x02000035 RID: 53
		public abstract class IpDeviceCommand
		{
			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x00012A2C File Offset: 0x00010C2C
			// (set) Token: 0x060001B9 RID: 441 RVA: 0x00012A34 File Offset: 0x00010C34
			private protected string Command { protected get; private set; }

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060001BA RID: 442 RVA: 0x00012A3D File Offset: 0x00010C3D
			// (set) Token: 0x060001BB RID: 443 RVA: 0x00012A45 File Offset: 0x00010C45
			private protected string AlternateCommand { protected get; private set; }

			// Token: 0x060001BC RID: 444 RVA: 0x00012A4E File Offset: 0x00010C4E
			public IpDeviceCommand(string command, string alternateCommand, string args)
			{
				this.Command = command;
				this.AlternateCommand = alternateCommand;
				this.args = args;
			}

			// Token: 0x060001BD RID: 445 RVA: 0x00012A6B File Offset: 0x00010C6B
			protected string Args(string additionalArgs = null)
			{
				if (string.IsNullOrEmpty(additionalArgs))
				{
					return this.args;
				}
				return string.Format("{0} {1}", this.args, additionalArgs);
			}

			// Token: 0x060001BE RID: 446 RVA: 0x00012A8D File Offset: 0x00010C8D
			protected string GetFullCommandString(string additionalArgs = null)
			{
				return string.Format("{0} {1}", this.Command, this.Args(additionalArgs));
			}

			// Token: 0x060001BF RID: 447
			public abstract string Execute(RemoteDevice device, string additionalArgs);

			// Token: 0x04000370 RID: 880
			private string args;
		}

		// Token: 0x02000036 RID: 54
		public class IpDeviceDeviceUpdateUtilCommand : IpDeviceCommunicator.IpDeviceCommand
		{
			// Token: 0x060001C0 RID: 448 RVA: 0x00012AA6 File Offset: 0x00010CA6
			public IpDeviceDeviceUpdateUtilCommand(string args, string secondaryArgs = null) : base("C:\\Windows\\System32\\DeviceUpdateUtil.exe", "DeviceUpdateUtil.exe", args)
			{
				this.secondaryArgs = secondaryArgs;
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x00012AC0 File Offset: 0x00010CC0
			private bool HasSecondaryArgs()
			{
				return this.secondaryArgs != null;
			}

			// Token: 0x060001C2 RID: 450 RVA: 0x00012ACB File Offset: 0x00010CCB
			private string SecondaryArgs(string additionalArgs = null)
			{
				if (string.IsNullOrEmpty(additionalArgs))
				{
					return this.secondaryArgs;
				}
				return string.Format("{0} {1}", this.secondaryArgs, additionalArgs);
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x00012AF0 File Offset: 0x00010CF0
			public override string Execute(RemoteDevice device, string additionalArgs)
			{
				string result;
				try
				{
					result = this.Execute(device, additionalArgs, false);
				}
				catch (DeviceException ex)
				{
					if (!(ex.InnerException is InvalidOperationException))
					{
						throw;
					}
					result = this.Execute(device, additionalArgs, true);
				}
				return result;
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x00012B34 File Offset: 0x00010D34
			private string Execute(RemoteDevice device, string additionalArgs, bool useSecondaryArgs)
			{
				string fullCommandString = base.GetFullCommandString(additionalArgs);
				string text = null;
				try
				{
					try
					{
						text = device.RunCommand(base.Command, useSecondaryArgs ? this.SecondaryArgs(additionalArgs) : base.Args(additionalArgs));
					}
					catch (OperationFailedException)
					{
						text = device.RunCommand(base.AlternateCommand, useSecondaryArgs ? this.SecondaryArgs(additionalArgs) : base.Args(additionalArgs));
					}
				}
				catch (Exception innerException)
				{
					throw new DeviceException(string.Format("Unexpected failure for command \"{0}\"", fullCommandString), innerException);
				}
				if (!text.Contains(';'))
				{
					throw new DeviceException(string.Format("Unexpected device response for command \"{0}\": {1}", fullCommandString, text));
				}
				int num;
				try
				{
					string text2 = text;
					num = int.Parse(text2.Substring(text2.LastIndexOf(';') + 1));
				}
				catch (Exception innerException2)
				{
					throw new DeviceException(string.Format("Unexpected status for command \"{0}\"\n{1}", fullCommandString), innerException2);
				}
				if (num == 4317)
				{
					Exception ex = new InvalidOperationException(string.Format("Command \"{0}\" failed with status {1}", fullCommandString, num));
					throw new DeviceException(ex.Message, ex);
				}
				if (num == 87)
				{
					Exception ex2 = new ArgumentException(string.Format("Command \"{0}\" failed with status {1}", fullCommandString, num));
					throw new DeviceException(ex2.Message, ex2);
				}
				if (num != 0)
				{
					throw new DeviceException(string.Format("Command \"{0}\" failed with status {1}", fullCommandString, num));
				}
				return text.Substring(0, text.LastIndexOf(';'));
			}

			// Token: 0x04000371 RID: 881
			private const string DEVICE_UPDATE_UTIL_PATH = "C:\\Windows\\System32\\DeviceUpdateUtil.exe";

			// Token: 0x04000372 RID: 882
			private const string DEVICE_UPDATE_UTIL_ALTERNATE_PATH = "DeviceUpdateUtil.exe";

			// Token: 0x04000373 RID: 883
			private const int DEVICE_UPDATE_STATUS_SUCCESS = 0;

			// Token: 0x04000374 RID: 884
			private const int DEVICE_UPDATE_STATUS_INVALID_PARAMETER = 87;

			// Token: 0x04000375 RID: 885
			private const int DEVICE_UPDATE_STATUS_INVALID_OPERATION = 4317;

			// Token: 0x04000376 RID: 886
			private string secondaryArgs;
		}

		// Token: 0x02000037 RID: 55
		public class IpDeviceApplyUpdateCommand : IpDeviceCommunicator.IpDeviceCommand
		{
			// Token: 0x060001C5 RID: 453 RVA: 0x00012C9C File Offset: 0x00010E9C
			public IpDeviceApplyUpdateCommand(string args, bool log = true, bool ignoreFailure = false, int? timeoutSeconds = null) : base("C:\\Windows\\System32\\ApplyUpdate.exe", "ApplyUpdate.exe", log ? string.Format("-log {0} {1}", "C:\\Data\\ProgramData\\Update\\ApplyUpdate.log", args) : args)
			{
				this.ignoreFailure = ignoreFailure;
				this.timeoutSeconds = timeoutSeconds;
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x00012CD4 File Offset: 0x00010ED4
			public override string Execute(RemoteDevice device, string additionalArgs)
			{
				string fullCommandString = base.GetFullCommandString(additionalArgs);
				string text = null;
				int num = 0;
				try
				{
					try
					{
						RemoteCommand remoteCommand = device.Command(base.Command, base.Args(additionalArgs));
						remoteCommand.CaptureOutput = true;
						if (this.timeoutSeconds != null)
						{
							remoteCommand.Timeout = TimeSpan.FromSeconds((double)this.timeoutSeconds.Value);
						}
						num = remoteCommand.Execute();
						text = remoteCommand.Output;
					}
					catch (OperationFailedException)
					{
						RemoteCommand remoteCommand2 = device.Command(base.AlternateCommand, base.Args(additionalArgs));
						remoteCommand2.CaptureOutput = true;
						if (this.timeoutSeconds != null)
						{
							remoteCommand2.Timeout = TimeSpan.FromSeconds((double)this.timeoutSeconds.Value);
						}
						num = remoteCommand2.Execute();
						text = remoteCommand2.Output;
					}
				}
				catch (Exception innerException)
				{
					throw new DeviceException(string.Format("Unexpected failure for command \"{0}\"", fullCommandString), innerException);
				}
				string arg;
				string text2;
				string text3;
				if (!this.ignoreFailure && num != 0 && IpDeviceCommunicator.IpDeviceApplyUpdateCommand.ParseResponse(text, out arg, out text2, out text3))
				{
					throw new DeviceException(string.Format("Command \"{0}\" failed\n{1}", fullCommandString, arg));
				}
				return text;
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x00012DF4 File Offset: 0x00010FF4
			public static bool ParseResponse(string response, out string errorLine, out string updateState, out string updateProgress)
			{
				string[] array = response.Split(new string[]
				{
					"\r\n",
					"\n"
				}, StringSplitOptions.RemoveEmptyEntries);
				string text = "Unknown failure";
				bool flag = false;
				errorLine = "";
				updateState = "";
				updateProgress = "";
				foreach (string text2 in array)
				{
					if (text2.StartsWith("INFO: UpdateState"))
					{
						updateState = text2.Split(new string[]
						{
							"INFO: "
						}, StringSplitOptions.RemoveEmptyEntries)[0];
					}
					else if (text2.StartsWith("INFO: ProgressStateInstall"))
					{
						Match match = new Regex("\\d+", RegexOptions.IgnoreCase).Match(text2);
						if (match.Success)
						{
							updateProgress = match.Value;
						}
					}
					else if (text2.StartsWith("ERROR:"))
					{
						flag = true;
						text = text2.Split(new string[]
						{
							"ERROR: "
						}, StringSplitOptions.RemoveEmptyEntries)[0];
					}
				}
				if (flag)
				{
					errorLine = text;
				}
				return flag;
			}

			// Token: 0x04000377 RID: 887
			private const string APPLY_UPDATE_PATH = "C:\\Windows\\System32\\ApplyUpdate.exe";

			// Token: 0x04000378 RID: 888
			private const string APPLY_UPDATE_ALTERNATE_PATH = "ApplyUpdate.exe";

			// Token: 0x04000379 RID: 889
			private const int APPLY_UPDATE_STATUS_SUCCESS = 0;

			// Token: 0x0400037A RID: 890
			private bool ignoreFailure;

			// Token: 0x0400037B RID: 891
			private int? timeoutSeconds;
		}
	}
}
