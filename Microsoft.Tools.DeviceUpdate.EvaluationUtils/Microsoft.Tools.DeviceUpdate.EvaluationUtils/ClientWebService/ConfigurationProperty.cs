using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200001C RID: 28
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ConfigurationProperty", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class ConfigurationProperty : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000771C File Offset: 0x0000591C
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00007724 File Offset: 0x00005924
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000772D File Offset: 0x0000592D
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00007735 File Offset: 0x00005935
		[DataMember(EmitDefaultValue = false)]
		public string Name
		{
			get
			{
				return this.NameField;
			}
			set
			{
				if (this.NameField != value)
				{
					this.NameField = value;
					this.RaisePropertyChanged("Name");
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007752 File Offset: 0x00005952
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000775A File Offset: 0x0000595A
		[DataMember(EmitDefaultValue = false)]
		public string Value
		{
			get
			{
				return this.ValueField;
			}
			set
			{
				if (this.ValueField != value)
				{
					this.ValueField = value;
					this.RaisePropertyChanged("Value");
				}
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000140 RID: 320 RVA: 0x00007778 File Offset: 0x00005978
		// (remove) Token: 0x06000141 RID: 321 RVA: 0x000077B0 File Offset: 0x000059B0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000142 RID: 322 RVA: 0x000077E8 File Offset: 0x000059E8
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x04000093 RID: 147
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x04000094 RID: 148
		[OptionalField]
		private string NameField;

		// Token: 0x04000095 RID: 149
		[OptionalField]
		private string ValueField;
	}
}
