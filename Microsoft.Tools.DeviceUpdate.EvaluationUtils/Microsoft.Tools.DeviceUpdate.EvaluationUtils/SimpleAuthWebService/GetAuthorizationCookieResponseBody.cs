using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.SimpleAuthWebService
{
	// Token: 0x02000016 RID: 22
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/SimpleAuthWebService")]
	public class GetAuthorizationCookieResponseBody
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00003682 File Offset: 0x00001882
		public GetAuthorizationCookieResponseBody()
		{
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000073F7 File Offset: 0x000055F7
		public GetAuthorizationCookieResponseBody(AuthorizationCookie GetAuthorizationCookieResult)
		{
			this.GetAuthorizationCookieResult = GetAuthorizationCookieResult;
		}

		// Token: 0x04000086 RID: 134
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public AuthorizationCookie GetAuthorizationCookieResult;
	}
}
