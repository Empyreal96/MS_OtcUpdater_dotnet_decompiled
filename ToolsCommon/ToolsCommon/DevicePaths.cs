using System;
using System.IO;

namespace Microsoft.WindowsPhone.ImageUpdate.Tools.Common
{
	// Token: 0x02000057 RID: 87
	public class DevicePaths
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000B4CB File Offset: 0x000096CB
		public static string ImageUpdatePath
		{
			get
			{
				return DevicePaths._imageUpdatePath;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000B4D2 File Offset: 0x000096D2
		public static string DeviceLayoutFileName
		{
			get
			{
				return DevicePaths._deviceLayoutFileName;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000B4D9 File Offset: 0x000096D9
		public static string DeviceLayoutFilePath
		{
			get
			{
				return Path.Combine(DevicePaths.ImageUpdatePath, DevicePaths.DeviceLayoutFileName);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000B4EA File Offset: 0x000096EA
		public static string OemDevicePlatformFileName
		{
			get
			{
				return DevicePaths._oemDevicePlatformFileName;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000B4F1 File Offset: 0x000096F1
		public static string OemDevicePlatformFilePath
		{
			get
			{
				return Path.Combine(DevicePaths.ImageUpdatePath, DevicePaths.OemDevicePlatformFileName);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000B502 File Offset: 0x00009702
		public static string UpdateOutputFile
		{
			get
			{
				return DevicePaths._updateOutputFile;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000B509 File Offset: 0x00009709
		public static string UpdateOutputFilePath
		{
			get
			{
				return Path.Combine(DevicePaths._updateFilesPath, DevicePaths._updateOutputFile);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B51A File Offset: 0x0000971A
		public static string UpdateHistoryFile
		{
			get
			{
				return DevicePaths._updateHistoryFile;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000B521 File Offset: 0x00009721
		public static string UpdateHistoryFilePath
		{
			get
			{
				return Path.Combine(DevicePaths._imageUpdatePath, DevicePaths._updateHistoryFile);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000B532 File Offset: 0x00009732
		public static string UpdateOSWIMName
		{
			get
			{
				return DevicePaths._updateOSWIMName;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000B539 File Offset: 0x00009739
		public static string UpdateOSWIMFilePath
		{
			get
			{
				return Path.Combine(DevicePaths._UpdateOSPath, DevicePaths.UpdateOSWIMName);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000B54A File Offset: 0x0000974A
		public static string MMOSWIMName
		{
			get
			{
				return DevicePaths._mmosWIMName;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000B551 File Offset: 0x00009751
		public static string MMOSWIMFilePath
		{
			get
			{
				return DevicePaths.MMOSWIMName;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000B558 File Offset: 0x00009758
		public static string RegistryHivePath
		{
			get
			{
				return DevicePaths._registryHivePath;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000B55F File Offset: 0x0000975F
		public static string GetBCDHivePath(bool isUefiBoot)
		{
			if (!isUefiBoot)
			{
				return DevicePaths._BiosBCDHivePath;
			}
			return DevicePaths._UefiBCDHivePath;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000B56F File Offset: 0x0000976F
		public static string GetRegistryHiveFilePath(SystemRegistryHiveFiles hiveType)
		{
			return DevicePaths.GetRegistryHiveFilePath(hiveType, true);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000B578 File Offset: 0x00009778
		public static string GetRegistryHiveFilePath(SystemRegistryHiveFiles hiveType, bool isUefiBoot)
		{
			string result = "";
			switch (hiveType)
			{
			case SystemRegistryHiveFiles.SYSTEM:
				result = Path.Combine(DevicePaths.RegistryHivePath, "SYSTEM");
				break;
			case SystemRegistryHiveFiles.SOFTWARE:
				result = Path.Combine(DevicePaths.RegistryHivePath, "SOFTWARE");
				break;
			case SystemRegistryHiveFiles.DEFAULT:
				result = Path.Combine(DevicePaths.RegistryHivePath, "DEFAULT");
				break;
			case SystemRegistryHiveFiles.DRIVERS:
				result = Path.Combine(DevicePaths.RegistryHivePath, "DRIVERS");
				break;
			case SystemRegistryHiveFiles.SAM:
				result = Path.Combine(DevicePaths.RegistryHivePath, "SAM");
				break;
			case SystemRegistryHiveFiles.SECURITY:
				result = Path.Combine(DevicePaths.RegistryHivePath, "SECURITY");
				break;
			case SystemRegistryHiveFiles.BCD:
				result = Path.Combine(DevicePaths.GetBCDHivePath(isUefiBoot), "BCD");
				break;
			}
			return result;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000B62D File Offset: 0x0000982D
		public static string DeviceLayoutSchema
		{
			get
			{
				return "DeviceLayout.xsd";
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B634 File Offset: 0x00009834
		public static string DeviceLayoutSchema2
		{
			get
			{
				return "DeviceLayoutv2.xsd";
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000B63B File Offset: 0x0000983B
		public static string UpdateOSInputSchema
		{
			get
			{
				return "UpdateOSInput.xsd";
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000B642 File Offset: 0x00009842
		public static string OEMInputSchema
		{
			get
			{
				return "OEMInput.xsd";
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000B649 File Offset: 0x00009849
		public static string FeatureManifestSchema
		{
			get
			{
				return "FeatureManifest.xsd";
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B650 File Offset: 0x00009850
		public static string UpdateOSOutputSchema
		{
			get
			{
				return "UpdateOSOutput.xsd";
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000B657 File Offset: 0x00009857
		public static string UpdateHistorySchema
		{
			get
			{
				return "UpdateHistory.xsd";
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000B65E File Offset: 0x0000985E
		public static string OEMDevicePlatformSchema
		{
			get
			{
				return "OEMDevicePlatform.xsd";
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000B665 File Offset: 0x00009865
		public static string MSFMPath
		{
			get
			{
				return Path.Combine(DevicePaths.ImageUpdatePath, DevicePaths._FMFilesDirectory, "Microsoft");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000B67B File Offset: 0x0000987B
		public static string MSFMPathOld
		{
			get
			{
				return DevicePaths.ImageUpdatePath;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000B682 File Offset: 0x00009882
		public static string OEMFMPath
		{
			get
			{
				return Path.Combine(DevicePaths.ImageUpdatePath, DevicePaths._FMFilesDirectory, "OEM");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000B698 File Offset: 0x00009898
		public static string OEMInputPath
		{
			get
			{
				return Path.Combine(DevicePaths.ImageUpdatePath, DevicePaths._OEMInputPath);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000B6A9 File Offset: 0x000098A9
		public static string OEMInputFile
		{
			get
			{
				return Path.Combine(DevicePaths.OEMInputPath, DevicePaths._OEMInputFile);
			}
		}

		// Token: 0x0400011D RID: 285
		private static string _imageUpdatePath = "Windows\\ImageUpdate";

		// Token: 0x0400011E RID: 286
		private static string _updateFilesPath = "SharedData\\DuShared";

		// Token: 0x0400011F RID: 287
		private static string _registryHivePath = "Windows\\System32\\Config";

		// Token: 0x04000120 RID: 288
		private static string _BiosBCDHivePath = "boot";

		// Token: 0x04000121 RID: 289
		private static string _UefiBCDHivePath = "efi\\Microsoft\\boot";

		// Token: 0x04000122 RID: 290
		private static string _dsmPath = DevicePaths._imageUpdatePath;

		// Token: 0x04000123 RID: 291
		private static string _UpdateOSPath = "PROGRAMS\\UpdateOS\\";

		// Token: 0x04000124 RID: 292
		private static string _FMFilesDirectory = "FeatureManifest";

		// Token: 0x04000125 RID: 293
		private static string _OEMInputPath = "OEMInput";

		// Token: 0x04000126 RID: 294
		private static string _OEMInputFile = "OEMInput.xml";

		// Token: 0x04000127 RID: 295
		private static string _deviceLayoutFileName = "DeviceLayout.xml";

		// Token: 0x04000128 RID: 296
		private static string _oemDevicePlatformFileName = "OEMDevicePlatform.xml";

		// Token: 0x04000129 RID: 297
		private static string _updateOutputFile = "UpdateOutput.xml";

		// Token: 0x0400012A RID: 298
		private static string _updateHistoryFile = "UpdateHistory.xml";

		// Token: 0x0400012B RID: 299
		private static string _updateOSWIMName = "UpdateOS.wim";

		// Token: 0x0400012C RID: 300
		private static string _mmosWIMName = "MMOS.wim";

		// Token: 0x0400012D RID: 301
		public const string MAINOS_PARTITION_NAME = "MainOS";

		// Token: 0x0400012E RID: 302
		public const string MMOS_PARTITION_NAME = "MMOS";
	}
}
