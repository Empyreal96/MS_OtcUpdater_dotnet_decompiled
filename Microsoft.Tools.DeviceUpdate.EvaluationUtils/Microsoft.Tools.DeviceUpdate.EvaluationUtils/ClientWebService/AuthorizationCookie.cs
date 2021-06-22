using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200001D RID: 29
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AuthorizationCookie", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class AuthorizationCookie : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000780C File Offset: 0x00005A0C
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00007814 File Offset: 0x00005A14
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000781D File Offset: 0x00005A1D
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00007825 File Offset: 0x00005A25
		[DataMember(EmitDefaultValue = false)]
		public string PlugInId
		{
			get
			{
				return this.PlugInIdField;
			}
			set
			{
				if (this.PlugInIdField != value)
				{
					this.PlugInIdField = value;
					this.RaisePropertyChanged("PlugInId");
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00007842 File Offset: 0x00005A42
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000784A File Offset: 0x00005A4A
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public byte[] CookieData
		{
			get
			{
				return this.CookieDataField;
			}
			set
			{
				if (this.CookieDataField != value)
				{
					this.CookieDataField = value;
					this.RaisePropertyChanged("CookieData");
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600014A RID: 330 RVA: 0x00007868 File Offset: 0x00005A68
		// (remove) Token: 0x0600014B RID: 331 RVA: 0x000078A0 File Offset: 0x00005AA0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600014C RID: 332 RVA: 0x000078D8 File Offset: 0x00005AD8
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000097 RID: 151
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000098 RID: 152
		[OptionalField]
		private string PlugInIdField;

		// Token: 0x04000099 RID: 153
		[OptionalField]
		private byte[] CookieDataField;
	}
}
