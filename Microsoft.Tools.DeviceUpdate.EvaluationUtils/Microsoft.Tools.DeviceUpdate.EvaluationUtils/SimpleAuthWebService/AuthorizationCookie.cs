using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.SimpleAuthWebService
{
	// Token: 0x02000011 RID: 17
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "AuthorizationCookie", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/SimpleAuthWebService")]
	[Serializable]
	public class AuthorizationCookie : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000072CC File Offset: 0x000054CC
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000072D4 File Offset: 0x000054D4
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000072DD File Offset: 0x000054DD
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000072E5 File Offset: 0x000054E5
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00007302 File Offset: 0x00005502
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000730A File Offset: 0x0000550A
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

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000109 RID: 265 RVA: 0x00007328 File Offset: 0x00005528
		// (remove) Token: 0x0600010A RID: 266 RVA: 0x00007360 File Offset: 0x00005560
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600010B RID: 267 RVA: 0x00007398 File Offset: 0x00005598
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400007D RID: 125
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400007E RID: 126
		[OptionalField]
		private string PlugInIdField;

		// Token: 0x0400007F RID: 127
		[OptionalField]
		private byte[] CookieDataField;
	}
}
