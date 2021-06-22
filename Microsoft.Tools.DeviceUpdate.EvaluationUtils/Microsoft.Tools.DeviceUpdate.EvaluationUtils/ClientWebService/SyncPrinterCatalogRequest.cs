using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x0200004E RID: 78
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(IsWrapped = false)]
	public class SyncPrinterCatalogRequest
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x00003682 File Offset: 0x00001882
		public SyncPrinterCatalogRequest()
		{
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00009818 File Offset: 0x00007A18
		public SyncPrinterCatalogRequest(SyncPrinterCatalogRequestBody Body)
		{
			this.Body = Body;
		}

		// Token: 0x04000158 RID: 344
		[MessageBodyMember(Name = "SyncPrinterCatalog", Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService", Order = 0)]
		public SyncPrinterCatalogRequestBody Body;
	}
}
