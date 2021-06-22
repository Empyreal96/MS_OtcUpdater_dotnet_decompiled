using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200003D RID: 61
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetConfigResponseBody
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x00003682 File Offset: 0x00001882
		public GetConfigResponseBody()
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000096F5 File Offset: 0x000078F5
		public GetConfigResponseBody(Config GetConfigResult)
		{
			this.GetConfigResult = GetConfigResult;
		}

		// Token: 0x04000141 RID: 321
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public Config GetConfigResult;
	}
}
