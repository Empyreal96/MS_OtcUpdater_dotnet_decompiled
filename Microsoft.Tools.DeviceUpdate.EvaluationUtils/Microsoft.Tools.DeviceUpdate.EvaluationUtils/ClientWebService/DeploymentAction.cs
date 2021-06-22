using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200002B RID: 43
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DeploymentAction", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public enum DeploymentAction
	{
		// Token: 0x040000FE RID: 254
		[EnumMember]
		Install,
		// Token: 0x040000FF RID: 255
		[EnumMember]
		Uninstall,
		// Token: 0x04000100 RID: 256
		[EnumMember]
		PreDeploymentCheck,
		// Token: 0x04000101 RID: 257
		[EnumMember]
		Block,
		// Token: 0x04000102 RID: 258
		[EnumMember]
		Evaluate,
		// Token: 0x04000103 RID: 259
		[EnumMember]
		Bundle
	}
}
