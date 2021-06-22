using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200002F RID: 47
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ExtendedUpdateInfo", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class ExtendedUpdateInfo : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00008E24 File Offset: 0x00007024
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00008E2C File Offset: 0x0000702C
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00008E35 File Offset: 0x00007035
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00008E3D File Offset: 0x0000703D
		[DataMember(EmitDefaultValue = false)]
		public ArrayOfUpdateData Updates
		{
			get
			{
				return this.UpdatesField;
			}
			set
			{
				if (this.UpdatesField != value)
				{
					this.UpdatesField = value;
					this.RaisePropertyChanged("Updates");
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00008E5A File Offset: 0x0000705A
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00008E62 File Offset: 0x00007062
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public FileLocation[] FileLocations
		{
			get
			{
				return this.FileLocationsField;
			}
			set
			{
				if (this.FileLocationsField != value)
				{
					this.FileLocationsField = value;
					this.RaisePropertyChanged("FileLocations");
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00008E7F File Offset: 0x0000707F
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00008E87 File Offset: 0x00007087
		[DataMember(EmitDefaultValue = false, Order = 2)]
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

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600024A RID: 586 RVA: 0x00008EA4 File Offset: 0x000070A4
		// (remove) Token: 0x0600024B RID: 587 RVA: 0x00008EDC File Offset: 0x000070DC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600024C RID: 588 RVA: 0x00008F14 File Offset: 0x00007114
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000117 RID: 279
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000118 RID: 280
		[OptionalField]
		private ArrayOfUpdateData UpdatesField;

		// Token: 0x04000119 RID: 281
		[OptionalField]
		private FileLocation[] FileLocationsField;

		// Token: 0x0400011A RID: 282
		[OptionalField]
		private ArrayOfInt OutOfScopeRevisionIDsField;
	}
}
