using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200002E RID: 46
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "XmlUpdateFragmentType", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public enum XmlUpdateFragmentType
	{
		// Token: 0x0400010F RID: 271
		[EnumMember]
		Published,
		// Token: 0x04000110 RID: 272
		[EnumMember]
		Core,
		// Token: 0x04000111 RID: 273
		[EnumMember]
		Extended,
		// Token: 0x04000112 RID: 274
		[EnumMember]
		VerificationRule,
		// Token: 0x04000113 RID: 275
		[EnumMember]
		LocalizedProperties,
		// Token: 0x04000114 RID: 276
		[EnumMember]
		Eula,
		// Token: 0x04000115 RID: 277
		[EnumMember]
		FileUrl,
		// Token: 0x04000116 RID: 278
		[EnumMember]
		FileDecryption
	}
}
