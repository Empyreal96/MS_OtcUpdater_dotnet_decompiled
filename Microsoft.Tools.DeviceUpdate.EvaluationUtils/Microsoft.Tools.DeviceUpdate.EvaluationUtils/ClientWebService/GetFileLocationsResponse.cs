using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000060 RID: 96
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetFileLocationsResponse
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x00003682 File Offset: 0x00001882
		public GetFileLocationsResponse()
		{
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000996E File Offset: 0x00007B6E
		public GetFileLocationsResponse(GetFileLocationsResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000174 RID: 372
		[MessageBodyMember(Name = "GetFileLocationsResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetFileLocationsResponseBody Body;
	}
}
