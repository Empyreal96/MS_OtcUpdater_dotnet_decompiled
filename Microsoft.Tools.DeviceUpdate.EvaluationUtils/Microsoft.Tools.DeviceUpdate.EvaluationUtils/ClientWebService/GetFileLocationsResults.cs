using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000038 RID: 56
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "GetFileLocationsResults", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	[Serializable]
	public class GetFileLocationsResults : IExtensibleDataObject, INotifyPropertyChanged
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000298 RID: 664 RVA: 0x000095D8 File Offset: 0x000077D8
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000095E0 File Offset: 0x000077E0
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000095E9 File Offset: 0x000077E9
		// (set) Token: 0x0600029B RID: 667 RVA: 0x000095F1 File Offset: 0x000077F1
		[DataMember(EmitDefaultValue = false)]
		public FileLocation[] FileLocations
		{
			get
			{
				return this.FileLocationsField;
			}
			set
			{
				if (this.FileLocationsField != value)
				{
					this.FileLocationsField = value;
					this.RaisePropertyChanged("FileLocations");
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000960E File Offset: 0x0000780E
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00009616 File Offset: 0x00007816
		[DataMember(EmitDefaultValue = false)]
		public Cookie NewCookie
		{
			get
			{
				return this.NewCookieField;
			}
			set
			{
				if (this.NewCookieField != value)
				{
					this.NewCookieField = value;
					this.RaisePropertyChanged("NewCookie");
				}
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600029E RID: 670 RVA: 0x00009634 File Offset: 0x00007834
		// (remove) Token: 0x0600029F RID: 671 RVA: 0x0000966C File Offset: 0x0000786C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060002A0 RID: 672 RVA: 0x000096A4 File Offset: 0x000078A4
		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400013A RID: 314
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400013B RID: 315
		[OptionalField]
		private FileLocation[] FileLocationsField;

		// Token: 0x0400013C RID: 316
		[OptionalField]
		private Cookie NewCookieField;
	}
}
