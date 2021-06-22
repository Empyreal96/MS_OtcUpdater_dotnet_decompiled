using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000055 RID: 85
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class RefreshCacheResponseBody
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00003682 File Offset: 0x00001882
		public RefreshCacheResponseBody()
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009896 File Offset: 0x00007A96
		public RefreshCacheResponseBody(RefreshCacheResult[] RefreshCacheResult)
		{
			this.RefreshCacheResult = RefreshCacheResult;
		}

		// Token: 0x04000162 RID: 354
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public RefreshCacheResult[] RefreshCacheResult;
	}
}
