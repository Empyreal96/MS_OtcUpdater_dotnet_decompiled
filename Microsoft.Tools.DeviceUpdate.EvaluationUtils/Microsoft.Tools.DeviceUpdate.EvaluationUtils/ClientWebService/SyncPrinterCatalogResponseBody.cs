using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Tools.DeviceUpdate.EvaluationUtils.ClientWebService
{
	// Token: 0x02000051 RID: 81
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DataContract(Namespace = "http://www.microsoft.com/SoftwareDistribution/Server/ClientWebService")]
	public class SyncPrinterCatalogResponseBody
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00003682 File Offset: 0x00001882
		public SyncPrinterCatalogResponseBody()
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009853 File Offset: 0x00007A53
		public SyncPrinterCatalogResponseBody(SyncInfo SyncPrinterCatalogResult)
		{
			this.SyncPrinterCatalogResult = SyncPrinterCatalogResult;
		}

		// Token: 0x0400015D RID: 349
		[DataMember(EmitDefaultValue = false, Order = 0)]
		public SyncInfo SyncPrinterCatalogResult;
	}
}
