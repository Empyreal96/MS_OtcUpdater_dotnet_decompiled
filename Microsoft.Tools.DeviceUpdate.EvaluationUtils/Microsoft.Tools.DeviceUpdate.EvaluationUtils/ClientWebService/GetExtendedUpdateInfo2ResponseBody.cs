using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200005D RID: 93
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetExtendedUpdateInfo2ResponseBody
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00003682 File Offset: 0x00001882
		public GetExtendedUpdateInfo2ResponseBody()
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000993A File Offset: 0x00007B3A
		public GetExtendedUpdateInfo2ResponseBody(ExtendedUpdateInfo2 GetExtendedUpdateInfo2Result)
		{
			this.GetExtendedUpdateInfo2Result = GetExtendedUpdateInfo2Result;
		}

		// Token: 0x04000170 RID: 368
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public ExtendedUpdateInfo2 GetExtendedUpdateInfo2Result;
	}
}
