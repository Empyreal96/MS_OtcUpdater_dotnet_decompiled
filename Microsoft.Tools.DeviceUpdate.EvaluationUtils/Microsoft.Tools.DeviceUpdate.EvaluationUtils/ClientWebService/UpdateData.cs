using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000032 RID: 50
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateData", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class UpdateData : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00009030 File Offset: 0x00007230
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00009038 File Offset: 0x00007238
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00009041 File Offset: 0x00007241
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00009049 File Offset: 0x00007249
		[DataMember(IsRequired = true)]
		public int ID
		{
			get
			{
				return this.IDField;
			}
			set
			{
				if (!this.IDField.Equals(value))
				{
					this.IDField = value;
					this.RaisePropertyChanged("ID");
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000906B File Offset: 0x0000726B
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00009073 File Offset: 0x00007273
		[DataMember(EmitDefaultValue = false)]
		public string Xml
		{
			get
			{
				return this.XmlField;
			}
			set
			{
				if (this.XmlField != value)
				{
					this.XmlField = value;
					this.RaisePropertyChanged("Xml");
				}
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600025F RID: 607 RVA: 0x00009090 File Offset: 0x00007290
		// (remove) Token: 0x06000260 RID: 608 RVA: 0x000090C8 File Offset: 0x000072C8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000261 RID: 609 RVA: 0x00009100 File Offset: 0x00007300
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000120 RID: 288
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000121 RID: 289
		private int IDField;

		// Token: 0x04000122 RID: 290
		[OptionalField]
		private string XmlField;
	}
}
