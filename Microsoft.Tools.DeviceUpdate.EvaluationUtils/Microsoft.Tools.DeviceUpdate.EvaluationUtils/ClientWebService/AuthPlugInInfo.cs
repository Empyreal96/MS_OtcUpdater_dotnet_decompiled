using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200001A RID: 26
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AuthPlugInInfo", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class AuthPlugInInfo : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00007600 File Offset: 0x00005800
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00007608 File Offset: 0x00005808
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007611 File Offset: 0x00005811
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00007619 File Offset: 0x00005819
		[DataMember(EmitDefaultValue = false)]
		public string PlugInID
		{
			get
			{
				return this.PlugInIDField;
			}
			set
			{
				if (this.PlugInIDField != value)
				{
					this.PlugInIDField = value;
					this.RaisePropertyChanged("PlugInID");
				}
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00007636 File Offset: 0x00005836
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000763E File Offset: 0x0000583E
		[DataMember(EmitDefaultValue = false)]
		public string ServiceUrl
		{
			get
			{
				return this.ServiceUrlField;
			}
			set
			{
				if (this.ServiceUrlField != value)
				{
					this.ServiceUrlField = value;
					this.RaisePropertyChanged("ServiceUrl");
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000765B File Offset: 0x0000585B
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00007663 File Offset: 0x00005863
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string Parameter
		{
			get
			{
				return this.ParameterField;
			}
			set
			{
				if (this.ParameterField != value)
				{
					this.ParameterField = value;
					this.RaisePropertyChanged("Parameter");
				}
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000135 RID: 309 RVA: 0x00007680 File Offset: 0x00005880
		// (remove) Token: 0x06000136 RID: 310 RVA: 0x000076B8 File Offset: 0x000058B8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000137 RID: 311 RVA: 0x000076F0 File Offset: 0x000058F0
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400008E RID: 142
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400008F RID: 143
		[OptionalField]
		private string PlugInIDField;

		// Token: 0x04000090 RID: 144
		[OptionalField]
		private string ServiceUrlField;

		// Token: 0x04000091 RID: 145
		[OptionalField]
		private string ParameterField;
	}
}
