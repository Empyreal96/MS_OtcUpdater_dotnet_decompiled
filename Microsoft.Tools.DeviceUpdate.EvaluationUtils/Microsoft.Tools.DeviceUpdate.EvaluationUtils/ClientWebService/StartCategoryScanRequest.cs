using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000046 RID: 70
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class StartCategoryScanRequest
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00003682 File Offset: 0x00001882
		public StartCategoryScanRequest()
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00009792 File Offset: 0x00007992
		public StartCategoryScanRequest(StartCategoryScanRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x0400014E RID: 334
		[MessageBodyMember(Name = "StartCategoryScan", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public StartCategoryScanRequestBody Body;
	}
}
