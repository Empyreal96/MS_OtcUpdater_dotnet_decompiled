using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000020 RID: 32
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "CategoryRelationship", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class CategoryRelationship : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007EA0 File Offset: 0x000060A0
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00007EA8 File Offset: 0x000060A8
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007EB1 File Offset: 0x000060B1
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00007EB9 File Offset: 0x000060B9
		[DataMember(IsRequired = true)]
		public int IndexOfAndGroup
		{
			get
			{
				return this.IndexOfAndGroupField;
			}
			set
			{
				if (!this.IndexOfAndGroupField.Equals(value))
				{
					this.IndexOfAndGroupField = value;
					this.RaisePropertyChanged("IndexOfAndGroup");
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007EDB File Offset: 0x000060DB
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00007EE3 File Offset: 0x000060E3
		[DataMember(IsRequired = true, Order = 1)]
		public Guid CategoryId
		{
			get
			{
				return this.CategoryIdField;
			}
			set
			{
				if (!this.CategoryIdField.Equals(value))
				{
					this.CategoryIdField = value;
					this.RaisePropertyChanged("CategoryId");
				}
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000198 RID: 408 RVA: 0x00007F08 File Offset: 0x00006108
		// (remove) Token: 0x06000199 RID: 409 RVA: 0x00007F40 File Offset: 0x00006140
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600019A RID: 410 RVA: 0x00007F78 File Offset: 0x00006178
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000BB RID: 187
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000BC RID: 188
		private int IndexOfAndGroupField;

		// Token: 0x040000BD RID: 189
		private Guid CategoryIdField;
	}
}
