using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200003C RID: 60
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetConfigResponse
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x00003682 File Offset: 0x00001882
		public GetConfigResponse()
		{
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000096E6 File Offset: 0x000078E6
		public GetConfigResponse(GetConfigResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000140 RID: 320
		[MessageBodyMember(Name = "GetConfigResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetConfigResponseBody Body;
	}
}
