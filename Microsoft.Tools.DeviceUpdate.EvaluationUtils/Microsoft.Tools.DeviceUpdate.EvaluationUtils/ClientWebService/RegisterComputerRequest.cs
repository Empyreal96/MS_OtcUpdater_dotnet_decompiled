using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000042 RID: 66
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class RegisterComputerRequest
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00003682 File Offset: 0x00001882
		public RegisterComputerRequest()
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000975E File Offset: 0x0000795E
		public RegisterComputerRequest(RegisterComputerRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x0400014A RID: 330
		[MessageBodyMember(Name = "RegisterComputer", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public RegisterComputerRequestBody Body;
	}
}
