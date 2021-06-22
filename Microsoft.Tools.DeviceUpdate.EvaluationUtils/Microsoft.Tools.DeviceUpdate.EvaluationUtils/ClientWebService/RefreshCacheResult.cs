using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200002D RID: 45
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RefreshCacheResult", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class RefreshCacheResult : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008CE0 File Offset: 0x00006EE0
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00008CE8 File Offset: 0x00006EE8
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00008CF1 File Offset: 0x00006EF1
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00008CF9 File Offset: 0x00006EF9
		[DataMember(IsRequired = true)]
		public int RevisionID
		{
			get
			{
				return this.RevisionIDField;
			}
			set
			{
				if (!this.RevisionIDField.Equals(value))
				{
					this.RevisionIDField = value;
					this.RaisePropertyChanged("RevisionID");
				}
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008D1B File Offset: 0x00006F1B
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00008D23 File Offset: 0x00006F23
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public UpdateIdentity GlobalID
		{
			get
			{
				return this.GlobalIDField;
			}
			set
			{
				if (this.GlobalIDField != value)
				{
					this.GlobalIDField = value;
					this.RaisePropertyChanged("GlobalID");
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00008D40 File Offset: 0x00006F40
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00008D48 File Offset: 0x00006F48
		[DataMember(IsRequired = true, Order = 2)]
		public bool IsLeaf
		{
			get
			{
				return this.IsLeafField;
			}
			set
			{
				if (!this.IsLeafField.Equals(value))
				{
					this.IsLeafField = value;
					this.RaisePropertyChanged("IsLeaf");
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008D6A File Offset: 0x00006F6A
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00008D72 File Offset: 0x00006F72
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public Deployment Deployment
		{
			get
			{
				return this.DeploymentField;
			}
			set
			{
				if (this.DeploymentField != value)
				{
					this.DeploymentField = value;
					this.RaisePropertyChanged("Deployment");
				}
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600023E RID: 574 RVA: 0x00008D90 File Offset: 0x00006F90
		// (remove) Token: 0x0600023F RID: 575 RVA: 0x00008DC8 File Offset: 0x00006FC8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000240 RID: 576 RVA: 0x00008E00 File Offset: 0x00007000
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000108 RID: 264
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000109 RID: 265
		private int RevisionIDField;

		// Token: 0x0400010A RID: 266
		[OptionalField]
		private UpdateIdentity GlobalIDField;

		// Token: 0x0400010B RID: 267
		private bool IsLeafField;

		// Token: 0x0400010C RID: 268
		[OptionalField]
		private Deployment DeploymentField;
	}
}
