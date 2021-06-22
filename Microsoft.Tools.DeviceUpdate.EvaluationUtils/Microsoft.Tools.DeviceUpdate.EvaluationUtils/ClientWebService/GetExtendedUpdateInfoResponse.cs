using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000058 RID: 88
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetExtendedUpdateInfoResponse
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00003682 File Offset: 0x00001882
		public GetExtendedUpdateInfoResponse()
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000098D9 File Offset: 0x00007AD9
		public GetExtendedUpdateInfoResponse(GetExtendedUpdateInfoResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000168 RID: 360
		[MessageBodyMember(Name = "GetExtendedUpdateInfoResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetExtendedUpdateInfoResponseBody Body;
	}
}
