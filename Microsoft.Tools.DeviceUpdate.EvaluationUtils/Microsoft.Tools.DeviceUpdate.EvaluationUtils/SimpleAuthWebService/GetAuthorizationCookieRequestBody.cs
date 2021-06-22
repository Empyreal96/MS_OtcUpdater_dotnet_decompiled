using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.SimpleAuthWebService
{
	// Token: 0x02000014 RID: 20
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/SimpleAuthWebService")]
	public class GetAuthorizationCookieRequestBody
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00003682 File Offset: 0x00001882
		public GetAuthorizationCookieRequestBody()
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000073CB File Offset: 0x000055CB
		public GetAuthorizationCookieRequestBody(string clientId, string targetGroupName, string dnsName)
		{
			this.clientId = clientId;
			this.targetGroupName = targetGroupName;
			this.dnsName = dnsName;
		}

		// Token: 0x04000082 RID: 130
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public string clientId;

		// Token: 0x04000083 RID: 131
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string targetGroupName;

		// Token: 0x04000084 RID: 132
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string dnsName;
	}
}
