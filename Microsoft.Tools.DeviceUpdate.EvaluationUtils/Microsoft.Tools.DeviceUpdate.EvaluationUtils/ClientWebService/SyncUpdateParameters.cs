using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000022 RID: 34
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SyncUpdateParameters", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class SyncUpdateParameters : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007FA4 File Offset: 0x000061A4
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00007FAC File Offset: 0x000061AC
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007FB5 File Offset: 0x000061B5
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00007FBD File Offset: 0x000061BD
		[DataMember(IsRequired = true)]
		public bool ExpressQuery
		{
			get
			{
				return this.ExpressQueryField;
			}
			set
			{
				if (!this.ExpressQueryField.Equals(value))
				{
					this.ExpressQueryField = value;
					this.RaisePropertyChanged("ExpressQuery");
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007FDF File Offset: 0x000061DF
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00007FE7 File Offset: 0x000061E7
		[DataMember(EmitDefaultValue = false)]
		public ArrayOfInt InstalledNonLeafUpdateIDs
		{
			get
			{
				return this.InstalledNonLeafUpdateIDsField;
			}
			set
			{
				if (this.InstalledNonLeafUpdateIDsField != value)
				{
					this.InstalledNonLeafUpdateIDsField = value;
					this.RaisePropertyChanged("InstalledNonLeafUpdateIDs");
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008004 File Offset: 0x00006204
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000800C File Offset: 0x0000620C
		[DataMember(EmitDefaultValue = false)]
		public ArrayOfInt OtherCachedUpdateIDs
		{
			get
			{
				return this.OtherCachedUpdateIDsField;
			}
			set
			{
				if (this.OtherCachedUpdateIDsField != value)
				{
					this.OtherCachedUpdateIDsField = value;
					this.RaisePropertyChanged("OtherCachedUpdateIDs");
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008029 File Offset: 0x00006229
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00008031 File Offset: 0x00006231
		[DataMember(EmitDefaultValue = false)]
		public Device[] SystemSpec
		{
			get
			{
				return this.SystemSpecField;
			}
			set
			{
				if (this.SystemSpecField != value)
				{
					this.SystemSpecField = value;
					this.RaisePropertyChanged("SystemSpec");
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000804E File Offset: 0x0000624E
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00008056 File Offset: 0x00006256
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public ArrayOfInt CachedDriverIDs
		{
			get
			{
				return this.CachedDriverIDsField;
			}
			set
			{
				if (this.CachedDriverIDsField != value)
				{
					this.CachedDriverIDsField = value;
					this.RaisePropertyChanged("CachedDriverIDs");
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00008073 File Offset: 0x00006273
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000807B File Offset: 0x0000627B
		[DataMember(IsRequired = true, Order = 5)]
		public bool SkipSoftwareSync
		{
			get
			{
				return this.SkipSoftwareSyncField;
			}
			set
			{
				if (!this.SkipSoftwareSyncField.Equals(value))
				{
					this.SkipSoftwareSyncField = value;
					this.RaisePropertyChanged("SkipSoftwareSync");
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000809D File Offset: 0x0000629D
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000080A5 File Offset: 0x000062A5
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public CategoryIdentifier[] FilterCategoryIds
		{
			get
			{
				return this.FilterCategoryIdsField;
			}
			set
			{
				if (this.FilterCategoryIdsField != value)
				{
					this.FilterCategoryIdsField = value;
					this.RaisePropertyChanged("FilterCategoryIds");
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000080C2 File Offset: 0x000062C2
		// (set) Token: 0x060001AE RID: 430 RVA: 0x000080CA File Offset: 0x000062CA
		[DataMember(Order = 7)]
		public bool NeedTwoGroupOutOfScopeUpdates
		{
			get
			{
				return this.NeedTwoGroupOutOfScopeUpdatesField;
			}
			set
			{
				if (!this.NeedTwoGroupOutOfScopeUpdatesField.Equals(value))
				{
					this.NeedTwoGroupOutOfScopeUpdatesField = value;
					this.RaisePropertyChanged("NeedTwoGroupOutOfScopeUpdates");
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000080EC File Offset: 0x000062EC
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x000080F4 File Offset: 0x000062F4
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public ComputerHardwareSpecification ComputerSpec
		{
			get
			{
				return this.ComputerSpecField;
			}
			set
			{
				if (this.ComputerSpecField != value)
				{
					this.ComputerSpecField = value;
					this.RaisePropertyChanged("ComputerSpec");
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008111 File Offset: 0x00006311
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00008119 File Offset: 0x00006319
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public string FeatureScoreMatchingKey
		{
			get
			{
				return this.FeatureScoreMatchingKeyField;
			}
			set
			{
				if (this.FeatureScoreMatchingKeyField != value)
				{
					this.FeatureScoreMatchingKeyField = value;
					this.RaisePropertyChanged("FeatureScoreMatchingKey");
				}
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060001B3 RID: 435 RVA: 0x00008138 File Offset: 0x00006338
		// (remove) Token: 0x060001B4 RID: 436 RVA: 0x00008170 File Offset: 0x00006370
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001B5 RID: 437 RVA: 0x000081A8 File Offset: 0x000063A8
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000BF RID: 191
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000C0 RID: 192
		private bool ExpressQueryField;

		// Token: 0x040000C1 RID: 193
		[OptionalField]
		private ArrayOfInt InstalledNonLeafUpdateIDsField;

		// Token: 0x040000C2 RID: 194
		[OptionalField]
		private ArrayOfInt OtherCachedUpdateIDsField;

		// Token: 0x040000C3 RID: 195
		[OptionalField]
		private Device[] SystemSpecField;

		// Token: 0x040000C4 RID: 196
		[OptionalField]
		private ArrayOfInt CachedDriverIDsField;

		// Token: 0x040000C5 RID: 197
		private bool SkipSoftwareSyncField;

		// Token: 0x040000C6 RID: 198
		[OptionalField]
		private CategoryIdentifier[] FilterCategoryIdsField;

		// Token: 0x040000C7 RID: 199
		[OptionalField]
		private bool NeedTwoGroupOutOfScopeUpdatesField;

		// Token: 0x040000C8 RID: 200
		[OptionalField]
		private ComputerHardwareSpecification ComputerSpecField;

		// Token: 0x040000C9 RID: 201
		[OptionalField]
		private string FeatureScoreMatchingKeyField;
	}
}
