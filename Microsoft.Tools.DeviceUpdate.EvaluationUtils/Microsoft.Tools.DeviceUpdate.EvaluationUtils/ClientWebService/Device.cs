using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000024 RID: 36
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Device", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class Device : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00008298 File Offset: 0x00006498
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000082A0 File Offset: 0x000064A0
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000082A9 File Offset: 0x000064A9
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000082B1 File Offset: 0x000064B1
		[DataMember(EmitDefaultValue = false)]
		public ArrayOfString HardwareIDs
		{
			get
			{
				return this.HardwareIDsField;
			}
			set
			{
				if (this.HardwareIDsField != value)
				{
					this.HardwareIDsField = value;
					this.RaisePropertyChanged("HardwareIDs");
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000082CE File Offset: 0x000064CE
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x000082D6 File Offset: 0x000064D6
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public ArrayOfString CompatibleIDs
		{
			get
			{
				return this.CompatibleIDsField;
			}
			set
			{
				if (this.CompatibleIDsField != value)
				{
					this.CompatibleIDsField = value;
					this.RaisePropertyChanged("CompatibleIDs");
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000082F3 File Offset: 0x000064F3
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000082FB File Offset: 0x000064FB
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public InstalledDriver installedDriver
		{
			get
			{
				return this.installedDriverField;
			}
			set
			{
				if (this.installedDriverField != value)
				{
					this.installedDriverField = value;
					this.RaisePropertyChanged("installedDriver");
				}
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060001C7 RID: 455 RVA: 0x00008318 File Offset: 0x00006518
		// (remove) Token: 0x060001C8 RID: 456 RVA: 0x00008350 File Offset: 0x00006550
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001C9 RID: 457 RVA: 0x00008388 File Offset: 0x00006588
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000CF RID: 207
		[OptionalField]
		private ArrayOfString HardwareIDsField;

		// Token: 0x040000D0 RID: 208
		[OptionalField]
		private ArrayOfString CompatibleIDsField;

		// Token: 0x040000D1 RID: 209
		[OptionalField]
		private InstalledDriver installedDriverField;
	}
}
