using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000023 RID: 35
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ComputerHardwareSpecification", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class ComputerHardwareSpecification : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000081CC File Offset: 0x000063CC
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000081D4 File Offset: 0x000063D4
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000081DD File Offset: 0x000063DD
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000081E5 File Offset: 0x000063E5
		[DataMember(EmitDefaultValue = false)]
		public ArrayOfGuid HardwareIDs
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

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060001BB RID: 443 RVA: 0x00008204 File Offset: 0x00006404
		// (remove) Token: 0x060001BC RID: 444 RVA: 0x0000823C File Offset: 0x0000643C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001BD RID: 445 RVA: 0x00008274 File Offset: 0x00006474
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000CB RID: 203
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000CC RID: 204
		[OptionalField]
		private ArrayOfGuid HardwareIDsField;
	}
}
