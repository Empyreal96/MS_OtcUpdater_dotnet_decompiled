using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000061 RID: 97
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetFileLocationsResponseBody
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x00003682 File Offset: 0x00001882
		public GetFileLocationsResponseBody()
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000997D File Offset: 0x00007B7D
		public GetFileLocationsResponseBody(GetFileLocationsResults GetFileLocationsResult)
		{
			this.GetFileLocationsResult = GetFileLocationsResult;
		}

		// Token: 0x04000175 RID: 373
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public GetFileLocationsResults GetFileLocationsResult;
	}
}
