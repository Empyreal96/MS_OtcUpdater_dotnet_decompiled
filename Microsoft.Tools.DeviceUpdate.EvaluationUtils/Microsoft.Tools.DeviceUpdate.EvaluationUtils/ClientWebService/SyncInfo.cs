using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000028 RID: 40
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "SyncInfo", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class SyncInfo : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008698 File Offset: 0x00006898
		// (set) Token: 0x060001ED RID: 493 RVA: 0x000086A0 File Offset: 0x000068A0
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000086A9 File Offset: 0x000068A9
		// (set) Token: 0x060001EF RID: 495 RVA: 0x000086B1 File Offset: 0x000068B1
		[DataMember(EmitDefaultValue = false)]
		public UpdateInfo[] NewUpdates
		{
			get
			{
				return this.NewUpdatesField;
			}
			set
			{
				if (this.NewUpdatesField != value)
				{
					this.NewUpdatesField = value;
					this.RaisePropertyChanged("NewUpdates");
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000086CE File Offset: 0x000068CE
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000086D6 File Offset: 0x000068D6
		[DataMember(EmitDefaultValue = false)]
		public ArrayOfInt OutOfScopeRevisionIDs
		{
			get
			{
				return this.OutOfScopeRevisionIDsField;
			}
			set
			{
				if (this.OutOfScopeRevisionIDsField != value)
				{
					this.OutOfScopeRevisionIDsField = value;
					this.RaisePropertyChanged("OutOfScopeRevisionIDs");
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000086F3 File Offset: 0x000068F3
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x000086FB File Offset: 0x000068FB
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public UpdateInfo[] ChangedUpdates
		{
			get
			{
				return this.ChangedUpdatesField;
			}
			set
			{
				if (this.ChangedUpdatesField != value)
				{
					this.ChangedUpdatesField = value;
					this.RaisePropertyChanged("ChangedUpdates");
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008718 File Offset: 0x00006918
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00008720 File Offset: 0x00006920
		[DataMember(IsRequired = true, Order = 3)]
		public bool Truncated
		{
			get
			{
				return this.TruncatedField;
			}
			set
			{
				if (!this.TruncatedField.Equals(value))
				{
					this.TruncatedField = value;
					this.RaisePropertyChanged("Truncated");
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00008742 File Offset: 0x00006942
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000874A File Offset: 0x0000694A
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public Cookie NewCookie
		{
			get
			{
				return this.NewCookieField;
			}
			set
			{
				if (this.NewCookieField != value)
				{
					this.NewCookieField = value;
					this.RaisePropertyChanged("NewCookie");
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00008767 File Offset: 0x00006967
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000876F File Offset: 0x0000696F
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public ArrayOfInt DeployedOutOfScopeRevisionIds
		{
			get
			{
				return this.DeployedOutOfScopeRevisionIdsField;
			}
			set
			{
				if (this.DeployedOutOfScopeRevisionIdsField != value)
				{
					this.DeployedOutOfScopeRevisionIdsField = value;
					this.RaisePropertyChanged("DeployedOutOfScopeRevisionIds");
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000878C File Offset: 0x0000698C
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00008794 File Offset: 0x00006994
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string DriverSyncNotNeeded
		{
			get
			{
				return this.DriverSyncNotNeededField;
			}
			set
			{
				if (this.DriverSyncNotNeededField != value)
				{
					this.DriverSyncNotNeededField = value;
					this.RaisePropertyChanged("DriverSyncNotNeeded");
				}
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060001FC RID: 508 RVA: 0x000087B4 File Offset: 0x000069B4
		// (remove) Token: 0x060001FD RID: 509 RVA: 0x000087EC File Offset: 0x000069EC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001FE RID: 510 RVA: 0x00008824 File Offset: 0x00006A24
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000E1 RID: 225
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000E2 RID: 226
		[OptionalField]
		private UpdateInfo[] NewUpdatesField;

		// Token: 0x040000E3 RID: 227
		[OptionalField]
		private ArrayOfInt OutOfScopeRevisionIDsField;

		// Token: 0x040000E4 RID: 228
		[OptionalField]
		private UpdateInfo[] ChangedUpdatesField;

		// Token: 0x040000E5 RID: 229
		private bool TruncatedField;

		// Token: 0x040000E6 RID: 230
		[OptionalField]
		private Cookie NewCookieField;

		// Token: 0x040000E7 RID: 231
		[OptionalField]
		private ArrayOfInt DeployedOutOfScopeRevisionIdsField;

		// Token: 0x040000E8 RID: 232
		[OptionalField]
		private string DriverSyncNotNeededField;
	}
}
