using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000019 RID: 25
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Config", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class Config : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007497 File Offset: 0x00005697
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000749F File Offset: 0x0000569F
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000074A8 File Offset: 0x000056A8
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000074B0 File Offset: 0x000056B0
		[DataMember(IsRequired = true)]
		public DateTime LastChange
		{
			get
			{
				return this.LastChangeField;
			}
			set
			{
				if (!this.LastChangeField.Equals(value))
				{
					this.LastChangeField = value;
					this.RaisePropertyChanged("LastChange");
				}
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000074D2 File Offset: 0x000056D2
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000074DA File Offset: 0x000056DA
		[DataMember(IsRequired = true, Order = 1)]
		public bool IsRegistrationRequired
		{
			get
			{
				return this.IsRegistrationRequiredField;
			}
			set
			{
				if (!this.IsRegistrationRequiredField.Equals(value))
				{
					this.IsRegistrationRequiredField = value;
					this.RaisePropertyChanged("IsRegistrationRequired");
				}
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000074FC File Offset: 0x000056FC
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00007504 File Offset: 0x00005704
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public AuthPlugInInfo[] AuthInfo
		{
			get
			{
				return this.AuthInfoField;
			}
			set
			{
				if (this.AuthInfoField != value)
				{
					this.AuthInfoField = value;
					this.RaisePropertyChanged("AuthInfo");
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00007521 File Offset: 0x00005721
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00007529 File Offset: 0x00005729
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public ArrayOfInt AllowedEventIds
		{
			get
			{
				return this.AllowedEventIdsField;
			}
			set
			{
				if (this.AllowedEventIdsField != value)
				{
					this.AllowedEventIdsField = value;
					this.RaisePropertyChanged("AllowedEventIds");
				}
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00007546 File Offset: 0x00005746
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000754E File Offset: 0x0000574E
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public ConfigurationProperty[] Properties
		{
			get
			{
				return this.PropertiesField;
			}
			set
			{
				if (this.PropertiesField != value)
				{
					this.PropertiesField = value;
					this.RaisePropertyChanged("Properties");
				}
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000129 RID: 297 RVA: 0x0000756C File Offset: 0x0000576C
		// (remove) Token: 0x0600012A RID: 298 RVA: 0x000075A4 File Offset: 0x000057A4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600012B RID: 299 RVA: 0x000075DC File Offset: 0x000057DC
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000087 RID: 135
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000088 RID: 136
		private DateTime LastChangeField;

		// Token: 0x04000089 RID: 137
		private bool IsRegistrationRequiredField;

		// Token: 0x0400008A RID: 138
		[OptionalField]
		private AuthPlugInInfo[] AuthInfoField;

		// Token: 0x0400008B RID: 139
		[OptionalField]
		private ArrayOfInt AllowedEventIdsField;

		// Token: 0x0400008C RID: 140
		[OptionalField]
		private ConfigurationProperty[] PropertiesField;
	}
}
