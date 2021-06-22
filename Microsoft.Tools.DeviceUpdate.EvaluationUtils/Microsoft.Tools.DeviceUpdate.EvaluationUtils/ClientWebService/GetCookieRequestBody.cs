using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200003F RID: 63
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class GetCookieRequestBody
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x00003682 File Offset: 0x00001882
		public GetCookieRequestBody()
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009713 File Offset: 0x00007913
		public GetCookieRequestBody(AuthorizationCookie[] authCookies, Cookie oldCookie, DateTime lastChange, DateTime currentTime, string protocolVersion)
		{
			this.authCookies = authCookies;
			this.oldCookie = oldCookie;
			this.lastChange = lastChange;
			this.currentTime = currentTime;
			this.protocolVersion = protocolVersion;
		}

		// Token: 0x04000143 RID: 323
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public AuthorizationCookie[] authCookies;

		// Token: 0x04000144 RID: 324
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public Cookie oldCookie;

		// Token: 0x04000145 RID: 325
		[DataMember(Order = 2)]
		public DateTime lastChange;

		// Token: 0x04000146 RID: 326
		[DataMember(Order = 3)]
		public DateTime currentTime;

		// Token: 0x04000147 RID: 327
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string protocolVersion;
	}
}
