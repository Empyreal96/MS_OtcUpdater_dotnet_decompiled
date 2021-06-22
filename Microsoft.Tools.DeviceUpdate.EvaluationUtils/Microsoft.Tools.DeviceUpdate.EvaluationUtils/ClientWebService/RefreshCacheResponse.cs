using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000054 RID: 84
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class RefreshCacheResponse
	{
		// Token: 0x060002DF RID: 735 RVA: 0x00003682 File Offset: 0x00001882
		public RefreshCacheResponse()
		{
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009887 File Offset: 0x00007A87
		public RefreshCacheResponse(RefreshCacheResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000161 RID: 353
		[MessageBodyMember(Name = "RefreshCacheResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public RefreshCacheResponseBody Body;
	}
}
