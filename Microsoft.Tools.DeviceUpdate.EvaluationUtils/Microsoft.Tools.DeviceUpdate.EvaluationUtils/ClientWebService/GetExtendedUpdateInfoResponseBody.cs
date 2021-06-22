using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000059 RID: 89
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetExtendedUpdateInfoResponseBody
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00003682 File Offset: 0x00001882
		public GetExtendedUpdateInfoResponseBody()
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000098E8 File Offset: 0x00007AE8
		public GetExtendedUpdateInfoResponseBody(ExtendedUpdateInfo GetExtendedUpdateInfoResult)
		{
			this.GetExtendedUpdateInfoResult = GetExtendedUpdateInfoResult;
		}

		// Token: 0x04000169 RID: 361
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public ExtendedUpdateInfo GetExtendedUpdateInfoResult;
	}
}
