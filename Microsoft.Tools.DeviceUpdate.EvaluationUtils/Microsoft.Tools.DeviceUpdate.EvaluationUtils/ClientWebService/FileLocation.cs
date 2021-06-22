using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000031 RID: 49
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "FileLocation", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class FileLocation : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00008F40 File Offset: 0x00007140
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00008F48 File Offset: 0x00007148
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00008F51 File Offset: 0x00007151
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00008F59 File Offset: 0x00007159
		[DataMember(EmitDefaultValue = false)]
		public byte[] FileDigest
		{
			get
			{
				return this.FileDigestField;
			}
			set
			{
				if (this.FileDigestField != value)
				{
					this.FileDigestField = value;
					this.RaisePropertyChanged("FileDigest");
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00008F76 File Offset: 0x00007176
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00008F7E File Offset: 0x0000717E
		[DataMember(EmitDefaultValue = false)]
		public string Url
		{
			get
			{
				return this.UrlField;
			}
			set
			{
				if (this.UrlField != value)
				{
					this.UrlField = value;
					this.RaisePropertyChanged("Url");
				}
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000255 RID: 597 RVA: 0x00008F9C File Offset: 0x0000719C
		// (remove) Token: 0x06000256 RID: 598 RVA: 0x00008FD4 File Offset: 0x000071D4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000257 RID: 599 RVA: 0x0000900C File Offset: 0x0000720C
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400011C RID: 284
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400011D RID: 285
		[OptionalField]
		private byte[] FileDigestField;

		// Token: 0x0400011E RID: 286
		[OptionalField]
		private string UrlField;
	}
}
