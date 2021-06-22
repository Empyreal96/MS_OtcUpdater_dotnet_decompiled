using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200001F RID: 31
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ComputerInfo", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class ComputerInfo : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000079F0 File Offset: 0x00005BF0
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000079F8 File Offset: 0x00005BF8
		[Browsable(false)]
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007A01 File Offset: 0x00005C01
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00007A09 File Offset: 0x00005C09
		[DataMember(EmitDefaultValue = false)]
		public string DnsName
		{
			get
			{
				return this.DnsNameField;
			}
			set
			{
				if (this.DnsNameField != value)
				{
					this.DnsNameField = value;
					this.RaisePropertyChanged("DnsName");
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007A26 File Offset: 0x00005C26
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00007A2E File Offset: 0x00005C2E
		[DataMember(IsRequired = true)]
		public int OSMajorVersion
		{
			get
			{
				return this.OSMajorVersionField;
			}
			set
			{
				if (!this.OSMajorVersionField.Equals(value))
				{
					this.OSMajorVersionField = value;
					this.RaisePropertyChanged("OSMajorVersion");
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007A50 File Offset: 0x00005C50
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00007A58 File Offset: 0x00005C58
		[DataMember(IsRequired = true)]
		public int OSMinorVersion
		{
			get
			{
				return this.OSMinorVersionField;
			}
			set
			{
				if (!this.OSMinorVersionField.Equals(value))
				{
					this.OSMinorVersionField = value;
					this.RaisePropertyChanged("OSMinorVersion");
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007A7A File Offset: 0x00005C7A
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00007A82 File Offset: 0x00005C82
		[DataMember(IsRequired = true, Order = 3)]
		public int OSBuildNumber
		{
			get
			{
				return this.OSBuildNumberField;
			}
			set
			{
				if (!this.OSBuildNumberField.Equals(value))
				{
					this.OSBuildNumberField = value;
					this.RaisePropertyChanged("OSBuildNumber");
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007AA4 File Offset: 0x00005CA4
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00007AAC File Offset: 0x00005CAC
		[DataMember(IsRequired = true, Order = 4)]
		public short OSServicePackMajorNumber
		{
			get
			{
				return this.OSServicePackMajorNumberField;
			}
			set
			{
				if (!this.OSServicePackMajorNumberField.Equals(value))
				{
					this.OSServicePackMajorNumberField = value;
					this.RaisePropertyChanged("OSServicePackMajorNumber");
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007ACE File Offset: 0x00005CCE
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00007AD6 File Offset: 0x00005CD6
		[DataMember(IsRequired = true, Order = 5)]
		public short OSServicePackMinorNumber
		{
			get
			{
				return this.OSServicePackMinorNumberField;
			}
			set
			{
				if (!this.OSServicePackMinorNumberField.Equals(value))
				{
					this.OSServicePackMinorNumberField = value;
					this.RaisePropertyChanged("OSServicePackMinorNumber");
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00007AF8 File Offset: 0x00005CF8
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007B00 File Offset: 0x00005D00
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string OSLocale
		{
			get
			{
				return this.OSLocaleField;
			}
			set
			{
				if (this.OSLocaleField != value)
				{
					this.OSLocaleField = value;
					this.RaisePropertyChanged("OSLocale");
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007B1D File Offset: 0x00005D1D
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00007B25 File Offset: 0x00005D25
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public string ComputerManufacturer
		{
			get
			{
				return this.ComputerManufacturerField;
			}
			set
			{
				if (this.ComputerManufacturerField != value)
				{
					this.ComputerManufacturerField = value;
					this.RaisePropertyChanged("ComputerManufacturer");
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00007B42 File Offset: 0x00005D42
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00007B4A File Offset: 0x00005D4A
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public string ComputerModel
		{
			get
			{
				return this.ComputerModelField;
			}
			set
			{
				if (this.ComputerModelField != value)
				{
					this.ComputerModelField = value;
					this.RaisePropertyChanged("ComputerModel");
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00007B67 File Offset: 0x00005D67
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00007B6F File Offset: 0x00005D6F
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public string BiosVersion
		{
			get
			{
				return this.BiosVersionField;
			}
			set
			{
				if (this.BiosVersionField != value)
				{
					this.BiosVersionField = value;
					this.RaisePropertyChanged("BiosVersion");
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007B8C File Offset: 0x00005D8C
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00007B94 File Offset: 0x00005D94
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public string BiosName
		{
			get
			{
				return this.BiosNameField;
			}
			set
			{
				if (this.BiosNameField != value)
				{
					this.BiosNameField = value;
					this.RaisePropertyChanged("BiosName");
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007BB1 File Offset: 0x00005DB1
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00007BB9 File Offset: 0x00005DB9
		[DataMember(IsRequired = true, Order = 11)]
		public DateTime BiosReleaseDate
		{
			get
			{
				return this.BiosReleaseDateField;
			}
			set
			{
				if (!this.BiosReleaseDateField.Equals(value))
				{
					this.BiosReleaseDateField = value;
					this.RaisePropertyChanged("BiosReleaseDate");
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007BDB File Offset: 0x00005DDB
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00007BE3 File Offset: 0x00005DE3
		[DataMember(EmitDefaultValue = false, Order = 12)]
		public string ProcessorArchitecture
		{
			get
			{
				return this.ProcessorArchitectureField;
			}
			set
			{
				if (this.ProcessorArchitectureField != value)
				{
					this.ProcessorArchitectureField = value;
					this.RaisePropertyChanged("ProcessorArchitecture");
				}
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007C00 File Offset: 0x00005E00
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00007C08 File Offset: 0x00005E08
		[DataMember(IsRequired = true, Order = 13)]
		public short SuiteMask
		{
			get
			{
				return this.SuiteMaskField;
			}
			set
			{
				if (!this.SuiteMaskField.Equals(value))
				{
					this.SuiteMaskField = value;
					this.RaisePropertyChanged("SuiteMask");
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007C2A File Offset: 0x00005E2A
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00007C32 File Offset: 0x00005E32
		[DataMember(IsRequired = true, Order = 14)]
		public byte OldProductType
		{
			get
			{
				return this.OldProductTypeField;
			}
			set
			{
				if (!this.OldProductTypeField.Equals(value))
				{
					this.OldProductTypeField = value;
					this.RaisePropertyChanged("OldProductType");
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00007C54 File Offset: 0x00005E54
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00007C5C File Offset: 0x00005E5C
		[DataMember(IsRequired = true, Order = 15)]
		public int NewProductType
		{
			get
			{
				return this.NewProductTypeField;
			}
			set
			{
				if (!this.NewProductTypeField.Equals(value))
				{
					this.NewProductTypeField = value;
					this.RaisePropertyChanged("NewProductType");
				}
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00007C7E File Offset: 0x00005E7E
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00007C86 File Offset: 0x00005E86
		[DataMember(IsRequired = true, Order = 16)]
		public int SystemMetrics
		{
			get
			{
				return this.SystemMetricsField;
			}
			set
			{
				if (!this.SystemMetricsField.Equals(value))
				{
					this.SystemMetricsField = value;
					this.RaisePropertyChanged("SystemMetrics");
				}
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00007CA8 File Offset: 0x00005EA8
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00007CB0 File Offset: 0x00005EB0
		[DataMember(IsRequired = true, Order = 17)]
		public short ClientVersionMajorNumber
		{
			get
			{
				return this.ClientVersionMajorNumberField;
			}
			set
			{
				if (!this.ClientVersionMajorNumberField.Equals(value))
				{
					this.ClientVersionMajorNumberField = value;
					this.RaisePropertyChanged("ClientVersionMajorNumber");
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00007CD2 File Offset: 0x00005ED2
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00007CDA File Offset: 0x00005EDA
		[DataMember(IsRequired = true, Order = 18)]
		public short ClientVersionMinorNumber
		{
			get
			{
				return this.ClientVersionMinorNumberField;
			}
			set
			{
				if (!this.ClientVersionMinorNumberField.Equals(value))
				{
					this.ClientVersionMinorNumberField = value;
					this.RaisePropertyChanged("ClientVersionMinorNumber");
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00007CFC File Offset: 0x00005EFC
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00007D04 File Offset: 0x00005F04
		[DataMember(IsRequired = true, Order = 19)]
		public short ClientVersionBuildNumber
		{
			get
			{
				return this.ClientVersionBuildNumberField;
			}
			set
			{
				if (!this.ClientVersionBuildNumberField.Equals(value))
				{
					this.ClientVersionBuildNumberField = value;
					this.RaisePropertyChanged("ClientVersionBuildNumber");
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00007D26 File Offset: 0x00005F26
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00007D2E File Offset: 0x00005F2E
		[DataMember(IsRequired = true, Order = 20)]
		public short ClientVersionQfeNumber
		{
			get
			{
				return this.ClientVersionQfeNumberField;
			}
			set
			{
				if (!this.ClientVersionQfeNumberField.Equals(value))
				{
					this.ClientVersionQfeNumberField = value;
					this.RaisePropertyChanged("ClientVersionQfeNumber");
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007D50 File Offset: 0x00005F50
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00007D58 File Offset: 0x00005F58
		[DataMember(EmitDefaultValue = false, Order = 21)]
		public string OSDescription
		{
			get
			{
				return this.OSDescriptionField;
			}
			set
			{
				if (this.OSDescriptionField != value)
				{
					this.OSDescriptionField = value;
					this.RaisePropertyChanged("OSDescription");
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007D75 File Offset: 0x00005F75
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00007D7D File Offset: 0x00005F7D
		[DataMember(EmitDefaultValue = false, Order = 22)]
		public string OEM
		{
			get
			{
				return this.OEMField;
			}
			set
			{
				if (this.OEMField != value)
				{
					this.OEMField = value;
					this.RaisePropertyChanged("OEM");
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007D9A File Offset: 0x00005F9A
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00007DA2 File Offset: 0x00005FA2
		[DataMember(EmitDefaultValue = false, Order = 23)]
		public string DeviceType
		{
			get
			{
				return this.DeviceTypeField;
			}
			set
			{
				if (this.DeviceTypeField != value)
				{
					this.DeviceTypeField = value;
					this.RaisePropertyChanged("DeviceType");
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007DBF File Offset: 0x00005FBF
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00007DC7 File Offset: 0x00005FC7
		[DataMember(EmitDefaultValue = false, Order = 24)]
		public string FirmwareVersion
		{
			get
			{
				return this.FirmwareVersionField;
			}
			set
			{
				if (this.FirmwareVersionField != value)
				{
					this.FirmwareVersionField = value;
					this.RaisePropertyChanged("FirmwareVersion");
				}
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007DE4 File Offset: 0x00005FE4
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00007DEC File Offset: 0x00005FEC
		[DataMember(EmitDefaultValue = false, Order = 25)]
		public string MobileOperator
		{
			get
			{
				return this.MobileOperatorField;
			}
			set
			{
				if (this.MobileOperatorField != value)
				{
					this.MobileOperatorField = value;
					this.RaisePropertyChanged("MobileOperator");
				}
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600018E RID: 398 RVA: 0x00007E0C File Offset: 0x0000600C
		// (remove) Token: 0x0600018F RID: 399 RVA: 0x00007E44 File Offset: 0x00006044
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000190 RID: 400 RVA: 0x00007E7C File Offset: 0x0000607C
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400009F RID: 159
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000A0 RID: 160
		[OptionalField]
		private string DnsNameField;

		// Token: 0x040000A1 RID: 161
		private int OSMajorVersionField;

		// Token: 0x040000A2 RID: 162
		private int OSMinorVersionField;

		// Token: 0x040000A3 RID: 163
		private int OSBuildNumberField;

		// Token: 0x040000A4 RID: 164
		private short OSServicePackMajorNumberField;

		// Token: 0x040000A5 RID: 165
		private short OSServicePackMinorNumberField;

		// Token: 0x040000A6 RID: 166
		[OptionalField]
		private string OSLocaleField;

		// Token: 0x040000A7 RID: 167
		[OptionalField]
		private string ComputerManufacturerField;

		// Token: 0x040000A8 RID: 168
		[OptionalField]
		private string ComputerModelField;

		// Token: 0x040000A9 RID: 169
		[OptionalField]
		private string BiosVersionField;

		// Token: 0x040000AA RID: 170
		[OptionalField]
		private string BiosNameField;

		// Token: 0x040000AB RID: 171
		private DateTime BiosReleaseDateField;

		// Token: 0x040000AC RID: 172
		[OptionalField]
		private string ProcessorArchitectureField;

		// Token: 0x040000AD RID: 173
		private short SuiteMaskField;

		// Token: 0x040000AE RID: 174
		private byte OldProductTypeField;

		// Token: 0x040000AF RID: 175
		private int NewProductTypeField;

		// Token: 0x040000B0 RID: 176
		private int SystemMetricsField;

		// Token: 0x040000B1 RID: 177
		private short ClientVersionMajorNumberField;

		// Token: 0x040000B2 RID: 178
		private short ClientVersionMinorNumberField;

		// Token: 0x040000B3 RID: 179
		private short ClientVersionBuildNumberField;

		// Token: 0x040000B4 RID: 180
		private short ClientVersionQfeNumberField;

		// Token: 0x040000B5 RID: 181
		[OptionalField]
		private string OSDescriptionField;

		// Token: 0x040000B6 RID: 182
		[OptionalField]
		private string OEMField;

		// Token: 0x040000B7 RID: 183
		[OptionalField]
		private string DeviceTypeField;

		// Token: 0x040000B8 RID: 184
		[OptionalField]
		private string FirmwareVersionField;

		// Token: 0x040000B9 RID: 185
		[OptionalField]
		private string MobileOperatorField;
	}
}
