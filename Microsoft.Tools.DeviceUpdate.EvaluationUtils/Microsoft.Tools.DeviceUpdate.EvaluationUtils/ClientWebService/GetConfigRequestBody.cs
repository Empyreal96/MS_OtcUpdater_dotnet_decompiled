using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200003B RID: 59
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetConfigRequestBody
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00003682 File Offset: 0x00001882
		public GetConfigRequestBody()
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000096D7 File Offset: 0x000078D7
		public GetConfigRequestBody(string protocolVersion)
		{
			this.protocolVersion = protocolVersion;
		}

		// Token: 0x0400013F RID: 319
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public string protocolVersion;
	}
}
