using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000044 RID: 68
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class RegisterComputerResponse
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00003682 File Offset: 0x00001882
		public RegisterComputerResponse()
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00009783 File Offset: 0x00007983
		public RegisterComputerResponse(RegisterComputerResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x0400014D RID: 333
		[MessageBodyMember(Name = "RegisterComputerResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public RegisterComputerResponseBody Body;
	}
}
