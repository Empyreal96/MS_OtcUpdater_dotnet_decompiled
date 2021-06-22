using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.SimpleAuthWebService
{
	// Token: 0x02000013 RID: 19
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetAuthorizationCookieRequest
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00003682 File Offset: 0x00001882
		public GetAuthorizationCookieRequest()
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000073BC File Offset: 0x000055BC
		public GetAuthorizationCookieRequest(GetAuthorizationCookieRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000081 RID: 129
		[MessageBodyMember(Name = "GetAuthorizationCookie", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/SimpleAuthWebService", Order = 0)]
		public GetAuthorizationCookieRequestBody Body;
	}
}
