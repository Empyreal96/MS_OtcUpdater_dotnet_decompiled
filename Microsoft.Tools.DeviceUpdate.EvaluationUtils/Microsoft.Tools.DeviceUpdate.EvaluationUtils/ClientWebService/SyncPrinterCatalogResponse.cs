using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000050 RID: 80
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class SyncPrinterCatalogResponse
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00003682 File Offset: 0x00001882
		public SyncPrinterCatalogResponse()
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00009844 File Offset: 0x00007A44
		public SyncPrinterCatalogResponse(SyncPrinterCatalogResponseBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x0400015C RID: 348
		[MessageBodyMember(Name = "SyncPrinterCatalogResponse", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public SyncPrinterCatalogResponseBody Body;
	}
}
