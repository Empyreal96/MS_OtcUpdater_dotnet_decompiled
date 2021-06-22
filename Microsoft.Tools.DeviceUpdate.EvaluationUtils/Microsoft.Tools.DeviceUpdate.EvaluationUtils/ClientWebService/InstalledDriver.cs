using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000026 RID: 38
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "InstalledDriver", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class InstalledDriver : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000847C File Offset: 0x0000667C
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00008484 File Offset: 0x00006684
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000848D File Offset: 0x0000668D
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00008495 File Offset: 0x00006695
		[DataMember(EmitDefaultValue = false)]
		public string MatchingID
		{
			get
			{
				return this.MatchingIDField;
			}
			set
			{
				if (this.MatchingIDField != value)
				{
					this.MatchingIDField = value;
					this.RaisePropertyChanged("MatchingID");
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000084B2 File Offset: 0x000066B2
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000084BA File Offset: 0x000066BA
		[DataMember(IsRequired = true, Order = 1)]
		public DateTime DriverVerDate
		{
			get
			{
				return this.DriverVerDateField;
			}
			set
			{
				if (!this.DriverVerDateField.Equals(value))
				{
					this.DriverVerDateField = value;
					this.RaisePropertyChanged("DriverVerDate");
				}
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000084DC File Offset: 0x000066DC
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000084E4 File Offset: 0x000066E4
		[DataMember(IsRequired = true, Order = 2)]
		public long DriverVerVersion
		{
			get
			{
				return this.DriverVerVersionField;
			}
			set
			{
				if (!this.DriverVerVersionField.Equals(value))
				{
					this.DriverVerVersionField = value;
					this.RaisePropertyChanged("DriverVerVersion");
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00008506 File Offset: 0x00006706
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000850E File Offset: 0x0000670E
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string Class
		{
			get
			{
				return this.ClassField;
			}
			set
			{
				if (this.ClassField != value)
				{
					this.ClassField = value;
					this.RaisePropertyChanged("Class");
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000852B File Offset: 0x0000672B
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00008533 File Offset: 0x00006733
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string Manufacturer
		{
			get
			{
				return this.ManufacturerField;
			}
			set
			{
				if (this.ManufacturerField != value)
				{
					this.ManufacturerField = value;
					this.RaisePropertyChanged("Manufacturer");
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00008550 File Offset: 0x00006750
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00008558 File Offset: 0x00006758
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public string Provider
		{
			get
			{
				return this.ProviderField;
			}
			set
			{
				if (this.ProviderField != value)
				{
					this.ProviderField = value;
					this.RaisePropertyChanged("Provider");
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008575 File Offset: 0x00006775
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000857D File Offset: 0x0000677D
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string Model
		{
			get
			{
				return this.ModelField;
			}
			set
			{
				if (this.ModelField != value)
				{
					this.ModelField = value;
					this.RaisePropertyChanged("Model");
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000859A File Offset: 0x0000679A
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000085A2 File Offset: 0x000067A2
		[DataMember(Order = 7)]
		public Guid? MatchingComputerHWID
		{
			get
			{
				return this.MatchingComputerHWIDField;
			}
			set
			{
				if (!this.MatchingComputerHWIDField.Equals(value))
				{
					this.MatchingComputerHWIDField = value;
					this.RaisePropertyChanged("MatchingComputerHWID");
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000085CF File Offset: 0x000067CF
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000085D7 File Offset: 0x000067D7
		[DataMember(IsRequired = true, Order = 8)]
		public int DriverRank
		{
			get
			{
				return this.DriverRankField;
			}
			set
			{
				if (!this.DriverRankField.Equals(value))
				{
					this.DriverRankField = value;
					this.RaisePropertyChanged("DriverRank");
				}
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060001E7 RID: 487 RVA: 0x000085FC File Offset: 0x000067FC
		// (remove) Token: 0x060001E8 RID: 488 RVA: 0x00008634 File Offset: 0x00006834
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001E9 RID: 489 RVA: 0x0000866C File Offset: 0x0000686C
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000D6 RID: 214
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000D7 RID: 215
		[OptionalField]
		private string MatchingIDField;

		// Token: 0x040000D8 RID: 216
		private DateTime DriverVerDateField;

		// Token: 0x040000D9 RID: 217
		private long DriverVerVersionField;

		// Token: 0x040000DA RID: 218
		[OptionalField]
		private string ClassField;

		// Token: 0x040000DB RID: 219
		[OptionalField]
		private string ManufacturerField;

		// Token: 0x040000DC RID: 220
		[OptionalField]
		private string ProviderField;

		// Token: 0x040000DD RID: 221
		[OptionalField]
		private string ModelField;

		// Token: 0x040000DE RID: 222
		[OptionalField]
		private Guid? MatchingComputerHWIDField;

		// Token: 0x040000DF RID: 223
		private int DriverRankField;
	}
}
