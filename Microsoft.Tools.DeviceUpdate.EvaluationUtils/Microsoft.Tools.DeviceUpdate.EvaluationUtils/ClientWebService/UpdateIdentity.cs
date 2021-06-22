using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200002C RID: 44
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateIdentity", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class UpdateIdentity : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00008BE4 File Offset: 0x00006DE4
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00008BEC File Offset: 0x00006DEC
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00008BF5 File Offset: 0x00006DF5
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00008BFD File Offset: 0x00006DFD
		[DataMember(IsRequired = true)]
		public Guid UpdateID
		{
			get
			{
				return this.UpdateIDField;
			}
			set
			{
				if (!this.UpdateIDField.Equals(value))
				{
					this.UpdateIDField = value;
					this.RaisePropertyChanged("UpdateID");
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008C1F File Offset: 0x00006E1F
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00008C27 File Offset: 0x00006E27
		[DataMember(IsRequired = true, Order = 1)]
		public int RevisionNumber
		{
			get
			{
				return this.RevisionNumberField;
			}
			set
			{
				if (!this.RevisionNumberField.Equals(value))
				{
					this.RevisionNumberField = value;
					this.RaisePropertyChanged("RevisionNumber");
				}
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000230 RID: 560 RVA: 0x00008C4C File Offset: 0x00006E4C
		// (remove) Token: 0x06000231 RID: 561 RVA: 0x00008C84 File Offset: 0x00006E84
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000232 RID: 562 RVA: 0x00008CBC File Offset: 0x00006EBC
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000104 RID: 260
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000105 RID: 261
		private Guid UpdateIDField;

		// Token: 0x04000106 RID: 262
		private int RevisionNumberField;
	}
}
