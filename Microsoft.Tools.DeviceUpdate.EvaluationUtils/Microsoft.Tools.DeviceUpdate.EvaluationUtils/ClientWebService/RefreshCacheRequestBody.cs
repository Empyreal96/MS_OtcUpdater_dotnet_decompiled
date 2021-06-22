using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000053 RID: 83
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class RefreshCacheRequestBody
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00003682 File Offset: 0x00001882
		public RefreshCacheRequestBody()
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00009871 File Offset: 0x00007A71
		public RefreshCacheRequestBody(Cookie cookie, UpdateIdentity[] globalIDs)
		{
			this.cookie = cookie;
			this.globalIDs = globalIDs;
		}

		// Token: 0x0400015F RID: 351
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public Cookie cookie;

		// Token: 0x04000160 RID: 352
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public UpdateIdentity[] globalIDs;
	}
}
