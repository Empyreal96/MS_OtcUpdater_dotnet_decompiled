using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200004B RID: 75
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class SyncUpdatesRequestBody
	{
		// Token: 0x060002CD RID: 717 RVA: 0x00003682 File Offset: 0x00001882
		public SyncUpdatesRequestBody()
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000097E4 File Offset: 0x000079E4
		public SyncUpdatesRequestBody(Cookie cookie, SyncUpdateParameters parameters)
		{
			this.cookie = cookie;
			this.parameters = parameters;
		}

		// Token: 0x04000154 RID: 340
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public Cookie cookie;

		// Token: 0x04000155 RID: 341
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public SyncUpdateParameters parameters;
	}
}
