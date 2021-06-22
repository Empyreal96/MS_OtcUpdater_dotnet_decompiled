using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200004A RID: 74
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class SyncUpdatesRequest
	{
		// Token: 0x060002CB RID: 715 RVA: 0x00003682 File Offset: 0x00001882
		public SyncUpdatesRequest()
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000097D5 File Offset: 0x000079D5
		public SyncUpdatesRequest(SyncUpdatesRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000153 RID: 339
		[MessageBodyMember(Name = "SyncUpdates", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public SyncUpdatesRequestBody Body;
	}
}
