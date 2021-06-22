using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000056 RID: 86
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetExtendedUpdateInfoRequest
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00003682 File Offset: 0x00001882
		public GetExtendedUpdateInfoRequest()
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000098A5 File Offset: 0x00007AA5
		public GetExtendedUpdateInfoRequest(GetExtendedUpdateInfoRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000163 RID: 355
		[MessageBodyMember(Name = "GetExtendedUpdateInfo", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetExtendedUpdateInfoRequestBody Body;
	}
}
