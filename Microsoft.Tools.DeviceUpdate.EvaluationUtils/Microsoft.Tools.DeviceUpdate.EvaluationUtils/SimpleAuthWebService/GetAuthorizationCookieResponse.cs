using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.SimpleAuthWebService
{
	// Token: 0x02000015 RID: 21
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetAuthorizationCookieResponse
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00003682 File Offset: 0x00001882
		public GetAuthorizationCookieResponse()
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000073E8 File Offset: 0x000055E8
		public GetAuthorizationCookieResponse(GetAuthorizationCookieResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000085 RID: 133
		[MessageBodyMember(Name = "GetAuthorizationCookieResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/SimpleAuthWebService", Order = 0)]
		public GetAuthorizationCookieResponseBody Body;
	}
}
