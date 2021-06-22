using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000034 RID: 52
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateReportingSampleRate", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class UpdateReportingSampleRate : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00009284 File Offset: 0x00007484
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000928C File Offset: 0x0000748C
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009295 File Offset: 0x00007495
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000929D File Offset: 0x0000749D
		[DataMember(IsRequired = true)]
		public Guid UpdateId
		{
			get
			{
				return this.UpdateIdField;
			}
			set
			{
				if (!this.UpdateIdField.Equals(value))
				{
					this.UpdateIdField = value;
					this.RaisePropertyChanged("UpdateId");
				}
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000277 RID: 631 RVA: 0x000092BF File Offset: 0x000074BF
		// (set) Token: 0x06000278 RID: 632 RVA: 0x000092C7 File Offset: 0x000074C7
		[DataMember(IsRequired = true, Order = 1)]
		public int SamplingRateForInstall
		{
			get
			{
				return this.SamplingRateForInstallField;
			}
			set
			{
				if (!this.SamplingRateForInstallField.Equals(value))
				{
					this.SamplingRateForInstallField = value;
					this.RaisePropertyChanged("SamplingRateForInstall");
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000092E9 File Offset: 0x000074E9
		// (set) Token: 0x0600027A RID: 634 RVA: 0x000092F1 File Offset: 0x000074F1
		[DataMember(IsRequired = true, Order = 2)]
		public int SamplingRateForDownload
		{
			get
			{
				return this.SamplingRateForDownloadField;
			}
			set
			{
				if (!this.SamplingRateForDownloadField.Equals(value))
				{
					this.SamplingRateForDownloadField = value;
					this.RaisePropertyChanged("SamplingRateForDownload");
				}
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600027B RID: 635 RVA: 0x00009314 File Offset: 0x00007514
		// (remove) Token: 0x0600027C RID: 636 RVA: 0x0000934C File Offset: 0x0000754C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600027D RID: 637 RVA: 0x00009384 File Offset: 0x00007584
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400012B RID: 299
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400012C RID: 300
		private Guid UpdateIdField;

		// Token: 0x0400012D RID: 301
		private int SamplingRateForInstallField;

		// Token: 0x0400012E RID: 302
		private int SamplingRateForDownloadField;
	}
}
