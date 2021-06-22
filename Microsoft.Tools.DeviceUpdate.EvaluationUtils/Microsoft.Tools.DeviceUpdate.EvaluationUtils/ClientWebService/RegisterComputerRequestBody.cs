using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000043 RID: 67
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class RegisterComputerRequestBody
	{
		// Token: 0x060002BE RID: 702 RVA: 0x00003682 File Offset: 0x00001882
		public RegisterComputerRequestBody()
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000976D File Offset: 0x0000796D
		public RegisterComputerRequestBody(Cookie cookie, ComputerInfo computerInfo)
		{
			this.cookie = cookie;
			this.computerInfo = computerInfo;
		}

		// Token: 0x0400014B RID: 331
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public Cookie cookie;

		// Token: 0x0400014C RID: 332
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public ComputerInfo computerInfo;
	}
}
