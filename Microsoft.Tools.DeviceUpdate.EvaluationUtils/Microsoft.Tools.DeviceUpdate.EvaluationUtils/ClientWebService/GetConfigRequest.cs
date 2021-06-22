using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200003A RID: 58
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetConfigRequest
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00003682 File Offset: 0x00001882
		public GetConfigRequest()
		{
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000096C8 File Offset: 0x000078C8
		public GetConfigRequest(GetConfigRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x0400013E RID: 318
		[MessageBodyMember(Name = "GetConfig", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetConfigRequestBody Body;
	}
}
