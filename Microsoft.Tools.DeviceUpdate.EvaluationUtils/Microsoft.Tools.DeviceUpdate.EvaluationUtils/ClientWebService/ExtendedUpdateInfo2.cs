using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000033 RID: 51
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ExtendedUpdateInfo2", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class ExtendedUpdateInfo2 : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00009124 File Offset: 0x00007324
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000912C File Offset: 0x0000732C
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00009135 File Offset: 0x00007335
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000913D File Offset: 0x0000733D
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000915A File Offset: 0x0000735A
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00009162 File Offset: 0x00007362
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000917F File Offset: 0x0000737F
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00009187 File Offset: 0x00007387
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public UpdateReportingSampleRate[] UpdateReportingSamplingRates
		{
			get
			{
				return this.UpdateReportingSamplingRatesField;
			}
			set
			{
				if (this.UpdateReportingSamplingRatesField != value)
				{
					this.UpdateReportingSamplingRatesField = value;
					this.RaisePropertyChanged("UpdateReportingSamplingRates");
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000091A4 File Offset: 0x000073A4
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000091AC File Offset: 0x000073AC
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public FileDecryption[] FileDecryptionData
		{
			get
			{
				return this.FileDecryptionDataField;
			}
			set
			{
				if (this.FileDecryptionDataField != value)
				{
					this.FileDecryptionDataField = value;
					this.RaisePropertyChanged("FileDecryptionData");
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000091C9 File Offset: 0x000073C9
		// (set) Token: 0x0600026E RID: 622 RVA: 0x000091D1 File Offset: 0x000073D1
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public FileDecryption2[] FileDecryptionData2
		{
			get
			{
				return this.FileDecryptionData2Field;
			}
			set
			{
				if (this.FileDecryptionData2Field != value)
				{
					this.FileDecryptionData2Field = value;
					this.RaisePropertyChanged("FileDecryptionData2");
				}
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600026F RID: 623 RVA: 0x000091F0 File Offset: 0x000073F0
		// (remove) Token: 0x06000270 RID: 624 RVA: 0x00009228 File Offset: 0x00007428
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000271 RID: 625 RVA: 0x00009260 File Offset: 0x00007460
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000124 RID: 292
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000125 RID: 293
		[OptionalField]
		private ArrayOfUpdateData UpdatesField;

		// Token: 0x04000126 RID: 294
		[OptionalField]
		private FileLocation[] FileLocationsField;

		// Token: 0x04000127 RID: 295
		[OptionalField]
		private UpdateReportingSampleRate[] UpdateReportingSamplingRatesField;

		// Token: 0x04000128 RID: 296
		[OptionalField]
		private FileDecryption[] FileDecryptionDataField;

		// Token: 0x04000129 RID: 297
		[OptionalField]
		private FileDecryption2[] FileDecryptionData2Field;
	}
}
