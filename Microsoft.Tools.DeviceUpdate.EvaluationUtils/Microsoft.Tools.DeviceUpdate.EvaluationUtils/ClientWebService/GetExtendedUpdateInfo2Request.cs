using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200005A RID: 90
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetExtendedUpdateInfo2Request
	{
		// Token: 0x060002EB RID: 747 RVA: 0x00003682 File Offset: 0x00001882
		public GetExtendedUpdateInfo2Request()
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000098F7 File Offset: 0x00007AF7
		public GetExtendedUpdateInfo2Request(GetExtendedUpdateInfo2RequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x0400016A RID: 362
		[MessageBodyMember(Name = "GetExtendedUpdateInfo2", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetExtendedUpdateInfo2RequestBody Body;
	}
}
