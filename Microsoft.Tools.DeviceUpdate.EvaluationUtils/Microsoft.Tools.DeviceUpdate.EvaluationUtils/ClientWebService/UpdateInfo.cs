using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000029 RID: 41
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "UpdateInfo", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class UpdateInfo : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00008848 File Offset: 0x00006A48
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00008850 File Offset: 0x00006A50
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00008859 File Offset: 0x00006A59
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00008861 File Offset: 0x00006A61
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00008883 File Offset: 0x00006A83
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000888B File Offset: 0x00006A8B
		[DataMember(EmitDefaultValue = false, Order = 1)]
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000088A8 File Offset: 0x00006AA8
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000088B0 File Offset: 0x00006AB0
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000088D2 File Offset: 0x00006AD2
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000088DA File Offset: 0x00006ADA
		[DataMember(EmitDefaultValue = false, Order = 3)]
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

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600020A RID: 522 RVA: 0x000088F8 File Offset: 0x00006AF8
		// (remove) Token: 0x0600020B RID: 523 RVA: 0x00008930 File Offset: 0x00006B30
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600020C RID: 524 RVA: 0x00008968 File Offset: 0x00006B68
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x040000EA RID: 234
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x040000EB RID: 235
		private int IDField;

		// Token: 0x040000EC RID: 236
		[OptionalField]
		private Deployment DeploymentField;

		// Token: 0x040000ED RID: 237
		private bool IsLeafField;

		// Token: 0x040000EE RID: 238
		[OptionalField]
		private string XmlField;
	}
}
