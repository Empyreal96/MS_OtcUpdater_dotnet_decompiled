using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000040 RID: 64
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetCookieResponse
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00003682 File Offset: 0x00001882
		public GetCookieResponse()
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00009740 File Offset: 0x00007940
		public GetCookieResponse(GetCookieResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000148 RID: 328
		[MessageBodyMember(Name = "GetCookieResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetCookieResponseBody Body;
	}
}
