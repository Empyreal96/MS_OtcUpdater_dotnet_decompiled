using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000036 RID: 54
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "FileDecryption2", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class FileDecryption2 : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000094BC File Offset: 0x000076BC
		// (set) Token: 0x0600028C RID: 652 RVA: 0x000094C4 File Offset: 0x000076C4
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000094CD File Offset: 0x000076CD
		// (set) Token: 0x0600028E RID: 654 RVA: 0x000094D5 File Offset: 0x000076D5
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000094F2 File Offset: 0x000076F2
		// (set) Token: 0x06000290 RID: 656 RVA: 0x000094FA File Offset: 0x000076FA
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009517 File Offset: 0x00007717
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000951F File Offset: 0x0000771F
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

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000293 RID: 659 RVA: 0x0000953C File Offset: 0x0000773C
		// (remove) Token: 0x06000294 RID: 660 RVA: 0x00009574 File Offset: 0x00007774
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000295 RID: 661 RVA: 0x000095AC File Offset: 0x000077AC
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000135 RID: 309
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000136 RID: 310
		[OptionalField]
		private byte[] FileDigestField;

		// Token: 0x04000137 RID: 311
		[OptionalField]
		private byte[] DecryptionKeyField;

		// Token: 0x04000138 RID: 312
		[OptionalField]
		private ArrayOfBase64Binary SecurityDataField;
	}
}
