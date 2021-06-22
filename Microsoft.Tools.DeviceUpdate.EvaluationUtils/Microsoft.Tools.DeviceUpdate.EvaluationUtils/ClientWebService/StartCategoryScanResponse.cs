using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000048 RID: 72
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class StartCategoryScanResponse
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00003682 File Offset: 0x00001882
		public StartCategoryScanResponse()
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000097B0 File Offset: 0x000079B0
		public StartCategoryScanResponse(StartCategoryScanResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000150 RID: 336
		[MessageBodyMember(Name = "StartCategoryScanResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public StartCategoryScanResponseBody Body;
	}
}
