using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200005F RID: 95
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetFileLocationsRequestBody
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x00003682 File Offset: 0x00001882
		public GetFileLocationsRequestBody()
		{
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009958 File Offset: 0x00007B58
		public GetFileLocationsRequestBody(Cookie cookie, ArrayOfBase64Binary fileDigests)
		{
			this.cookie = cookie;
			this.fileDigests = fileDigests;
		}

		// Token: 0x04000172 RID: 370
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public Cookie cookie;

		// Token: 0x04000173 RID: 371
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public ArrayOfBase64Binary fileDigests;
	}
}
