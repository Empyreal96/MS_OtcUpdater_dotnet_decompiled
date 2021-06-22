using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000052 RID: 82
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class RefreshCacheRequest
	{
		// Token: 0x060002DB RID: 731 RVA: 0x00003682 File Offset: 0x00001882
		public RefreshCacheRequest()
		{
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009862 File Offset: 0x00007A62
		public RefreshCacheRequest(RefreshCacheRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x0400015E RID: 350
		[MessageBodyMember(Name = "RefreshCache", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public RefreshCacheRequestBody Body;
	}
}
