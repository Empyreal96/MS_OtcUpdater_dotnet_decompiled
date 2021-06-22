using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000025 RID: 37
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "CategoryIdentifier", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class CategoryIdentifier : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000083AC File Offset: 0x000065AC
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000083B4 File Offset: 0x000065B4
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000083BD File Offset: 0x000065BD
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000083C5 File Offset: 0x000065C5
		[DataMember(IsRequired = true)]
		public Guid Id
		{
			get
			{
				return this.IdField;
			}
			set
			{
				if (!this.IdField.Equals(value))
				{
					this.IdField = value;
					this.RaisePropertyChanged("Id");
				}
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060001CF RID: 463 RVA: 0x000083E8 File Offset: 0x000065E8
		// (remove) Token: 0x060001D0 RID: 464 RVA: 0x00008420 File Offset: 0x00006620
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001D1 RID: 465 RVA: 0x00008458 File Offset: 0x00006658
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000D3 RID: 211
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000D4 RID: 212
		private Guid IdField;
	}
}
