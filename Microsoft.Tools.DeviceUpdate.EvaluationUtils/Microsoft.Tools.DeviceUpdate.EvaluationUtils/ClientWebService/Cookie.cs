using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200001E RID: 30
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Cookie", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class Cookie : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000078FC File Offset: 0x00005AFC
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00007904 File Offset: 0x00005B04
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000790D File Offset: 0x00005B0D
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00007915 File Offset: 0x00005B15
		[DataMember(IsRequired = true)]
		public DateTime Expiration
		{
			get
			{
				return this.ExpirationField;
			}
			set
			{
				if (!this.ExpirationField.Equals(value))
				{
					this.ExpirationField = value;
					this.RaisePropertyChanged("Expiration");
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00007937 File Offset: 0x00005B37
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000793F File Offset: 0x00005B3F
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public byte[] EncryptedData
		{
			get
			{
				return this.EncryptedDataField;
			}
			set
			{
				if (this.EncryptedDataField != value)
				{
					this.EncryptedDataField = value;
					this.RaisePropertyChanged("EncryptedData");
				}
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000154 RID: 340 RVA: 0x0000795C File Offset: 0x00005B5C
		// (remove) Token: 0x06000155 RID: 341 RVA: 0x00007994 File Offset: 0x00005B94
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000156 RID: 342 RVA: 0x000079CC File Offset: 0x00005BCC
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400009B RID: 155
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400009C RID: 156
		private DateTime ExpirationField;

		// Token: 0x0400009D RID: 157
		[OptionalField]
		private byte[] EncryptedDataField;
	}
}
