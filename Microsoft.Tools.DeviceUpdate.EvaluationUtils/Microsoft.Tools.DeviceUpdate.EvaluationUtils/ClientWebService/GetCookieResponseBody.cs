using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000041 RID: 65
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetCookieResponseBody
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00003682 File Offset: 0x00001882
		public GetCookieResponseBody()
		{
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000974F File Offset: 0x0000794F
		public GetCookieResponseBody(Cookie GetCookieResult)
		{
			this.GetCookieResult = GetCookieResult;
		}

		// Token: 0x04000149 RID: 329
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public Cookie GetCookieResult;
	}
}
