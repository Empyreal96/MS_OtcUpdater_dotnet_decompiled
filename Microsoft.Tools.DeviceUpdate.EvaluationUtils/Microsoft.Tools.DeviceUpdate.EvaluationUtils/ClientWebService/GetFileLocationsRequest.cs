using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200005E RID: 94
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class GetFileLocationsRequest
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x00003682 File Offset: 0x00001882
		public GetFileLocationsRequest()
		{
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009949 File Offset: 0x00007B49
		public GetFileLocationsRequest(GetFileLocationsRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000171 RID: 369
		[MessageBodyMember(Name = "GetFileLocations", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public GetFileLocationsRequestBody Body;
	}
}
