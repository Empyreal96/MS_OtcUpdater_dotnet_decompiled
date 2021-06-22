using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000035 RID: 53
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "FileDecryption", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class FileDecryption : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000093A8 File Offset: 0x000075A8
		// (set) Token: 0x06000280 RID: 640 RVA: 0x000093B0 File Offset: 0x000075B0
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000281 RID: 641 RVA: 0x000093B9 File Offset: 0x000075B9
		// (set) Token: 0x06000282 RID: 642 RVA: 0x000093C1 File Offset: 0x000075C1
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000283 RID: 643 RVA: 0x000093DE File Offset: 0x000075DE
		// (set) Token: 0x06000284 RID: 644 RVA: 0x000093E6 File Offset: 0x000075E6
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public byte[] DecryptionKey
		{
			get
			{
				return this.DecryptionKeyField;
			}
			set
			{
				if (this.DecryptionKeyField != value)
				{
					this.DecryptionKeyField = value;
					this.RaisePropertyChanged("DecryptionKey");
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009403 File Offset: 0x00007603
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000940B File Offset: 0x0000760B
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public ArrayOfBase64Binary SecurityData
		{
			get
			{
				return this.SecurityDataField;
			}
			set
			{
				if (this.SecurityDataField != value)
				{
					this.SecurityDataField = value;
					this.RaisePropertyChanged("SecurityData");
				}
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000287 RID: 647 RVA: 0x00009428 File Offset: 0x00007628
		// (remove) Token: 0x06000288 RID: 648 RVA: 0x00009460 File Offset: 0x00007660
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000289 RID: 649 RVA: 0x00009498 File Offset: 0x00007698
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000130 RID: 304
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000131 RID: 305
		[OptionalField]
		private byte[] FileDigestField;

		// Token: 0x04000132 RID: 306
		[OptionalField]
		private byte[] DecryptionKeyField;

		// Token: 0x04000133 RID: 307
		[OptionalField]
		private ArrayOfBase64Binary SecurityDataField;
	}
}
