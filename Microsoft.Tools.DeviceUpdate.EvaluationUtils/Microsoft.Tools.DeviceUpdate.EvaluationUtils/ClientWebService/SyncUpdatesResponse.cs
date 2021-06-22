using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200004C RID: 76
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class SyncUpdatesResponse
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00003682 File Offset: 0x00001882
		public SyncUpdatesResponse()
		{
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000097FA File Offset: 0x000079FA
		public SyncUpdatesResponse(SyncUpdatesResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000156 RID: 342
		[MessageBodyMember(Name = "SyncUpdatesResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public SyncUpdatesResponseBody Body;
	}
}
