using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200002A RID: 42
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Deployment", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class Deployment : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000898C File Offset: 0x00006B8C
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00008994 File Offset: 0x00006B94
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000899D File Offset: 0x00006B9D
		// (set) Token: 0x06000211 RID: 529 RVA: 0x000089A5 File Offset: 0x00006BA5
		[DataMember(IsRequired = true)]
		public int ID
		{
			get
			{
				return this.IDField;
			}
			set
			{
				if (!this.IDField.Equals(value))
				{
					this.IDField = value;
					this.RaisePropertyChanged("ID");
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000089C7 File Offset: 0x00006BC7
		// (set) Token: 0x06000213 RID: 531 RVA: 0x000089CF File Offset: 0x00006BCF
		[DataMember(IsRequired = true, Order = 1)]
		public DeploymentAction Action
		{
			get
			{
				return this.ActionField;
			}
			set
			{
				if (!this.ActionField.Equals(value))
				{
					this.ActionField = value;
					this.RaisePropertyChanged("Action");
				}
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000089FC File Offset: 0x00006BFC
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00008A04 File Offset: 0x00006C04
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string Deadline
		{
			get
			{
				return this.DeadlineField;
			}
			set
			{
				if (this.DeadlineField != value)
				{
					this.DeadlineField = value;
					this.RaisePropertyChanged("Deadline");
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00008A21 File Offset: 0x00006C21
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00008A29 File Offset: 0x00006C29
		[DataMember(IsRequired = true, Order = 3)]
		public bool IsAssigned
		{
			get
			{
				return this.IsAssignedField;
			}
			set
			{
				if (!this.IsAssignedField.Equals(value))
				{
					this.IsAssignedField = value;
					this.RaisePropertyChanged("IsAssigned");
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00008A4B File Offset: 0x00006C4B
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00008A53 File Offset: 0x00006C53
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string LastChangeTime
		{
			get
			{
				return this.LastChangeTimeField;
			}
			set
			{
				if (this.LastChangeTimeField != value)
				{
					this.LastChangeTimeField = value;
					this.RaisePropertyChanged("LastChangeTime");
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00008A70 File Offset: 0x00006C70
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00008A78 File Offset: 0x00006C78
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public string DownloadPriority
		{
			get
			{
				return this.DownloadPriorityField;
			}
			set
			{
				if (this.DownloadPriorityField != value)
				{
					this.DownloadPriorityField = value;
					this.RaisePropertyChanged("DownloadPriority");
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008A95 File Offset: 0x00006C95
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00008A9D File Offset: 0x00006C9D
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public ArrayOfString HardwareIds
		{
			get
			{
				return this.HardwareIdsField;
			}
			set
			{
				if (this.HardwareIdsField != value)
				{
					this.HardwareIdsField = value;
					this.RaisePropertyChanged("HardwareIds");
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008ABA File Offset: 0x00006CBA
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00008AC2 File Offset: 0x00006CC2
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public string AutoSelect
		{
			get
			{
				return this.AutoSelectField;
			}
			set
			{
				if (this.AutoSelectField != value)
				{
					this.AutoSelectField = value;
					this.RaisePropertyChanged("AutoSelect");
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00008ADF File Offset: 0x00006CDF
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00008AE7 File Offset: 0x00006CE7
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public string AutoDownload
		{
			get
			{
				return this.AutoDownloadField;
			}
			set
			{
				if (this.AutoDownloadField != value)
				{
					this.AutoDownloadField = value;
					this.RaisePropertyChanged("AutoDownload");
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008B04 File Offset: 0x00006D04
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00008B0C File Offset: 0x00006D0C
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public string SupersedenceBehavior
		{
			get
			{
				return this.SupersedenceBehaviorField;
			}
			set
			{
				if (this.SupersedenceBehaviorField != value)
				{
					this.SupersedenceBehaviorField = value;
					this.RaisePropertyChanged("SupersedenceBehavior");
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008B29 File Offset: 0x00006D29
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00008B31 File Offset: 0x00006D31
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public string FlagBitmask
		{
			get
			{
				return this.FlagBitmaskField;
			}
			set
			{
				if (this.FlagBitmaskField != value)
				{
					this.FlagBitmaskField = value;
					this.RaisePropertyChanged("FlagBitmask");
				}
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000226 RID: 550 RVA: 0x00008B50 File Offset: 0x00006D50
		// (remove) Token: 0x06000227 RID: 551 RVA: 0x00008B88 File Offset: 0x00006D88
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000228 RID: 552 RVA: 0x00008BC0 File Offset: 0x00006DC0
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000F0 RID: 240
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000F1 RID: 241
		private int IDField;

		// Token: 0x040000F2 RID: 242
		private DeploymentAction ActionField;

		// Token: 0x040000F3 RID: 243
		[OptionalField]
		private string DeadlineField;

		// Token: 0x040000F4 RID: 244
		private bool IsAssignedField;

		// Token: 0x040000F5 RID: 245
		[OptionalField]
		private string LastChangeTimeField;

		// Token: 0x040000F6 RID: 246
		[OptionalField]
		private string DownloadPriorityField;

		// Token: 0x040000F7 RID: 247
		[OptionalField]
		private ArrayOfString HardwareIdsField;

		// Token: 0x040000F8 RID: 248
		[OptionalField]
		private string AutoSelectField;

		// Token: 0x040000F9 RID: 249
		[OptionalField]
		private string AutoDownloadField;

		// Token: 0x040000FA RID: 250
		[OptionalField]
		private string SupersedenceBehaviorField;

		// Token: 0x040000FB RID: 251
		[OptionalField]
		private string FlagBitmaskField;
	}
}
