using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.WindowsPhone.ImageUpdate.Tools;
using Microsoft.WindowsPhone.ImageUpdate.Tools.Common;
using PortableDeviceApiLib;
using PortableDeviceConstants;
using PortableDeviceTypesLib;

namespace Microsoft.Tools.DeviceUpdate.DeviceUtils
{
	// Token: 0x0200001A RID: 26
	public class WpdDevice : Disposable, IWpdDevice, IUpdateableDevice, IDevicePropertyCollection, IDisposable
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000124 RID: 292 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		// (remove) Token: 0x06000125 RID: 293 RVA: 0x0000FE24 File Offset: 0x0000E024
		public event MessageHandler NormalMessageEvent;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000126 RID: 294 RVA: 0x0000FE5C File Offset: 0x0000E05C
		// (remove) Token: 0x06000127 RID: 295 RVA: 0x0000FE94 File Offset: 0x0000E094
		public event MessageHandler WarningMessageEvent;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000128 RID: 296 RVA: 0x0000FECC File Offset: 0x0000E0CC
		// (remove) Token: 0x06000129 RID: 297 RVA: 0x0000FF04 File Offset: 0x0000E104
		public event MessageHandler ProgressEvent;

		// Token: 0x0600012A RID: 298 RVA: 0x0000FF3C File Offset: 0x0000E13C
		static WpdDevice()
		{
			WpdDevice.WPD_MTPAUTHDEVICESERVICE_ISLOCKED.fmtid = WpdDevice.AuthMtpDevicePropertyGuid;
			WpdDevice.WPD_MTPAUTHDEVICESERVICE_ISLOCKED.pid = 1U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUENGINESTATE.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUENGINESTATE.pid = 2U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DURESULT.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DURESULT.pid = 3U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBRANCHNAME.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBRANCHNAME.pid = 4U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBUILDER.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBUILDER.pid = 5U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUCORESYSBUILDNUMBER.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUCORESYSBUILDNUMBER.pid = 6U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUWINDOWSPHONEBUILDNUMBER.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUWINDOWSPHONEBUILDNUMBER.pid = 7U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBUILDTIMESTAMP.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBUILDTIMESTAMP.pid = 8U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DULANGUAGES.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DULANGUAGES.pid = 9U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DURESOLUTION.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DURESOLUTION.pid = 10U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSTAGINGPERCENTAGE.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSTAGINGPERCENTAGE.pid = 11U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUOSVERSION.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUOSVERSION.pid = 12U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUMOID.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUMOID.pid = 13U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUOEM.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUOEM.pid = 14U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUOEMDEVICENAME.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUOEMDEVICENAME.pid = 15U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUFIRMWAREVERSION.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUFIRMWAREVERSION.pid = 16U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSOCVERSION.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSOCVERSION.pid = 17U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DURADIOSWVERSION.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DURADIOSWVERSION.pid = 18U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DURADIOHWVERSION.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DURADIOHWVERSION.pid = 19U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBOOTLOADERVERSION.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBOOTLOADERVERSION.pid = 20U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUMUILANGUAGEIDS.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUMUILANGUAGEIDS.pid = 21U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBOOTUILANGUAGEIDS.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBOOTUILANGUAGEIDS.pid = 22U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBOOTLOCALELANGUAGEIDS.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUBOOTLOCALELANGUAGEIDS.pid = 23U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSPEECHLANGUAGEIDS.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSPEECHLANGUAGEIDS.pid = 24U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUKEYBOARDLANGUAGEIDS.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUKEYBOARDLANGUAGEIDS.pid = 25U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSUPPORTEDRESOLUTIONS.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSUPPORTEDRESOLUTIONS.pid = 26U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUFEEDBACKID.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUFEEDBACKID.pid = 28U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUPLATFORMID.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUPLATFORMID.pid = 29U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUISPRODUCTIONCONFIGURATION.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUISPRODUCTIONCONFIGURATION.pid = 30U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUIMAGETARGETINGTYPE.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUIMAGETARGETINGTYPE.pid = 31U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSMBIOSUUID.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSMBIOSUUID.pid = 32U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUDEVICEUPDATERESULT.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUDEVICEUPDATERESULT.pid = 33U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSHELLSTARTREADY.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSHELLSTARTREADY.pid = 34U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSERIALNUMBER.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSERIALNUMBER.pid = 35U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSHELLAPIREADY.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSHELLAPIREADY.pid = 36U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUIMEI.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUIMEI.pid = 37U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUTOTALSTORAGE.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUTOTALSTORAGE.pid = 38U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUTOTALRAM.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUTOTALRAM.pid = 39U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUUPDATEAGENTERROR.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUUPDATEAGENTERROR.pid = 40U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSENDIUPACKAGEOPTIONS.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUSENDIUPACKAGEOPTIONS.pid = 41U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUCTACPROPERTIES.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUCTACPROPERTIES.pid = 42U;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUPRODUCTPROPERTIES.fmtid = WpdDevice.DuMtpDeviceServiceGuid;
			WpdDevice.WPD_MTPDUDEVICESERVICE_DUPRODUCTPROPERTIES.pid = 43U;
			WpdDevice.WPD_STATUSSERVICE_BATTERYLIFE.fmtid = WpdDevice.StatusServiceGuid;
			WpdDevice.WPD_STATUSSERVICE_BATTERYLIFE.pid = 10U;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000104A8 File Offset: 0x0000E6A8
		public WpdDevice(IPortableDeviceManager wpdManager, IPortableDevice portableDevice, string deviceId)
		{
			IPortableDeviceContent portableDeviceContent = null;
			IPortableDeviceProperties portableDeviceProperties = null;
			PortableDeviceApiLib.IPortableDeviceValues portableDeviceValues = null;
			this.wpdManager = wpdManager;
			this.portableDevice = portableDevice;
			this.deviceId = deviceId;
			portableDevice.Content(out portableDeviceContent);
			portableDeviceContent.Properties(out portableDeviceProperties);
			portableDeviceProperties.GetValues("DEVICE", null, out portableDeviceValues);
			portableDeviceValues.GetStringValue(ref PortableDevicePKeys.WPD_DEVICE_MODEL, out this.model);
			portableDeviceValues.GetStringValue(ref PortableDevicePKeys.WPD_DEVICE_MANUFACTURER, out this.manufacturer);
			portableDeviceValues.GetStringValue(ref PortableDevicePKeys.WPD_DEVICE_SERIAL_NUMBER, out this.serialNumber);
			portableDeviceValues.GetStringValue(ref PortableDevicePKeys.WPD_DEVICE_FRIENDLY_NAME, out this.friendlyName);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00010538 File Offset: 0x0000E738
		public string DeviceId
		{
			get
			{
				return this.deviceId;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00010540 File Offset: 0x0000E740
		public string Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00010548 File Offset: 0x0000E748
		public string FriendlyName
		{
			get
			{
				return this.friendlyName;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00010550 File Offset: 0x0000E750
		public string Manufacturer
		{
			get
			{
				return this.manufacturer;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00010558 File Offset: 0x0000E758
		public string SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00010560 File Offset: 0x0000E760
		public bool IsLocked
		{
			get
			{
				this.LoadAuthMtpService();
				bool flag = WpdUtils.GetServicePropertyByteValue(this.authMtpDeviceService, WpdDevice.WPD_MTPAUTHDEVICESERVICE_ISLOCKED) > 0;
				if (!flag)
				{
					this.isMtpSessionUnlocked = true;
				}
				return flag;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00010585 File Offset: 0x0000E785
		public bool IsMtpSessionUnlocked
		{
			get
			{
				if (!this.isMtpSessionUnlocked)
				{
					this.isMtpSessionUnlocked = !this.IsLocked;
				}
				return this.isMtpSessionUnlocked;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000105A4 File Offset: 0x0000E7A4
		public string Branch
		{
			get
			{
				this.LoadDuMtpService();
				return this.branch;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000105B2 File Offset: 0x0000E7B2
		public string WindowsPhoneBuildNumber
		{
			get
			{
				this.LoadDuMtpService();
				return this.windowsPhoneBuildNumber;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public string CoreSysBuildNumber
		{
			get
			{
				this.LoadDuMtpService();
				return this.coreSysBuildNumber;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000105CE File Offset: 0x0000E7CE
		public string BuildTimeStamp
		{
			get
			{
				this.LoadDuMtpService();
				return this.buildTimeStamp;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000105DC File Offset: 0x0000E7DC
		public string ImageTargetingType
		{
			get
			{
				this.LoadDuMtpService();
				return this.imageTargetingType;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000105EA File Offset: 0x0000E7EA
		public string FeedbackId
		{
			get
			{
				this.LoadDuMtpService();
				return this.feedbackId;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000105F8 File Offset: 0x0000E7F8
		public string OsVersion
		{
			get
			{
				this.LoadDuMtpService();
				return this.osVersion;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00010608 File Offset: 0x0000E808
		public string FirmwareVersion
		{
			get
			{
				this.LoadDuMtpService();
				string result;
				try
				{
					result = this.firmwareVersion;
				}
				catch
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00010640 File Offset: 0x0000E840
		public string MoId
		{
			get
			{
				this.LoadDuMtpService();
				string result;
				try
				{
					result = this.moId;
				}
				catch
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00010678 File Offset: 0x0000E878
		public string BuildString
		{
			get
			{
				if (this.osEdition == null || "" == this.osEdition || this.revisionNumber == null || "" == this.revisionNumber || "Mobile" == this.osEdition)
				{
					return string.Format("{0}.{1}.{2}.{3}", new object[]
					{
						this.Branch,
						this.CoreSysBuildNumber,
						this.WindowsPhoneBuildNumber,
						this.BuildTimeStamp
					});
				}
				return string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					this.CoreSysBuildNumber,
					this.revisionNumber,
					this.Branch,
					this.BuildTimeStamp
				});
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00010734 File Offset: 0x0000E934
		public string Oem
		{
			get
			{
				this.LoadDuMtpService();
				return this.oem;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00010744 File Offset: 0x0000E944
		public string OemDeviceName
		{
			get
			{
				this.LoadDuMtpService();
				string result;
				try
				{
					result = this.oemDeviceName;
				}
				catch
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0001077C File Offset: 0x0000E97C
		public string Resolution
		{
			get
			{
				if (string.IsNullOrEmpty(this.resolution))
				{
					this.LoadDuMtpService();
					this.resolution = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DURESOLUTION);
				}
				return this.resolution;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000107AD File Offset: 0x0000E9AD
		public string UefiName
		{
			get
			{
				this.LoadDuMtpService();
				return this.uefiName;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000107BC File Offset: 0x0000E9BC
		public string DuEngineState
		{
			get
			{
				this.LoadDuMtpService();
				return WpdUtils.GetServicePropertyByteValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUENGINESTATE).ToString();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000107E8 File Offset: 0x0000E9E8
		public string DuUpdateAgentError
		{
			get
			{
				this.LoadDuMtpService();
				string result;
				try
				{
					result = "0X" + WpdUtils.GetServicePropertyUnsignedIntegerValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUUPDATEAGENTERROR).ToString("X");
				}
				catch
				{
					result = "0X80004005";
				}
				return result;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00010840 File Offset: 0x0000EA40
		public string DuSendIuPackageOptions
		{
			get
			{
				this.LoadDuMtpService();
				string result;
				try
				{
					result = "0X" + WpdUtils.GetServicePropertyUnsignedIntegerValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUSENDIUPACKAGEOPTIONS).ToString("X");
				}
				catch
				{
					result = "0X0";
				}
				return result;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00010898 File Offset: 0x0000EA98
		public string DuCTAC
		{
			get
			{
				this.LoadDuMtpService();
				return this.ctac;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public string DuProductNames
		{
			get
			{
				this.LoadDuMtpService();
				return this.productNames;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000108B4 File Offset: 0x0000EAB4
		public string DuResult
		{
			get
			{
				this.LoadDuMtpService();
				return "0X" + WpdUtils.GetServicePropertyUnsignedIntegerValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DURESULT).ToString("X");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000108EE File Offset: 0x0000EAEE
		public Guid DeviceUniqueId
		{
			get
			{
				this.LoadDuMtpService();
				return this.deviceUniqueId;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000108FC File Offset: 0x0000EAFC
		public string UniqueID
		{
			get
			{
				return this.DeviceUniqueId.ToString().Replace("-", "");
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0001092C File Offset: 0x0000EB2C
		public string IMEI
		{
			get
			{
				this.LoadDuMtpService();
				return this.imei;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0001093C File Offset: 0x0000EB3C
		public string DuDeviceUpdateResult
		{
			get
			{
				this.LoadDuMtpService();
				string result;
				try
				{
					result = "0X" + WpdUtils.GetServicePropertyUnsignedIntegerValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUDEVICEUPDATERESULT).ToString("X");
				}
				catch
				{
					result = "0X1";
				}
				return result;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00010994 File Offset: 0x0000EB94
		public string DuShellStartReady
		{
			get
			{
				this.LoadDuMtpService();
				string result;
				try
				{
					result = "0X" + WpdUtils.GetServicePropertyUnsignedIntegerValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUSHELLSTARTREADY).ToString("X");
				}
				catch
				{
					result = "0X0";
				}
				return result;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000109EC File Offset: 0x0000EBEC
		public string WPSerialNumber
		{
			get
			{
				this.LoadDuMtpService();
				return this.wpSerialNumber;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000109FC File Offset: 0x0000EBFC
		public string DuShellApiReady
		{
			get
			{
				this.LoadDuMtpService();
				string result;
				try
				{
					result = "0X" + WpdUtils.GetServicePropertyUnsignedIntegerValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUSHELLAPIREADY).ToString("X");
				}
				catch
				{
					result = "0X0";
				}
				return result;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00010A54 File Offset: 0x0000EC54
		public string TotalStorage
		{
			get
			{
				this.LoadDuMtpService();
				return this.totalStorage;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00010A62 File Offset: 0x0000EC62
		public string TotalRAM
		{
			get
			{
				this.LoadDuMtpService();
				return this.totalRAM;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00010A70 File Offset: 0x0000EC70
		public string BatteryLife
		{
			get
			{
				this.LoadStatusMtpService();
				string result;
				try
				{
					result = WpdUtils.GetServicePropertyUnsignedIntegerValue(this.statusMtpDeviceService, WpdDevice.WPD_STATUSSERVICE_BATTERYLIFE).ToString();
				}
				catch
				{
					result = "100";
				}
				return result;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00010AB8 File Offset: 0x0000ECB8
		public string OSEdition
		{
			get
			{
				this.LoadDuMtpService();
				return this.osEdition;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00010AC6 File Offset: 0x0000ECC6
		public InstalledPackageInfo[] InstalledPackages
		{
			get
			{
				if (this.installedPackages == null)
				{
					this.installedPackages = this.GetInstalledPackages();
				}
				return this.installedPackages;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		public virtual string GetProperty(string name)
		{
			return PropertyDeviceCollection.GetPropertyString(this, name);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00010AE2 File Offset: 0x0000ECE2
		public void RebootToUefi()
		{
			WpdDevice.RebootToUefi(this.portableDevice);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		public static void RebootToUefi(IPortableDevice portableDevice)
		{
			uint num = 0U;
			uint num2 = 0U;
			WpdUtils.ExecuteMtpOpcode(portableDevice, 37889U, out num, out num2);
			if (num != 0U)
			{
				if (2147942431U != num)
				{
					throw new DeviceException(string.Format("RebootToUefi failed, hresult: {0}", num));
				}
			}
			else if (8193U != num2)
			{
				throw new DeviceException(string.Format("RebootToUefi failed, response code: {0}", num2));
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00010B4F File Offset: 0x0000ED4F
		public void RebootToTarget(uint target)
		{
			WpdDevice.RebootToTarget(this.portableDevice, target);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00010B60 File Offset: 0x0000ED60
		public static void RebootToTarget(IPortableDevice portableDevice, uint target)
		{
			uint num = 0U;
			uint num2 = 0U;
			PortableDeviceApiLib.IPortableDevicePropVariantCollection mtpParameters = (PortableDeviceApiLib.IPortableDevicePropVariantCollection)((PortableDevicePropVariantCollection)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("08A99E2F-6D6D-4B80-AF5A-BAF2BCBE4CB9"))));
			WpdUtils.AddUnsignedIntegerValue(mtpParameters, target);
			WpdUtils.ExecuteMtpOpcode(portableDevice, 37893U, mtpParameters, out num, out num2);
			if (num != 0U)
			{
				if (2147942431U != num)
				{
					throw new DeviceException(string.Format("Reboot failed, hresult: {0}", num));
				}
			}
			else if (8193U != num2)
			{
				throw new DeviceException(string.Format("Reboot failed, response code: {0}", num2));
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		public void StartDeviceUpdateScan(uint throttle)
		{
			uint num = 0U;
			uint num2 = 0U;
			PortableDeviceApiLib.IPortableDevicePropVariantCollection mtpParameters = (PortableDeviceApiLib.IPortableDevicePropVariantCollection)((PortableDevicePropVariantCollection)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("08A99E2F-6D6D-4B80-AF5A-BAF2BCBE4CB9"))));
			WpdUtils.AddUnsignedIntegerValue(mtpParameters, throttle);
			WpdUtils.ExecuteMtpOpcode(this.portableDevice, 37908U, mtpParameters, out num, out num2);
			if (num != 0U)
			{
				if (2147942431U != num)
				{
					throw new DeviceException(string.Format("StartDeviceUpdate failed, hresult: {0}", num));
				}
			}
			else if (8193U != num2)
			{
				throw new DeviceException(string.Format("StartDeviceUpdate failed, response code: {0}", num2));
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00010C74 File Offset: 0x0000EE74
		public void StartDeviceUpdateOtcScan()
		{
			uint num = 0U;
			uint num2 = 0U;
			WpdUtils.ExecuteMtpOpcode(this.portableDevice, 37910U, out num, out num2);
			if (num != 0U)
			{
				throw new DeviceException(string.Format("InitiateOtcScan failed, hresult: {0}", num));
			}
			if (8194U == num2)
			{
				throw new DeviceException(string.Format("InitiateOtcScan failed, response code: {0} (This often means an OTA update is already in progress. Please allow that update to complete before trying again.)", num2));
			}
			if (8193U != num2)
			{
				throw new DeviceException(string.Format("InitiateOtcScan failed, response code: {0}", num2));
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		public void InitiateDuInstall()
		{
			uint num = 0U;
			uint num2 = 0U;
			WpdUtils.ExecuteMtpOpcode(this.portableDevice, 37905U, out num, out num2);
			if (num != 0U)
			{
				throw new DeviceException(string.Format("InitiateDuInstall failed, hresult: {0}", num));
			}
			if (8193U != num2)
			{
				throw new DeviceException(string.Format("InitiateDuInstall failed, response code: {0}", num2));
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00010D4C File Offset: 0x0000EF4C
		public void ClearDuStagingDirectory()
		{
			uint num = 0U;
			uint num2 = 0U;
			WpdUtils.ExecuteMtpOpcode(this.portableDevice, 37906U, out num, out num2);
			if (num != 0U)
			{
				throw new DeviceException(string.Format("ClearDuStagingDirectory, hresult: {0}", num));
			}
			if (8193U != num2)
			{
				throw new DeviceException(string.Format("ClearDuStagingDirectory, response code: {0}", num2));
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		public void SendCompositionDB(string path)
		{
			using (FileStream fileStream = LongPathFile.OpenRead(path))
			{
				this.SendCompositionDB(fileStream);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00010DE0 File Offset: 0x0000EFE0
		public void SendCompositionDB(Stream stream)
		{
			uint num = 0U;
			uint num2 = 0U;
			PortableDeviceApiLib.IPortableDevicePropVariantCollection portableDevicePropVariantCollection = null;
			PortableDeviceApiLib.IPortableDevicePropVariantCollection mtpParameters = (PortableDeviceApiLib.IPortableDevicePropVariantCollection)((PortableDevicePropVariantCollection)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("08A99E2F-6D6D-4B80-AF5A-BAF2BCBE4CB9"))));
			WpdUtils.AddUnsignedIntegerValue(mtpParameters, (uint)stream.Length);
			WpdUtils.AddUnsignedIntegerValue(mtpParameters, 2U);
			WpdUtils.ExecuteMtpOpcodeAndWriteData(this.portableDevice, mtpParameters, 37907U, stream, (uint)stream.Length, out num, out num2, out portableDevicePropVariantCollection);
			if (num != 0U)
			{
				throw new DeviceException(string.Format("SendCompositionDB, hresult: {0}", num));
			}
			if (8204U == num2)
			{
				throw new DeviceException(string.Format("SendCompositionDB, response code: {0} (Disk Full)", num2));
			}
			if (8193U != num2)
			{
				throw new DeviceException(string.Format("SendCompositionDB, response code: {0}", num2));
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00010E9C File Offset: 0x0000F09C
		public void SendUpdateAgent(string path)
		{
			using (FileStream fileStream = LongPathFile.OpenRead(path))
			{
				this.SendUpdateAgent(fileStream);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		public void SendUpdateAgent(Stream stream)
		{
			uint num = 0U;
			uint num2 = 0U;
			PortableDeviceApiLib.IPortableDevicePropVariantCollection portableDevicePropVariantCollection = null;
			PortableDeviceApiLib.IPortableDevicePropVariantCollection mtpParameters = (PortableDeviceApiLib.IPortableDevicePropVariantCollection)((PortableDevicePropVariantCollection)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("08A99E2F-6D6D-4B80-AF5A-BAF2BCBE4CB9"))));
			WpdUtils.AddUnsignedIntegerValue(mtpParameters, (uint)stream.Length);
			WpdUtils.AddUnsignedIntegerValue(mtpParameters, 1U);
			WpdUtils.ExecuteMtpOpcodeAndWriteData(this.portableDevice, mtpParameters, 37907U, stream, (uint)stream.Length, out num, out num2, out portableDevicePropVariantCollection);
			if (num != 0U)
			{
				throw new DeviceException(string.Format("SendUpdateAgent, hresult: {0}", num));
			}
			if (8204U == num2)
			{
				throw new DeviceException(string.Format("SendUpdateAgent, response code: {0} (Disk Full)", num2));
			}
			if (8193U != num2)
			{
				throw new DeviceException(string.Format("SendUpdateAgent, response code: {0}", num2));
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00010F90 File Offset: 0x0000F190
		public void SendIuPackage(string path)
		{
			using (FileStream fileStream = LongPathFile.OpenRead(path))
			{
				this.SendIuPackage(fileStream);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		public void SendIuPackage(Stream stream)
		{
			this.SendIuPackageWithMode(stream, 0U);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00010FD4 File Offset: 0x0000F1D4
		public void SendIuPackage(string path, string targetName)
		{
			using (FileStream fileStream = LongPathFile.OpenRead(path))
			{
				this.SendIuPackageName(targetName);
				this.SendIuPackageWithMode(fileStream, 4U);
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00011014 File Offset: 0x0000F214
		public void SendIuPackageName(string targetName)
		{
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(targetName)))
			{
				this.SendIuPackageWithMode(memoryStream, 3U);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00011058 File Offset: 0x0000F258
		public void SendIuPackageWithMode(Stream stream, uint mode)
		{
			uint num = 0U;
			uint num2 = 0U;
			PortableDeviceApiLib.IPortableDevicePropVariantCollection portableDevicePropVariantCollection = null;
			PortableDeviceApiLib.IPortableDevicePropVariantCollection mtpParameters = (PortableDeviceApiLib.IPortableDevicePropVariantCollection)((PortableDevicePropVariantCollection)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("08A99E2F-6D6D-4B80-AF5A-BAF2BCBE4CB9"))));
			WpdUtils.AddUnsignedIntegerValue(mtpParameters, (uint)stream.Length);
			WpdUtils.AddUnsignedIntegerValue(mtpParameters, mode);
			WpdUtils.ExecuteMtpOpcodeAndWriteData(this.portableDevice, mtpParameters, 37907U, stream, (uint)stream.Length, out num, out num2, out portableDevicePropVariantCollection);
			if (num != 0U)
			{
				throw new DeviceException(string.Format("SendIuPackage, mode: {0}, hresult: {1}", mode, num));
			}
			if (8204U == num2)
			{
				throw new DeviceException(string.Format("SendIuPackage, mode: {0}, response code: {1} (Disk Full)", mode, num2));
			}
			if (8193U != num2)
			{
				throw new DeviceException(string.Format("SendIuPackage, mode: {0}, response code: {1}", mode, num2));
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00011124 File Offset: 0x0000F324
		public void GetActionList(string path)
		{
			this.OnNormalMessageEvent("Retrieving update action list...");
			uint num = 0U;
			uint num2 = 0U;
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				PortableDeviceApiLib.IPortableDevicePropVariantCollection mtpParameters = (PortableDeviceApiLib.IPortableDevicePropVariantCollection)((PortableDevicePropVariantCollection)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("08A99E2F-6D6D-4B80-AF5A-BAF2BCBE4CB9"))));
				WpdUtils.AddUnsignedIntegerValue(mtpParameters, 1U);
				WpdUtils.ExecuteMtpOpcodeAndReadData(this.portableDevice, mtpParameters, 37911U, fileStream, out num, out num2);
			}
			if (num != 0U)
			{
				throw new DeviceException(string.Format("GetActionList, hresult: {0}", num));
			}
			if (8193U != num2)
			{
				throw new DeviceException(string.Format("GetActionList, response code: {0}", num2));
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000111D8 File Offset: 0x0000F3D8
		public void GetDuDiagnostics(string path)
		{
			this.OnNormalMessageEvent("Collecting log files...");
			uint num = 0U;
			uint num2 = 0U;
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				PortableDeviceApiLib.IPortableDevicePropVariantCollection mtpParameters = (PortableDeviceApiLib.IPortableDevicePropVariantCollection)((PortableDevicePropVariantCollection)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("08A99E2F-6D6D-4B80-AF5A-BAF2BCBE4CB9"))));
				WpdUtils.AddUnsignedIntegerValue(mtpParameters, 1U);
				WpdUtils.ExecuteMtpOpcodeAndReadData(this.portableDevice, mtpParameters, 37904U, fileStream, out num, out num2);
			}
			if (num != 0U)
			{
				throw new DeviceException(string.Format("GetDuDiagnostics, hresult: {0}", num));
			}
			if (8193U != num2)
			{
				throw new DeviceException(string.Format("GetDuDiagnostics, response code: {0}", num2));
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0001128C File Offset: 0x0000F48C
		public void GetPackageInfo(string path)
		{
			uint num = 0U;
			uint num2 = 0U;
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				PortableDeviceApiLib.IPortableDevicePropVariantCollection mtpParameters = (PortableDeviceApiLib.IPortableDevicePropVariantCollection)((PortableDevicePropVariantCollection)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("08A99E2F-6D6D-4B80-AF5A-BAF2BCBE4CB9"))));
				WpdUtils.AddUnsignedIntegerValue(mtpParameters, 1U);
				WpdUtils.ExecuteMtpOpcodeAndReadData(this.portableDevice, mtpParameters, 37909U, fileStream, out num, out num2);
			}
			if (num != 0U)
			{
				throw new DeviceException(string.Format("GetPackageInfo, hresult: {0}", num));
			}
			if (8193U != num2)
			{
				throw new DeviceException(string.Format("GetPackageInfo, response code: {0}", num2));
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00011334 File Offset: 0x0000F534
		private InstalledPackageInfo[] GetInstalledPackages()
		{
			this.OnNormalMessageEvent("Retrieving list of installed packages...");
			string text = null;
			string text2 = null;
			InstalledPackageInfo[] result;
			try
			{
				try
				{
					text2 = Path.GetTempFileName();
					this.GetPackageInfo(text2);
				}
				catch
				{
					if (!string.IsNullOrEmpty(text2))
					{
						WpdDevice.Delete(text2);
					}
					text = Path.GetTempFileName();
					string tempPath = Path.GetTempPath();
					this.GetDuDiagnostics(text);
					CabApiWrapper.ExtractOne(text, tempPath, "InstalledPackages.csv");
					WpdDevice.Delete(text);
					text2 = Path.Combine(tempPath, "InstalledPackages.csv");
				}
				FileInfo fileInfo = new FileInfo(text2);
				if (fileInfo.Length == 0L)
				{
					this.OnWarningMessageEvent(string.Format("{0} in DuDiagnostics is zero bytes. This usually indicates an update is already in progress.", "InstalledPackages.csv"));
				}
				List<InstalledPackageInfo> list = new List<InstalledPackageInfo>();
				using (StreamReader streamReader = new StreamReader(text2))
				{
					if (streamReader.ReadLine() == null)
					{
						throw new DeviceException(string.Format("{0} in DuDiagnostics doesn't have header", "InstalledPackages.csv"));
					}
					string text3;
					while ((text3 = streamReader.ReadLine()) != null)
					{
						string[] array = text3.Split(new char[]
						{
							','
						});
						if (3 != array.Length)
						{
							throw new DeviceException(string.Format("{0} file in DuDiagnostics has invalid format", "InstalledPackages.csv"));
						}
						InstalledPackageInfo item = new InstalledPackageInfo(array[0].ToUpper(), array[1].ToUpper(), array[2].ToUpper());
						list.Add(item);
					}
				}
				WpdDevice.Delete(text2);
				this.OnNormalMessageEvent("Retrieved list of installed packages");
				if (list.Count == 0)
				{
					throw new DeviceException("Device package count is 0");
				}
				result = list.ToArray();
			}
			catch (Exception arg)
			{
				throw new DeviceException(string.Format("Error retrieving list of installed packages: {0}", arg));
			}
			finally
			{
				if (!string.IsNullOrEmpty(text))
				{
					WpdDevice.Delete(text);
				}
				if (!string.IsNullOrEmpty(text2))
				{
					WpdDevice.Delete(text2);
				}
			}
			return result;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00011534 File Offset: 0x0000F734
		protected override void DisposeManaged()
		{
			if (this.authMtpDeviceService != null)
			{
				this.authMtpDeviceService.Close();
				this.authMtpDeviceService = null;
			}
			if (this.duMtpDeviceService != null)
			{
				this.duMtpDeviceService.Close();
				this.duMtpDeviceService = null;
			}
			base.DisposeManaged();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00011570 File Offset: 0x0000F770
		private static void Delete(string filePath)
		{
			if (File.Exists(filePath))
			{
				File.SetAttributes(filePath, FileAttributes.Normal);
				File.Delete(filePath);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0001158C File Offset: 0x0000F78C
		private void LoadMtpService(string name, out IPortableDeviceService service)
		{
			try
			{
				WpdUtils.GetDeviceService(this.wpdManager, this.deviceId, name, out service);
			}
			catch
			{
				throw new DeviceException(string.Format("Error loading MTP device service: {0}", name));
			}
			if (service == null)
			{
				throw new DeviceException(string.Format("Error loading MTP device service: {0}", name));
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000115E8 File Offset: 0x0000F7E8
		private void LoadAuthMtpService()
		{
			if (this.authMtpDeviceService != null)
			{
				return;
			}
			this.LoadMtpService("Authentication", out this.authMtpDeviceService);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00011604 File Offset: 0x0000F804
		private void LoadStatusMtpService()
		{
			if (this.statusMtpDeviceService != null)
			{
				return;
			}
			this.LoadMtpService("Status", out this.statusMtpDeviceService);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00011620 File Offset: 0x0000F820
		private void LoadDuMtpService()
		{
			if (this.duMtpDeviceService != null)
			{
				return;
			}
			this.LoadMtpService("MtpDuDeviceService", out this.duMtpDeviceService);
			this.osVersion = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUOSVERSION);
			this.branch = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUBRANCHNAME);
			this.coreSysBuildNumber = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUCORESYSBUILDNUMBER);
			this.windowsPhoneBuildNumber = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUWINDOWSPHONEBUILDNUMBER);
			this.buildTimeStamp = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUBUILDTIMESTAMP);
			this.oem = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUOEM);
			this.moId = "";
			this.revisionNumber = "";
			try
			{
				this.firmwareVersion = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUFIRMWAREVERSION);
			}
			catch
			{
				this.firmwareVersion = "";
			}
			try
			{
				this.oemDeviceName = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUOEMDEVICENAME);
			}
			catch
			{
				this.oemDeviceName = "";
			}
			try
			{
				this.uefiName = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUPLATFORMID);
			}
			catch
			{
				this.uefiName = "";
			}
			try
			{
				this.wpSerialNumber = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUSERIALNUMBER);
			}
			catch
			{
				this.wpSerialNumber = "";
			}
			try
			{
				this.deviceUniqueId = WpdUtils.GetServicePropertyGuid(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUSMBIOSUUID);
			}
			catch
			{
				this.deviceUniqueId = Guid.Empty;
			}
			try
			{
				this.imageTargetingType = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUIMAGETARGETINGTYPE);
			}
			catch
			{
				this.imageTargetingType = "";
			}
			try
			{
				this.feedbackId = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUFEEDBACKID);
			}
			catch
			{
				this.feedbackId = "";
			}
			try
			{
				this.imei = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUIMEI);
			}
			catch
			{
				this.imei = "";
			}
			try
			{
				this.totalStorage = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUTOTALSTORAGE);
			}
			catch
			{
				this.totalStorage = "";
			}
			try
			{
				this.totalRAM = WpdUtils.GetServicePropertyStringValue(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUTOTALRAM);
			}
			catch
			{
				this.totalRAM = "";
			}
			try
			{
				ushort[] servicePropertyUint16Array = WpdUtils.GetServicePropertyUint16Array(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUCTACPROPERTIES);
				byte[] array = new byte[servicePropertyUint16Array.Length * 2];
				for (int i = 0; i < servicePropertyUint16Array.Length; i++)
				{
					byte[] bytes = BitConverter.GetBytes(servicePropertyUint16Array[i]);
					array[i * 2] = bytes[0];
					array[i * 2 + 1] = bytes[1];
				}
				this.ctac = Encoding.Unicode.GetString(array, 0, array.Length);
			}
			catch
			{
				this.ctac = "";
			}
			if ("" != this.ctac)
			{
				if ("" == this.oemDeviceName)
				{
					try
					{
						this.oemDeviceName = this.ExtractPropertyFromCTAC(this.ctac, "OEMModel");
					}
					catch
					{
						this.oemDeviceName = "";
					}
				}
				if ("" == this.revisionNumber)
				{
					try
					{
						string[] array2 = this.ExtractPropertyFromCTAC(this.ctac, "OSVersion").Split(new char[]
						{
							'.'
						});
						this.revisionNumber = array2[3];
					}
					catch
					{
						this.revisionNumber = "";
					}
				}
			}
			try
			{
				ushort[] servicePropertyUint16Array2 = WpdUtils.GetServicePropertyUint16Array(this.duMtpDeviceService, WpdDevice.WPD_MTPDUDEVICESERVICE_DUPRODUCTPROPERTIES);
				byte[] array3 = new byte[servicePropertyUint16Array2.Length * 2];
				for (int j = 0; j < servicePropertyUint16Array2.Length; j++)
				{
					byte[] bytes2 = BitConverter.GetBytes(servicePropertyUint16Array2[j]);
					array3[j * 2] = bytes2[0];
					array3[j * 2 + 1] = bytes2[1];
				}
				this.productNames = Encoding.Unicode.GetString(array3, 0, array3.Length);
			}
			catch
			{
				this.productNames = "";
			}
			try
			{
				this.osEdition = this.ExtractOsEditionFromProducts(this.productNames);
			}
			catch
			{
				this.osEdition = "";
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		private string ExtractOsEditionFromProducts(string products)
		{
			string pattern = "PN=(\\w+).OS";
			MatchCollection matchCollection = Regex.Matches(products, pattern);
			string result;
			if (1 == matchCollection.Count)
			{
				result = matchCollection[0].Groups[1].Value;
			}
			else
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00011B10 File Offset: 0x0000FD10
		private string ExtractPropertyFromCTAC(string ctac, string propertyName)
		{
			bool flag = false;
			foreach (string text in ctac.Split(new char[]
			{
				','
			}))
			{
				if (text.Contains(propertyName))
				{
					string[] array2 = text.Split(new char[]
					{
						':'
					});
					for (int j = 0; j < array2.Length; j++)
					{
						string text2 = array2[j].Replace("\"", "");
						if (propertyName == text2)
						{
							flag = true;
						}
						else if (flag)
						{
							return text2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00011B9D File Offset: 0x0000FD9D
		protected void OnProgressEvent(string message)
		{
			if (this.ProgressEvent != null)
			{
				this.ProgressEvent(this, new MessageArgs(message));
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00011BB9 File Offset: 0x0000FDB9
		protected void OnNormalMessageEvent(string message)
		{
			if (this.NormalMessageEvent != null)
			{
				this.NormalMessageEvent(this, new MessageArgs(message));
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00011BD5 File Offset: 0x0000FDD5
		protected void OnWarningMessageEvent(string message)
		{
			if (this.WarningMessageEvent != null)
			{
				this.WarningMessageEvent(this, new MessageArgs(message));
			}
		}

		// Token: 0x040002EF RID: 751
		private IPortableDeviceManager wpdManager;

		// Token: 0x040002F0 RID: 752
		private IPortableDevice portableDevice;

		// Token: 0x040002F1 RID: 753
		private IPortableDeviceService duMtpDeviceService;

		// Token: 0x040002F2 RID: 754
		private IPortableDeviceService authMtpDeviceService;

		// Token: 0x040002F3 RID: 755
		private IPortableDeviceService statusMtpDeviceService;

		// Token: 0x040002F4 RID: 756
		private InstalledPackageInfo[] installedPackages;

		// Token: 0x040002F5 RID: 757
		private bool isMtpSessionUnlocked;

		// Token: 0x040002F6 RID: 758
		private const uint DeviceDBMetadata = 1U;

		// Token: 0x040002F7 RID: 759
		private const uint ErrorGenFailure = 2147942431U;

		// Token: 0x040002F8 RID: 760
		private const string AuthMtpDeviceServiceName = "Authentication";

		// Token: 0x040002F9 RID: 761
		private static readonly Guid AuthMtpDevicePropertyGuid = new Guid(3506435576U, 48324, 17025, 183, 188, 59, 19, 169, 9, 194, 194);

		// Token: 0x040002FA RID: 762
		private static readonly _tagpropertykey WPD_MTPAUTHDEVICESERVICE_ISLOCKED;

		// Token: 0x040002FB RID: 763
		public const string DuMtpDeviceServiceName = "MtpDuDeviceService";

		// Token: 0x040002FC RID: 764
		private const string InstalledPackagesFileName = "InstalledPackages.csv";

		// Token: 0x040002FD RID: 765
		public const string StatusServiceName = "Status";

		// Token: 0x040002FE RID: 766
		public static readonly Guid WindowsPhone8ModelID = new Guid(1508978345, 21454, 17709, 151, 17, 202, 78, 234, 241, 128, 137);

		// Token: 0x040002FF RID: 767
		public static Guid DuMtpDeviceServiceGuid = new Guid(2617009345U, 6601, 20285, 161, 77, 200, 219, 224, 71, 93, 19);

		// Token: 0x04000300 RID: 768
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUENGINESTATE;

		// Token: 0x04000301 RID: 769
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DURESULT;

		// Token: 0x04000302 RID: 770
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUBRANCHNAME;

		// Token: 0x04000303 RID: 771
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUBUILDER;

		// Token: 0x04000304 RID: 772
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUCORESYSBUILDNUMBER;

		// Token: 0x04000305 RID: 773
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUWINDOWSPHONEBUILDNUMBER;

		// Token: 0x04000306 RID: 774
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUBUILDTIMESTAMP;

		// Token: 0x04000307 RID: 775
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DULANGUAGES;

		// Token: 0x04000308 RID: 776
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DURESOLUTION;

		// Token: 0x04000309 RID: 777
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUSTAGINGPERCENTAGE;

		// Token: 0x0400030A RID: 778
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUOSVERSION;

		// Token: 0x0400030B RID: 779
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUMOID;

		// Token: 0x0400030C RID: 780
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUOEM;

		// Token: 0x0400030D RID: 781
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUOEMDEVICENAME;

		// Token: 0x0400030E RID: 782
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUFIRMWAREVERSION;

		// Token: 0x0400030F RID: 783
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUSOCVERSION;

		// Token: 0x04000310 RID: 784
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DURADIOSWVERSION;

		// Token: 0x04000311 RID: 785
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DURADIOHWVERSION;

		// Token: 0x04000312 RID: 786
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUBOOTLOADERVERSION;

		// Token: 0x04000313 RID: 787
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUMUILANGUAGEIDS;

		// Token: 0x04000314 RID: 788
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUBOOTUILANGUAGEIDS;

		// Token: 0x04000315 RID: 789
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUBOOTLOCALELANGUAGEIDS;

		// Token: 0x04000316 RID: 790
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUSPEECHLANGUAGEIDS;

		// Token: 0x04000317 RID: 791
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUKEYBOARDLANGUAGEIDS;

		// Token: 0x04000318 RID: 792
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUSUPPORTEDRESOLUTIONS;

		// Token: 0x04000319 RID: 793
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUFEEDBACKID;

		// Token: 0x0400031A RID: 794
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUPLATFORMID;

		// Token: 0x0400031B RID: 795
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUISPRODUCTIONCONFIGURATION;

		// Token: 0x0400031C RID: 796
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUIMAGETARGETINGTYPE;

		// Token: 0x0400031D RID: 797
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUSMBIOSUUID;

		// Token: 0x0400031E RID: 798
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUDEVICEUPDATERESULT;

		// Token: 0x0400031F RID: 799
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUSHELLSTARTREADY;

		// Token: 0x04000320 RID: 800
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUSERIALNUMBER;

		// Token: 0x04000321 RID: 801
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUSHELLAPIREADY;

		// Token: 0x04000322 RID: 802
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUIMEI;

		// Token: 0x04000323 RID: 803
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUTOTALSTORAGE;

		// Token: 0x04000324 RID: 804
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUTOTALRAM;

		// Token: 0x04000325 RID: 805
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUUPDATEAGENTERROR;

		// Token: 0x04000326 RID: 806
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUSENDIUPACKAGEOPTIONS;

		// Token: 0x04000327 RID: 807
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUCTACPROPERTIES;

		// Token: 0x04000328 RID: 808
		public static readonly _tagpropertykey WPD_MTPDUDEVICESERVICE_DUPRODUCTPROPERTIES;

		// Token: 0x04000329 RID: 809
		public static Guid StatusServiceGuid = new Guid(1238179702, 22054, 19223, 164, 232, 24, 180, 170, 26, 34, 19);

		// Token: 0x0400032A RID: 810
		public static readonly _tagpropertykey WPD_STATUSSERVICE_BATTERYLIFE;

		// Token: 0x0400032B RID: 811
		private const uint MtpOpCodeRebootDeviceFlashing = 37889U;

		// Token: 0x0400032C RID: 812
		private const uint MtpOpCodeRebootDevice = 37893U;

		// Token: 0x0400032D RID: 813
		private const uint MtpOpCodeGetDuDiagInfo = 37904U;

		// Token: 0x0400032E RID: 814
		private const uint MtpOpCodeInitiateDuInstall = 37905U;

		// Token: 0x0400032F RID: 815
		private const uint MtpOpCodeClearDuStagingDir = 37906U;

		// Token: 0x04000330 RID: 816
		private const uint MtpOpCodeSendIuPackage = 37907U;

		// Token: 0x04000331 RID: 817
		private const uint MtpOpCodeStartDeviceUpdate = 37908U;

		// Token: 0x04000332 RID: 818
		private const uint MtpOpCodeGetPackageInfo = 37909U;

		// Token: 0x04000333 RID: 819
		private const uint MtpOpCodeInitiateOtcScan = 37910U;

		// Token: 0x04000334 RID: 820
		private const uint MtpOpCodeGetActionList = 37911U;

		// Token: 0x04000335 RID: 821
		private string model;

		// Token: 0x04000336 RID: 822
		private string friendlyName;

		// Token: 0x04000337 RID: 823
		private string deviceId;

		// Token: 0x04000338 RID: 824
		private string branch;

		// Token: 0x04000339 RID: 825
		private string windowsPhoneBuildNumber;

		// Token: 0x0400033A RID: 826
		private string coreSysBuildNumber;

		// Token: 0x0400033B RID: 827
		private string revisionNumber;

		// Token: 0x0400033C RID: 828
		private string buildTimeStamp;

		// Token: 0x0400033D RID: 829
		private string imageTargetingType;

		// Token: 0x0400033E RID: 830
		private string feedbackId;

		// Token: 0x0400033F RID: 831
		private string osVersion;

		// Token: 0x04000340 RID: 832
		private string firmwareVersion;

		// Token: 0x04000341 RID: 833
		private string moId;

		// Token: 0x04000342 RID: 834
		private string serialNumber;

		// Token: 0x04000343 RID: 835
		private string manufacturer;

		// Token: 0x04000344 RID: 836
		private string oemDeviceName;

		// Token: 0x04000345 RID: 837
		private string oem;

		// Token: 0x04000346 RID: 838
		private string uefiName;

		// Token: 0x04000347 RID: 839
		private string resolution;

		// Token: 0x04000348 RID: 840
		private Guid deviceUniqueId;

		// Token: 0x04000349 RID: 841
		private string wpSerialNumber;

		// Token: 0x0400034A RID: 842
		private string imei;

		// Token: 0x0400034B RID: 843
		private string totalStorage;

		// Token: 0x0400034C RID: 844
		private string totalRAM;

		// Token: 0x0400034D RID: 845
		private string ctac;

		// Token: 0x0400034E RID: 846
		private string productNames;

		// Token: 0x0400034F RID: 847
		private string osEdition;
	}
}
