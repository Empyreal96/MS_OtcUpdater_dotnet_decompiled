using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200005C RID: 92
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetExtendedUpdateInfo2Response
	{
		// Token: 0x060002EF RID: 751 RVA: 0x00003682 File Offset: 0x00001882
		public GetExtendedUpdateInfo2Response()
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000992B File Offset: 0x00007B2B
		public GetExtendedUpdateInfo2Response(GetExtendedUpdateInfo2ResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x0400016F RID: 367
		[MessageBodyMember(Name = "GetExtendedUpdateInfo2Response", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetExtendedUpdateInfo2ResponseBody Body;
	}
}
