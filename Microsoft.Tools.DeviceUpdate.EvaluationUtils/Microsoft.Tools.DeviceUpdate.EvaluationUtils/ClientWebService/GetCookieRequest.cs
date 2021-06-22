using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200003E RID: 62
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetCookieRequest
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x00003682 File Offset: 0x00001882
		public GetCookieRequest()
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00009704 File Offset: 0x00007904
		public GetCookieRequest(GetCookieRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000142 RID: 322
		[MessageBodyMember(Name = "GetCookie", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetCookieRequestBody Body;
	}
}
